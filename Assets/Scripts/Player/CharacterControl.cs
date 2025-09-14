using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * CharacterControl
 * 플레이어 캐릭터의 이동, 공격, 스킬, 상태 관리
 * 주요 기능 : 
 * - 플레이어 이동 처리(마우시 기반)
 *      - 우클릭 시 목표 지점 설정 및 이동
 *      - 이동 중 캐릭터 회전 처리
 *      - 클릭 위치에 시각적 파티클 생성
 * 
 * - 공격 처리
 *      - 좌클릭으로 기본 공격 발동
 *      - Q/W/E/R 스킬 발동 처리 및 쿨타임 관리
 *      - 포션 사용 처리(T키)
 *      - 공격 상태 시 애니메이션 트리거 설정
 *      - 스킬 연속 발사, 스택형 스킬 등 특수 기능 처리
 *      
 * - 스킬 및 쿨타임 관리
 *      - KeyCode별 스킬 쿨타임과 마지막 사용 시간 저장
 *      - CanUseSkill()로 스킬 사용 가능 여부 판단
 *      - 스택형 스킬 처리 및 StackTimer 연동
 *      
 * - 상태 머신 적용
 *      - Idle/Move/Attack 상태를 IState 인터페이스 기반으로 관리
 *      - sm.SetState를 통해 상태 전환
 *      - FixedUpdate에서 상태별 OperateUpdate 호출
 *      
 * - 캐릭터 중력 및 이동 처리
 *      - CharacterController를 사용하여 이동 및 중력 적용
 *      - PlayerMoveHandle에서 이동 방향, 속도, 회전 처리
 *      
 *  - 기타 기능
 *      - 캐릭터 Hp 0시 사망 애니메이션 처리
 *      - 포션 사용 시 회복 이펙트 처리
 */
public class CharacterControl : MonoBehaviour
{
    public float Speed = 4.0f;
    public float rotateSpeed = 10.0f;
    public Animator charAnimator;
    private Vector3 destinationPoint;
    public bool shouldMove = false;
    public bool isAttacking = false;
    public GameObject Attack05Particle;
    public Vector3 currentTargetPosition;
    public GameObject Boss;

    private Dictionary<KeyCode, float> skillCooldowns = new Dictionary<KeyCode, float>();
    private Dictionary<KeyCode, float> lastSkillUseTime = new Dictionary<KeyCode, float>();

    public float Attack02CoolDown = 2.0f;
    public float Attack03CoolDown = 3.0f;
    public float Attack04CoolDown = 4.0f;
    public float Attack05CoolDown = 5.0f;
    public Character characterSetting;
    CharacterController characterCon;
    private Vector3 moveDirection;
    private LayerMask clickLayer;
    private float verticalVelocity;
    public float gravity = -9.81f;
    private bool isDead;
    public GameObject clickParticlePrefab;
    private CharacterAnimationEvent characterAnimationEvent;
    private SkillCooldownManager skillCooldownManager;
    public enum CharState
    {
        Idle,
        Move,
        Attack,
    }

    public float maxSpeed = 2.0f;

    public float CurrentSpeed { get; set; }
    public Direction CurrentTurnDirection { get; private set; }
    public enum Direction
    {
        Left = -1,
        Right = 1,
    }

    private Dictionary<CharState, IState<CharacterControl>> dicState = new Dictionary<CharState, IState<CharacterControl>>();
    private StateMachine<CharacterControl> sm;
    // Start is called before the first frame update
    void Start()
    {
        characterSetting = this.GetComponent<Character>();
        IState<CharacterControl> idle = new CharacterIdle();
        IState<CharacterControl> move = new CharacterMove();
        IState<CharacterControl> attack = new CharacterAttack();

        dicState.Add(CharState.Idle, idle);
        dicState.Add(CharState.Move, move);
        dicState.Add(CharState.Attack, attack);

        sm = new StateMachine<CharacterControl>(this, dicState[CharState.Idle]);

        charAnimator = GetComponent<Animator>();

        skillCooldowns[KeyCode.Q] = Attack02CoolDown;
        skillCooldowns[KeyCode.W] = Attack03CoolDown;
        skillCooldowns[KeyCode.E] = Attack04CoolDown;
        skillCooldowns[KeyCode.R] = Attack05CoolDown;

        lastSkillUseTime[KeyCode.Q] = -Attack02CoolDown;
        lastSkillUseTime[KeyCode.W] = -Attack03CoolDown;
        lastSkillUseTime[KeyCode.E] = -Attack04CoolDown;
        lastSkillUseTime[KeyCode.R] = -Attack05CoolDown;
        isDead = false;

        characterCon = GetComponent<CharacterController>();
        clickLayer = LayerMask.GetMask("Terrains");

        characterAnimationEvent = GetComponent<CharacterAnimationEvent>();
        skillCooldownManager = GetComponent<SkillCooldownManager>();
    }

    public void ChangeToIdleState()
    {
        sm.SetState(dicState[CharState.Idle]);
        if(isAttacking)
            isAttacking = false;
    }

    private void LookAtBoss()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            currentTargetPosition = hit.point;
            destinationPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);

            // 캐릭터를 즉시 클릭한 위치로 회전
            Quaternion targetRotation = Quaternion.LookRotation(destinationPoint - transform.position);
            transform.rotation = targetRotation;
        }
    }
    private bool CanUseSkill(KeyCode key)
    {
        if(key == KeyCode.E)
        {
            if (StatManger.Instance.eStackEnabled)
            {
                return StatManger.Instance.eCurrentStack > 0;
            }
            else
            {
                return Time.time >= lastSkillUseTime[key] + skillCooldowns[key];
            }
        }
        return Time.time >= lastSkillUseTime[key] + skillCooldowns[key];
    }
    public void SetAttackNum()
    {
        if (Input.GetKeyDown(KeyCode.Q) && CanUseSkill(KeyCode.Q))
        {
            lastSkillUseTime[KeyCode.Q] = Time.time;
            charAnimator.SetInteger("Attack_num", 1);
            shouldMove = false;
            sm.SetState(dicState[CharState.Attack]);
            LookAtBoss();
            characterSetting.UseMp(10.0f);
            isAttacking = true;
            if (StatManger.Instance.qCoolReset)
                UpgradeSkillFunction.Instance.Choice16_1();
        }
        else if (Input.GetKeyDown(KeyCode.W) && CanUseSkill(KeyCode.W))
        {
            lastSkillUseTime[KeyCode.W] = Time.time;
            charAnimator.SetInteger("Attack_num", 2);
            shouldMove = false;
            sm.SetState(dicState[CharState.Attack]);
            LookAtBoss();
            characterSetting.UseMp(10.0f);
            isAttacking = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && CanUseSkill(KeyCode.E))
        {
            if (StatManger.Instance.eStackEnabled)
            {
                if (StatManger.Instance.eCurrentStack <= 0)
                    return;
                StatManger.Instance.eCurrentStack--;
                if (skillCooldownManager != null)
                {
                    skillCooldownManager.ResetStackTimer(KeyCode.E);
                }
            }
            lastSkillUseTime[KeyCode.E] = Time.time;
            charAnimator.SetInteger("Attack_num", 3);
            shouldMove = false;
            sm.SetState(dicState[CharState.Attack]);
            LookAtBoss();
            characterSetting.UseMp(10.0f);
            isAttacking = true;
        }
        else if (Input.GetKeyDown(KeyCode.R) && CanUseSkill(KeyCode.R))
        {
            lastSkillUseTime[KeyCode.R] = Time.time;
            charAnimator.SetInteger("Attack_num", 4);
            shouldMove = false;
            sm.SetState(dicState[CharState.Attack]);
            LookAtBoss();
            characterSetting.UseMp(10.0f);
            isAttacking = true;
            if (StatManger.Instance.rMpUp)
                UpgradeSkillFunction.Instance.Choice13_2();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            if (characterSetting.posionCount <= 0)
                return;
            characterSetting.healOn = true;
            characterAnimationEvent.PotionHeal();
            characterSetting.posionCount--;
            characterSetting.posionCountText.text = characterSetting.posionCount.ToString();
        }

    }
    public Dictionary<KeyCode, float> GetSkillCooldowns()
    {
        return skillCooldowns;
    }

    public Dictionary<KeyCode, float> GetLastSkillUseTimes()
    {
        return lastSkillUseTime;
    }

    private void PlayerAttack()
    {
        if (isAttacking == true)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                destinationPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                currentTargetPosition = hit.point;
                // 캐릭터를 즉시 클릭한 위치로 회전
                Quaternion targetRotation = Quaternion.LookRotation(destinationPoint - transform.position);
                transform.rotation = targetRotation;
            }
            charAnimator.SetInteger("Attack_num", 0);
            shouldMove = false;
            sm.SetState(dicState[CharState.Attack]);
            isAttacking = true;
            if (StatManger.Instance.aaMpUp)
                UpgradeSkillFunction.Instance.Choice11_1();
        }
        SetAttackNum();

    }
    private void PlayerMove()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, clickLayer))
            {
                destinationPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                shouldMove = true;
                
                //click 파티클 생성
                Vector3 point = hit.point;
                if(point.y<0)
                    point.y = 0.2f;
                Instantiate(clickParticlePrefab, point, Quaternion.identity);
            }
        }
    }
    private void PlayerMoveHandle()
    {
        if (characterCon.isGrounded)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        if (shouldMove && !isAttacking)
        {
            Vector3 direction = destinationPoint - transform.position;
            direction.y = 0;

            if (direction.magnitude > 0.1f)
            {
                sm.SetState(dicState[CharState.Move]);
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                moveDirection = direction.normalized * Speed;
            }
            else
            {
                moveDirection = Vector3.zero;
            }
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        // 최종 이동 벡터 = 평면 이동 + 중력 적용
        Vector3 finalMove = moveDirection + Vector3.up * verticalVelocity;
        characterCon.Move(finalMove * Time.deltaTime);

        if (shouldMove && Vector3.Distance(transform.position, destinationPoint) < 0.1f)
        {
            shouldMove = false;
            sm.SetState(dicState[CharState.Idle]);
        }
    }
    private void Update()
    {
        PlayerAttack();
        PlayerMove();
    }
    void FixedUpdate()
    {
        PlayerMoveHandle();
        float hp = this.GetComponent<Character>().GetHp();
        if (characterSetting.healOn == false && Attack05Particle.activeSelf == true)
        {
            Attack05Particle.SetActive(false);

        }
        if(hp<=0.0f)
        {
            if(!isDead)
            {
                isDead = true;
                charAnimator.SetTrigger("Die");
            }
            
        }
        sm.DoOperateUpdate();
    }
}

