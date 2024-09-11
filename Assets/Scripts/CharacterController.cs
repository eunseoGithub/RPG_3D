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
    public enum CharState
    {
        Idle,
        Move,
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

        dicState.Add(CharState.Idle, idle);
        dicState.Add(CharState.Move, move);

        sm = new StateMachine<CharacterController>(this, dicState[CharState.Idle]);

        charAnimator = GetComponent<Animator>();
    }

    void Update()
    {
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

        if (shouldMove)
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

