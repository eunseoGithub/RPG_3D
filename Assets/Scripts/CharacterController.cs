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

    public void StartAttack()
    {
        isAttacking = true;
    }

    public void EndAttack()
    {
        sm.SetState(dicState[CharState.Idle]);
        isAttacking = false;
    }

    public void StartAnimation_CastSpell01()
    {
        StartAttack();
    }

    public void EndAnimation_CastSpell01()
    {
        EndAttack();
    }

    public void StartAnimation_Attack01()
    {
        StartAttack();
    }

    public void EndAnimation_Attack01()
    {
        EndAttack();
    }

    public void StartAnimation_CastSpell02()
    {
        StartAttack();
    }

    public void EndAnimation_CastSpell02()
    {
        EndAttack();
    }

    public void StartAnimation_MagicAreaAttack01()
    {
        StartAttack();
    }

    public void EndAnimation_MagicAreaAttack01()
    {
        EndAttack();
    }
    public void StartAnimation_MagicAreaAttack02()
    {
        StartAttack();
    }

    public void EndAnimation_MagicAreaAttack02()
    {
        EndAttack();
    }

    public void StartAnimation_MagicAttack01()
    {
        StartAttack();
    }

    public void EndAnimation_MagicAttack01()
    {
        EndAttack();
    }

    public void StartAnimation_MagicAttack02()
    {
        StartAttack();
    }

    public void EndAnimation_MagicdAttack02()
    {
        EndAttack();
    }

    public void StartAnimation_MagicAttack03()
    {
        StartAttack();
    }

    public void EndAnimation_MagicdAttack03()
    {
        EndAttack();
    }

    public void StartAnimation_MagicAttack04()
    {
        StartAttack();
    }

    public void EndAnimation_MagicdAttack04()
    {
        EndAttack();
    }


    void Update()
    {
        //attack
        if(Input.GetMouseButtonDown(0))
        {
            shouldMove = false;
            sm.SetState(dicState[CharState.Attack]);
        }
        if(Input.GetKey(KeyCode.Q)|| Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.R) ||
            Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.F))
        {
            shouldMove = false;
            sm.SetState(dicState[CharState.Attack]);
        }

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

        sm.DoOperateUpdate();
    }
}

