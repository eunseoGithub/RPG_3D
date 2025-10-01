using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Dragon
 * 보스 메인 클래스(FSM 기반 상태 관리 및 행동 처리)
 * 기능 요약 : 
 * - FSM 상태 관리 : Idle, Chase, Attack, ReturnBase, NearAttack, FarAttack
 * - Hp 관리 : GetDamage(), UpdateHpBar(), Hp 바 UI 업데이트
 * - 공격/추적 범위 확인 : CheckNearAttackRange(), CheckFarAttackRange(), CheckChaseRange()
 * - 타겟 감지 : DetectingTarget()
 * - 공격 및 상태 전환 : ChangeIdleState(), ChangeAttackState(), ChangeReturnBaseState(), 
 *                                ChangeNearAttackState(), ChangeNearFarState()
 * - FixedUpdate() : FSM 업데이트 호출
 * - Update() : 사망 처리
 */
public class Dragon : MonoBehaviour
{
    // Dragon의 fsm 상태 객체 저장용
    DragonAttackState _attackState;                       //공격상태관리
    DragonIdleState _idleState;                               //Idle 상태 관리
    DragonChaseState _chaseState;                        //추적상태관리
    DragonReturnBaseState _returnBaseState;     //귀환 상태 관리
    DragonNearAttackState _nearAttackState;     //근거리 공격 상태 관리
    DragonFarAttackState _farAttackState;          //원거리 공격 상태 관리

    public SkillStates _currentState;
    public float _currentDistance;

    StateMachine<Dragon> _fsm;
    [SerializeField]
    private float hp;
    public Image hpBarImage;
    public Text hpText;
    private bool isDead;
    public enum SkillStates
    {
        None,
        Near,
        Far,
    }
    public enum NearSkills
    {
        FlameAttack,
        ClawAttack,
        Defend,
    }

    public enum FarSkills
    {
        BasicAttack,
        Scream,
        Sleep,
        TakeOff,
    }

    public enum BossState
    {
        None,
        Idle,
        Chase,
        Retreat,
        ReturnBase,
        Attack,
    }

    public float speed = 3f;
    // 상태 처리용 값
    public float _attackNearRange = 10.0f;  // 근거리 공격 반경
    public float _attackFarRange = 20.0f;   // 원거리 공격 반경

    private float _chaseRange = 40.0f;   // 추적반경

    private float _detectingRange = 200.0f;   // 타겟 감지 반경

    private float _moveSpeed = 3.0f;        // 이동 속도

    private float _returnDistance = 100.0f;  // 귀환 위치 max 거리

    public Transform _targetTr = null;     // 공격 타겟 지정

    private Vector3 _returnPosition = Vector3.zero; // 귀환 위치

    public Animator _animator;

    public HashSet<string> AttackIds = new HashSet<string>();
    public GameObject damagePopupPrefeb;
    public Transform popupSpawnPoint;
    private Canvas monsterCanvas;
    // Start is called before the first frame update
    void Start()
    {
        hp = 500.0f;
        // 상태 객체 생성
        _attackState = new DragonAttackState(this);
        _idleState = new DragonIdleState(this);
        _chaseState = new DragonChaseState(this);
        _returnBaseState = new DragonReturnBaseState(this);
        _nearAttackState = new DragonNearAttackState(this);
        _farAttackState = new DragonFarAttackState(this);

        // 상태 관리 객체 생성
        _fsm = new StateMachine<Dragon>(this, _idleState); //  초기 상태를 Idle 상태로 설정한다.

        // 귀환 위치 저장
        _returnPosition = this.transform.position;

        // 애니메이터컴포넌트를 가지고 온다.
        _animator = GetComponent<Animator>();
        isDead = false;
        if (monsterCanvas == null)
        {
            monsterCanvas = GameObject.Find("DamageCanvas").GetComponent<Canvas>();
        }
    }
    public void GetDamage(float damage)
    {
        hp -= damage;
        GameObject popup = Instantiate(damagePopupPrefeb, monsterCanvas.transform);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(popupSpawnPoint.position);
        popup.transform.position = screenPos;
        popup.GetComponent<DamagePopup>().Setup(damage);
        UpdateHpBar();
    }
    public float GetHp()
    {
        return hp;
    }
    void UpdateHpBar()
    {
        if (hpBarImage != null && hpText != null)
        {
            hpBarImage.fillAmount = hp / 500.0f;
            hpText.text = $"HP : ({hp} / 500)";
        }
    }
    // 상태 변경함수
    public void ChangeIdleState()
    {
        _fsm.SetState(_idleState);
    }

    public void ChangeAttackState()
    {
        _fsm.SetState(_attackState);
    }

    public void ChangeChaseState()
    {
        _fsm.SetState(_chaseState);
    }

    public void ChangeReturnBaseState()
    {
        _fsm.SetState(_returnBaseState);
    }

    public void ChangeNearAttackState()
    {
        LookAtPlayer();
        _fsm.SetState(_nearAttackState);
    }
    public void ChangeFarAttackState()
    {
        LookAtPlayer();
        _fsm.SetState(_farAttackState);
    }

    public bool DetectingTarget()
    {
        // 감지 반경안에 들어온 오브젝트들의 콜라이더를 가져온다.
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, _detectingRange);

        foreach (var col in colliders)
        {
            // 반경안에 들어온 오브젝트의 collider.gameObject의 이름이 PolyArtWizardMaskTintMat 인지 확인한다.
            if (col.name.Contains("PolyArtWizardMaskTintMat"))
            {
                // 타겟 발견
                _targetTr = col.transform;  // 감지된 타겟을 targeting한다.
                return true;

            }
        }

        return false;   // 감지 반경내에 감지 target이 없음
    }

    public bool CheckChaseRange()
    {
        // 타겟과의 거리를 계산한다. 
        float distance = Vector3.Distance(this.transform.position, _targetTr.position);

        // 타겟과의 거리가 추적반경보다 같거나 작으면 추적을 시작한다.
        if (_targetTr != null && Vector3.Distance(this.transform.position, _targetTr.position) <= _chaseRange)
        {
            return true;
        }
        return false;
    }


    public bool CheckNearAttackRange()
    {

        if (_targetTr != null && Vector3.Distance(this.transform.position, _targetTr.position) <= _attackNearRange)
        {
            return true;
        }

        return false;
    }

    public bool CheckFarAttackRange()
    {
        if (Vector3.Distance(this.transform.position, _targetTr.position) <= _attackFarRange)
        {
            return true;
        }

        return false;
    }

    public void MoveChase()
    {
        if (_targetTr != null)
        {
            _animator.SetBool("chase", true);
            LookAtPlayer();
            if (Vector3.Distance(transform.position, _targetTr.position) > _currentDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetTr.position, speed * Time.deltaTime);
            }

        }
    }
    public void LookAtPlayer()
    {
        Vector3 direct = (_targetTr.position - this.transform.position).normalized;
        this.transform.rotation = Quaternion.LookRotation(direct);
    }
    public void MoveRetreat()
    {
        if (_targetTr != null)
        {
            // 타겟과 반대 방향으로 이동하도록 벡터 반전
            Vector3 retreatDirection = (this.transform.position - _targetTr.position).normalized;
            this.transform.rotation = Quaternion.LookRotation(retreatDirection);

            // 거리가 _currentDistance 이하일 때 후퇴
            if (Vector3.Distance(transform.position, _targetTr.position) <= _currentDistance)
            {
                transform.position += retreatDirection * speed * Time.deltaTime;
            }
            // 타겟을 다시 바라보게 
            else
            {
                Vector3 direct = (_targetTr.position - this.transform.position).normalized;
                this.transform.rotation = Quaternion.LookRotation(direct);
                ChangeAttackState();
            }
        }
    }

    void Update()
    {
        if(hp<=0.0f)
        {
            if (!isDead)
            {
                isDead = true;
                _animator.SetTrigger("Die");
            }
        }
    }

    private void FixedUpdate() // 0.02초마다 호출
    {
        // 상태기계머쉰의 UPdate함수를 매 프레임마다 호출
        _fsm.DoOperateUpdate();

    }
}
