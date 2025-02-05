using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float Speed = 4.0f;     
    public float rotateSpeed = 10.0f;
    public Animator charAnimator;
    private Vector3 destinationPoint;
    private bool shouldMove = false;
    private bool isAttacking = false;
    public Transform firePoint;
    public GameObject Attack01Prefab;
    public GameObject Attack02Prefab;
    public GameObject Attack03Prefab;
    public GameObject Attack04Prefab;
    public GameObject Attack05Particle;
    Vector3 currentTargetPosition;
    public GameObject Boss;
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

    private Dictionary<CharState, IState<CharacterController>> dicState = new Dictionary<CharState, IState<CharacterController>>();
    private StateMachine<CharacterController> sm;

    // Start is called before the first frame update
    void Start()
    {
        IState<CharacterController> idle = new CharacterIdle();
        IState<CharacterController> move = new CharacterMove();
        IState<CharacterController> attack = new CharacterAttack();

        dicState.Add(CharState.Idle, idle);
        dicState.Add(CharState.Move, move);
        dicState.Add(CharState.Attack, attack);

        sm = new StateMachine<CharacterController>(this, dicState[CharState.Idle]);

        charAnimator = GetComponent<Animator>();
    }
    //animation event
    public void StartAttack()
    {
        isAttacking = true;
    }

    public void EndAttack()
    {
        sm.SetState(dicState[CharState.Idle]);
        isAttacking = false;
    }

    public void Attack01_Start()
    {
        StartAttack();
    }
    public void Attack01_End()
    {
        EndAttack();
    }

    public void Attack02_Start()
    {
        StartAttack();
    }
    public void Attack02_End()
    {
        EndAttack();
    }

    public void Attack03_Start()
    {
        StartAttack();
    }
    public void Attack03_End()
    {
        EndAttack();
    }

    public void Attack04_Start()
    {
        StartAttack();
    }
    public void Attack04_End()
    {
        EndAttack();
    }

    public void Defend_Start()
    {
        StartAttack();
    }
    public void Defend_End()
    {
        EndAttack();
    }

    public void Attack01_Fire()
    {
        FireAtMousePosition_Attack01();
    }
    public void Attack02_Fire()
    {
        FireAtMousePosition_Attack02();
    }
    public void Attack03_Fire()
    {
        FireAtMousePosition_Attack03();
    }
    public void Attack04_Fire()
    {
        FireAtMousePosition_Attack04();
    }
    public void Attack05_Fire()
    {
        Attack05Particle.SetActive(true);
    }
    void FireAtMousePosition_Attack01()
    {
        Vector3 direction = (currentTargetPosition - firePoint.position).normalized;

        GameObject fireball = Instantiate(Attack01Prefab, firePoint.position, Quaternion.identity);
        fireball.GetComponent<Attack01Skill>().Launch(direction);
    }
    void FireAtMousePosition_Attack02()
    {
        Vector3 direction = (currentTargetPosition - firePoint.position).normalized;

        GameObject fireball = Instantiate(Attack02Prefab, firePoint.position, Quaternion.LookRotation(-direction));
        fireball.GetComponent<Attack02Skill>().Launch(direction);
    }

    void FireAtMousePosition_Attack03()
    {
        Vector3 direction = new Vector3(currentTargetPosition.x, 1.0f, currentTargetPosition.z);
        GameObject fire = Instantiate(Attack03Prefab, direction, Quaternion.identity);
        float distanceToBoss = Vector3.Distance(fire.transform.position, Boss.transform.position);
        if (distanceToBoss < 5.0f) // 거리 1.5 이하이면 히트 판정
        {
            /*Boss bossComponent = boss.GetComponent<Boss>();
            if (bossComponent != null)
            {
                bossComponent.TakeDamage(10); // 보스에게 10의 데미지
            }*/
            Debug.Log("보스 데미지 입음");
        }
    }
    void FireAtMousePosition_Attack04()
    {
        Vector3 direction = new Vector3(currentTargetPosition.x, 1.0f, currentTargetPosition.z);
        GameObject fire = Instantiate(Attack04Prefab, direction, Quaternion.identity);
        float distanceToBoss = Vector3.Distance(fire.transform.position, Boss.transform.position);
        if (distanceToBoss < 5.0f) // 거리 1.5 이하이면 히트 판정
        {
            /*Boss bossComponent = boss.GetComponent<Boss>();
            if (bossComponent != null)
            {
                bossComponent.TakeDamage(10); // 보스에게 10의 데미지
            }*/
            Debug.Log("보스 데미지 입음 Attack 04");
        }
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
    public void SetAttackNum()
    {
        if(isAttacking==true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            charAnimator.SetInteger("Attack_num", 1);
            shouldMove = false;
            sm.SetState(dicState[CharState.Attack]);
            LookAtBoss();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            charAnimator.SetInteger("Attack_num", 2);
            shouldMove = false;
            sm.SetState(dicState[CharState.Attack]);
            LookAtBoss();
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            charAnimator.SetInteger("Attack_num", 3);
            shouldMove = false;
            sm.SetState(dicState[CharState.Attack]);
            LookAtBoss();
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            charAnimator.SetInteger("Attack_num", 4);
            shouldMove = false;
            sm.SetState(dicState[CharState.Attack]);
            LookAtBoss();
        }
        else
        {
            
        }
    }

    void Update()
    {
        //attack
        if(Input.GetMouseButtonDown(0))
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
        
        //move
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                destinationPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                shouldMove = true;
            }
        }

        if (shouldMove && !isAttacking)
        {
            sm.SetState(dicState[CharState.Move]);
            Quaternion targetRotation = Quaternion.LookRotation(destinationPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, destinationPoint, Speed * Time.deltaTime);

            if (transform.position == destinationPoint)
            {
                shouldMove = false;
                sm.SetState(dicState[CharState.Idle]);
            }
        }
        float hp =this.GetComponent<Character>().GetHp();
        if (hp >= 100)
        {
            Attack05Particle.SetActive(false);
            
        }
        sm.DoOperateUpdate();
    }
}

