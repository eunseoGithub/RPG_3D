using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.AI;
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
    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(-0.5f, 2.4f, 0);
    private Canvas monsterCanvas;
    private Image hpBarImage;
    private LogManager logManager;
    private Character player;
    private float exp;
    public static event Action<Monster> OnMonsterDeath;
    public GameObject MonsterAttackColider;
    public NavMeshAgent agent;
    public float returnDistance = 15f;
    private MonsterDamageable damageable;
    public GameObject potionPrefab;
    public GameObject damagePopupPrefeb;
    public Transform popupSpawnPoint;

    // Start is called before the first frame update
    void Awake()
    {
        logManager = LogManager.Instance;
        player = Character.Instance;
        _attackState = new MonsterAttackState(this);
        _idleState = new MonsterIdleState(this);
        _chaseState = new MonsterChaseState(this);
        _returnState = new MonsterReturnState(this);
        _animator = GetComponent<Animator>();

        _fsm = new StateMachine<Monster>(this, _idleState);
        _target = GameObject.FindWithTag("Player");
        createPoint = this.transform.position;
        returnCheck = false;
        hp = 100.0f;
        exp = 100.0f;
        die = false;
        dieCount = 3.0f;
        isDeadHandled = false;

        if (monsterCanvas == null)
        {
            monsterCanvas = GameObject.Find("MonsterHpCanvas").GetComponent<Canvas>();
        }
        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, monsterCanvas.transform);

        MonsterHpBar _hpbar = hpBar.GetComponent<MonsterHpBar>();
        _hpbar.enemyTr = this.gameObject.transform;
        _hpbar.offset = hpBarOffset;
        hpBarImage = hpBar.GetComponent<Image>();
        agent = GetComponent<NavMeshAgent>();
        damageable = GetComponent<MonsterDamageable>();
    }

    void OnDestroy()
    {
        Destroy(hpBarImage);
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
        agent.SetDestination(_target.transform.position);
    }
    public void MoveCreatePoint()
    {
        agent.SetDestination(createPoint);
    }
    void PlayerDetectOn()
    {
        if (_fsm.curState != _chaseState)
        {
            _fsm.SetState(_chaseState);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_fsm.curState == _returnState || _fsm.curState == _idleState )
            return;
        if (other.CompareTag("PlayerAttack"))
        {
            SkillDamage skill = other.GetComponent<SkillDamage>();
            Skill skilltype;
            if (skill != null)
            {
                GetDamage(skill.damage);
                skilltype = other.GetComponent<SkillDamage>().skill;
                if (skill.snare)
                {
                    switch (skilltype)
                    {
                        case Skill.Q:
                            int result = UnityEngine.Random.Range(0, 10);
                            if(result >0)
                            {
                                damageable.ApplySnare(StatManger.Instance.qSnareDuration);
                            }
                            break;
                        case Skill.W:
                            damageable.ApplySnare(StatManger.Instance.wSnareDuration);
                            break;
                    }
                    
                }
                if (skill.dot)
                {
                    switch (skilltype)
                    {
                        case Skill.W:
                            damageable.ApplyDot(StatManger.Instance.wDotDamage, StatManger.Instance.wDotInterval,
                        StatManger.Instance.wDotDuration);
                            break;
                        case Skill.E:
                            damageable.ApplyDot(StatManger.Instance.eDotDamage, StatManger.Instance.eDotInterval,
                        StatManger.Instance.eDotDuration);
                            break;
                    }
                }
                    
                if(skill.slow)
                {
                    switch (skilltype)
                    {
                        case Skill.Q:
                            damageable.ApplySlow(StatManger.Instance.qSlowAmount, StatManger.Instance.qSlowDuration);
                            break;
                        case Skill.W:
                            damageable.ApplySlow(StatManger.Instance.wSlowAmount, StatManger.Instance.wSlowDuration);
                            break;
                    }
                }
            }
        }
    }
    void PlayerDetectOff()
    {
        if (_fsm.curState != _idleState)
        {
            _fsm.SetState(_idleState);
            agent.ResetPath();
        }
    }
    public void MonsterInit()
    {
        hp = 100;
        _animator.SetTrigger("Alive");
        die = false;
        isDeadHandled = false;
        UpdateHpBar();
        returnCheck = false;
        this.GetComponent<CapsuleCollider>().enabled = true;
    }

    private void OnEnable()
    {
        MonsterInit();

        if (_target != null)
        {
            float distance = Vector3.Distance(transform.position, _target.transform.position);
            if (distance <= triggerRange && !_fsm.curState.Equals(_chaseState))
            {
                _fsm.SetState(_chaseState);
            }
        }
    }

    public void GetDamage(float damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        GameObject popup = Instantiate(damagePopupPrefeb, monsterCanvas.transform);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(popupSpawnPoint.position);
        popup.transform.position = screenPos;
        popup.GetComponent<DamagePopup>().Setup(damage);
        if (hp < 0)
            hp = 0;
        UpdateHpBar();
    }

    void UpdateHpBar()
    {
        if (hpBarImage != null)
        {
            hpBarImage.fillAmount = hp / 100.0f; // HP 비율 반영
        }
    }

    bool RandomItem()
    {
        bool result = false;

        int ran = UnityEngine.Random.Range(0, 100);
        if (ran < 10)
        {
            result = true;
        }
        return result;
    }
    //Monster Animation event Function
    void MushroomAttackSound()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.mushroomAttack);
    }
    void CatusAttackSound()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.catusAttack);
    }
    void AttackColiderOn()
    {
        MonsterAttackColider.SetActive(true);
    }
    void AttackColiderOff()
    {
        MonsterAttackColider.SetActive(false);
    }
    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(dieCount);
        this.gameObject.SetActive(false);
    }
    private void MonsterMovement()
    {
        if (die)
            return;
        float createDistance = Vector3.Distance(transform.position, createPoint);

        // 생성 위치에서 너무 멀어졌으면 복귀
        if (createDistance > returnDistance)
        {
            if (_fsm.curState != _returnState)
            {
                _fsm.SetState(_returnState);
            }
            returnCheck = false;
            return;
        }

        if (returnCheck)
        {
            _fsm.SetState(_idleState);
            return;
        }

        if (_fsm.curState != _returnState)
        {
            
            float distance = Vector3.Distance(transform.position, _target.transform.position);

            if (distance <= triggerRange)
            {
                if (distance <= attackRange)
                {
                    if (_fsm.curState != _attackState)
                    {
                        _fsm.SetState(_attackState);
                        agent.ResetPath(); // 공격 중엔 멈춤
                    }
                }
                else
                {
                    PlayerDetectOn(); // 추적
                }
            }
            else
            {
                PlayerDetectOff(); // 플레이어 사라짐
            }
        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_target == null) return;  // 플레이어가 없으면 실행 X
        //hp가 0이되면 하는 작업의 과정
        if (hp <= 0)
        {

            if (!die)
            {
                die = true;
                agent.ResetPath();
                _animator.SetTrigger("Die");
                this.GetComponent<CapsuleCollider>().enabled = false;
                logManager.AddLog("경험치 " + exp + "를 획득하셨습니다.");
                float addExp = player.GetExp();
                player.SetExp(addExp + exp);
                player.UpdateExp();
                if (RandomItem())
                {
                    logManager.AddLog("Key를 획득하셨습니다.");
                    Character.Instance.key = true;
                }
                if(RandomItem())
                {
                    if(potionPrefab != null)
                    {
                        Vector3 dropPosition = transform.position + Vector3.up * 0.5f;
                        Instantiate(potionPrefab, dropPosition, Quaternion.identity);
                        logManager.AddLog("체력 포션을 획득하셨습니다.");
                    }
                }
                OnMonsterDeath?.Invoke(this);
                StartCoroutine(HandleDeath());
            }

        }
        MonsterMovement();
        
        if (!die)
            _fsm.DoOperateUpdate();
    }

}
