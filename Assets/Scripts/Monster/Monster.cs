using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    StateMachine<Monster> _fsm;
    MonsterAttackState _attackState;
    MonsterIdleState _idleState;
    MonsterChaseState _chaseState;
    MonsterReturnState _returnState;
    public Animator _animator;
    public GameObject _target;
    public float speed = 10;
    public float triggerRange = 10.0f;
    public float attackRange = 2.0f;
    public Vector3 createPoint;
    public bool returnCheck;//return 중일때 다른 행동을 잠구기 위함 true : return이 완료됨/ false : return이 진행중임
    public float hp;
    private bool die;
    private float dieCount;//죽고 시간 체크
    private bool isDeadHandled = false; // Watching()에서 이미 처리했는지 확인하는 변수
   
    // Start is called before the first frame update
    void Awake()
    {
        _attackState = new MonsterAttackState(this);
        _idleState = new MonsterIdleState(this);
        _chaseState = new MonsterChaseState(this);
        _returnState = new MonsterReturnState(this);

        _animator = GetComponent<Animator>();

        _fsm = new StateMachine<Monster>(this, _idleState);
        _target = GameObject.FindWithTag("Player");
        createPoint = this.transform.position; 
        returnCheck = true;
        hp = 100.0f;
        die = false;
        dieCount = 3.0f;
        isDeadHandled = false;
    }
    public bool GetDie()
    {
        return die;
    }
    public bool GetIsDeadHandled()
    {
        return isDeadHandled;
    }
    public void SetIsDeadHandled(bool _isDeadHandled)
    {
        isDeadHandled = _isDeadHandled;
    }
    public void MoveChase()
    {
        if (_target == null) return;

        Vector3 direction = (_target.transform.position - transform.position).normalized;

        // 부드러운 회전 적용 (회전 속도는 5f, 필요하면 조정 가능)
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

        // 타겟과의 거리 계산
        float distance = Vector3.Distance(transform.position, _target.transform.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    public void MoveCreatePoint()
    {
        Vector3 direction = (createPoint - transform.position).normalized;

        // 부드러운 회전 적용 (회전 속도는 5f, 필요하면 조정 가능)
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        transform.position += direction * speed * Time.deltaTime;
    }
    void OnTriggerEnterCustom()
    {
        //Debug.Log("플레이어가 몬스터 범위 안에 들어옴!");
        if(_fsm.curState != _chaseState)
            _fsm.SetState(_chaseState);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!returnCheck)
            return;
        if(other.CompareTag("PlayerAttack"))
        {
            Debug.Log("데미지 입음");
        }
    }
    void OnTriggerExitCustom()
    {
        //Debug.Log("플레이어가 몬스터 범위를 벗어남!");
        if(_fsm.curState != _idleState)
            _fsm.SetState(_idleState);
    }
    public void MonsterInit()
    {
        hp = 100;
        _animator.SetTrigger("Alive");
        die = false;
        isDeadHandled = false;
    }
    private void OnEnable()
    {
        MonsterInit();
    }
    // Update is called once per frame
    void Update()
    {
        if (_target == null) return;  // 플레이어가 없으면 실행 X
        if (hp <= 0)
        {
            if (!die)
            {
                die = true;
                _animator.SetTrigger("Die");

                //StartCoroutine(SetOrigin());
            }
            dieCount -= Time.deltaTime;
            if (dieCount <= 0.0f)
            {
                this.gameObject.SetActive(false);
                isDeadHandled = true;
            }
        }
            
        float createDistance = Vector3.Distance(transform.position, createPoint);
        if(createDistance > 15.0f)
        {
            if(_fsm.curState != _returnState)
            _fsm.SetState(_returnState);
            returnCheck = false;
        }
        if (returnCheck)
            _fsm.SetState(_idleState);
        if(_fsm.curState !=_returnState)
        {
            float distance = Vector3.Distance(transform.position, _target.transform.position); // 거리 계산
            if (distance <= triggerRange)
            {
                if( distance <= attackRange)
                {
                    if(_fsm.curState != _attackState)
                        _fsm.SetState(_attackState);
                }
                else
                    OnTriggerEnterCustom();
            }
            else if (distance > triggerRange)
            {
                OnTriggerExitCustom();
            }
        }
        
    }

    private void FixedUpdate() // 0.02초마다 호출
    {
        // 상태기계머쉰의 UPdate함수를 매 프레임마다 호출
        _fsm.DoOperateUpdate();

    }
}
