using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float Speed = 4.0f;
    public float rotateSpeed = 10.0f;
    public Animator charAnimator;
    private Vector3 destinationPoint;
    private bool shouldMove = false;
    public bool isAttacking = false;
    public GameObject Attack05Particle;
    public Vector3 currentTargetPosition;
    public GameObject Boss;

    private Dictionary<KeyCode, float> skillCooldowns = new Dictionary<KeyCode, float>();
    private Dictionary<KeyCode, float> lastSkillUseTime = new Dictionary<KeyCode, float>();

    public float Attack01CoolDown = 2.0f;
    public float Attack02CoolDown = 3.0f;
    public float Attack03CoolDown = 4.0f;
    public float Attack04CoolDown = 5.0f;
    public Character characterSetting;
    CharacterController characterCon;
    private Vector3 moveDirection;
    private LayerMask clickLayer;
    private float verticalVelocity;
    public float gravity = -9.81f;
    private bool isDead;


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

        skillCooldowns[KeyCode.Q] = Attack01CoolDown;
        skillCooldowns[KeyCode.W] = Attack02CoolDown;
        skillCooldowns[KeyCode.E] = Attack03CoolDown;
        skillCooldowns[KeyCode.R] = Attack04CoolDown;

        lastSkillUseTime[KeyCode.Q] = -Attack01CoolDown;
        lastSkillUseTime[KeyCode.W] = -Attack02CoolDown;
        lastSkillUseTime[KeyCode.E] = -Attack03CoolDown;
        lastSkillUseTime[KeyCode.R] = -Attack04CoolDown;
        isDead = false;

        characterCon = GetComponent<CharacterController>();
        clickLayer = LayerMask.GetMask("Terrains");

    }

    public void ChangeToIdleState()
    {
        sm.SetState(dicState[CharState.Idle]);
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
        return Time.time >= lastSkillUseTime[key] + skillCooldowns[key];
    }
    public void SetAttackNum()
    {
        if (isAttacking == true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q) && CanUseSkill(KeyCode.Q))
        {
            lastSkillUseTime[KeyCode.Q] = Time.time;
            charAnimator.SetInteger("Attack_num", 1);
            shouldMove = false;
            sm.SetState(dicState[CharState.Attack]);
            LookAtBoss();
            characterSetting.UseMp(10.0f);
        }
        else if (Input.GetKeyDown(KeyCode.W) && CanUseSkill(KeyCode.W))
        {
            lastSkillUseTime[KeyCode.W] = Time.time;
            charAnimator.SetInteger("Attack_num", 2);
            shouldMove = false;
            sm.SetState(dicState[CharState.Attack]);
            LookAtBoss();
            characterSetting.UseMp(10.0f);
        }
        else if (Input.GetKeyDown(KeyCode.E) && CanUseSkill(KeyCode.E))
        {
            lastSkillUseTime[KeyCode.E] = Time.time;
            charAnimator.SetInteger("Attack_num", 3);
            shouldMove = false;
            sm.SetState(dicState[CharState.Attack]);
            LookAtBoss();
            characterSetting.UseMp(10.0f);
        }
        else if (Input.GetKeyDown(KeyCode.R) && CanUseSkill(KeyCode.R))
        {
            lastSkillUseTime[KeyCode.R] = Time.time;
            charAnimator.SetInteger("Attack_num", 4);
            shouldMove = false;
            sm.SetState(dicState[CharState.Attack]);
            LookAtBoss();
            characterSetting.UseMp(10.0f);
            characterSetting.healOn = true;
        }
        else
        {

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
            //FireAtMousePosition();
        }
        SetAttackNum();
    }
    private void PlayerMove()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1f);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, clickLayer))
            {
                //Debug.Log($"Clicked: {hit.collider.name}");
                destinationPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                shouldMove = true;
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

    void Update()
    {
        PlayerAttack();
        PlayerMove();
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

