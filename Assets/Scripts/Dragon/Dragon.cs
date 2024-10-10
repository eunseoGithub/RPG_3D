using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    // Dragon의 fsm 상태 객체 저장용
    DragonAttackState _attackState;  //  공격상태관리
    DragonIdleState _idleState;     // Idle 상태 관리
    DragonChaseState _chaseState;   // 추적상태관리
    DragonMoveState _moveState;     // 이동 상태 관리
    DragonReturnBaseState _returnBaseState; // 귀환 상태 관리
    public SkillStates _currentState;
    public float _currentDistance;
    // 상태 컨트롤 객체
    StateMachine<Dragon> _fsm;
    public enum SkillStates
    {
        None,
        Near,
        Far,
    }
    public float speed = 3f;
    // 상태 처리용 값
    public float _attackNearRange = 5.0f;  // 근거리 공격 반경
    public float _attackFarRange = 10.0f;   // 원거리 공격 반경

    private float _chaseRange = 20.0f;   // 추적반경

    private float _detectingRange = 200.0f;   // 타겟 감지 반경

    private float _moveSpeed = 3.0f;        // 이동 속도
    
    private float _returnDistance = 100.0f;

    public Transform _targetTr = null;     // 공격 타겟 지정

    private Vector3 _returnPosition = Vector3.zero; // 귀환 위치

    public Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        // 상태 객체 생성
        _attackState = new DragonAttackState(this);
        _idleState = new DragonIdleState(this);
        _chaseState = new DragonChaseState(this);
        _moveState = new DragonMoveState(this);
        _returnBaseState = new DragonReturnBaseState(this);

        // 상태 관리 객체 생성
        _fsm = new StateMachine<Dragon>(this, _idleState); //  초기 상태를 Idle 상태로 설정한다.

        // 귀환 위치 저장
        _returnPosition = this.transform.position;

        // 애니메이터컴포넌트를 가지고 온다.
        _animator = GetComponent<Animator>();
    }

    // 상태 변경함수
    public void ChangeIdleState()
	{
        _fsm.SetState(_idleState);
	}

    public void ChangeMoveState()
	{
        _fsm.SetState(_moveState);
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

    /// <summary>
    /// 랜덤으로 보스 공격 범위를 지정한다
    /// </summary>
    /// <returns></returns>
    public SkillStates RandomSkiilState()
    {
        SkillStates randomSkill;
        int random = Random.Range(0, 2);
        if(random == 1)
        {
            randomSkill = SkillStates.Near;
            _animator.SetInteger("nearAttackSkill", Random.Range(0, 3));
        }
        else
        {
            randomSkill = SkillStates.Far;
            _animator.SetInteger("farAttackSkill", Random.Range(0, 4));
        }
        return randomSkill;
    }

    /// <summary>
    /// 감지 반경내에 들어온 타겟을 감지 한다.
    /// </summary>
    /// <returns></returns>
    public bool DetectingTarget()
	{
        // 감지 반경안에 들어온 오브젝트들의 콜라이더를 가져온다.
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, _detectingRange);

        foreach(var col in colliders)
		{
            // 반경안에 들어온 오브젝트의 collider.gameObject의 이름이 Magician_RIO 인지 확인한다.
			if (col.name.Contains("Magician_RIO"))
			{
                // 타겟 발견
                _targetTr = col.transform;  // 감지된 타겟을 targeting한다.
                return true;

			}
		}

        return false;   // 감지 반경내에 감지 target이 없음
	}

    /// <summary>
    /// 추적 반경안에 있는 체크
    /// </summary>
    /// <returns></returns>
    public bool CheckChaseRange()
    {
        float distance = Vector3.Distance(this.transform.position, _targetTr.position);

        if (_targetTr != null && Vector3.Distance(this.transform.position, _targetTr.position) <= _chaseRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 근거리 공격 반경안에 있는 체크
    /// </summary>
    /// <returns></returns>
    public bool CheckNearAttackRange()
	{
        if(_targetTr != null && Vector3.Distance(this.transform.position, _targetTr.position) <= _attackNearRange )
		{
            return true;
		}
		else
		{
            return false;
		}
	}

    /// <summary>
    /// 원거리 공격 반경안에 있는지 체크
    /// </summary>
    /// <returns></returns>
    public bool CheckFarAttackRange()
	{
        if (_targetTr != null && Vector3.Distance(this.transform.position, _targetTr.position) <= _attackFarRange
            && Vector3.Distance(this.transform.position, _targetTr.position) > _attackNearRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 타겟을 추적한다.
    /// </summary>
    public void MoveChase()
	{
        if(_targetTr != null)
        {
            Vector3 direct = (_targetTr.position - this.transform.position).normalized;
            this.transform.rotation = Quaternion.LookRotation(direct);
            if (Vector3.Distance(transform.position, _targetTr.position) > _currentDistance)
                transform.position = Vector3.MoveTowards(transform.position, _targetTr.position, speed * Time.deltaTime);     
            else
            {
                ChangeAttackState();
            }
        }
        /*if (_targetTr != null)
		{
            Vector3 direct = (_targetTr.position - this.transform.position).normalized; // 추적 방향벡터를 생성한다.

            this.transform.rotation = Quaternion.LookRotation(direct);  // 드래곤을 추적방향으로 회전 시킨다.
            this.transform.Translate(direct * _moveSpeed * Time.smoothDeltaTime, Space.World);
            
        }*/
	}

    /// <summary>
    /// 있던 위치로 귀환한다.
    /// </summary>
    public void MoveReturnBase()
	{
        if(_targetTr != null)
		{
            Vector3 direct = (_returnPosition - this.transform.position).normalized;
            this.transform.rotation = Quaternion.LookRotation(direct);  // 드래곤을 추적방향으로 회전.
            this.transform.Translate(direct * _moveSpeed * Time.smoothDeltaTime, Space.World);
		}
	}

    /// <summary>
    /// 귀환 해야 하는지 체크
    /// </summary>
    /// <returns></returns>
    public bool CheckReturnBase()
	{
        float ReturnDistance = (_returnPosition - this.transform.position).magnitude;

        if(ReturnDistance >= _returnDistance)
		{
            return true;
		}else
		{
            return false;
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate() // 0.02초마다 호출
	{
        // 상태기계머쉰의 UPdate함수를 매 프레임마다 호출
        _fsm.DoOperateUpdate();

    }
}
