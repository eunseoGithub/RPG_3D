using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public enum CharState
    {
        Idle,
        Move,
    }

    public float maxSpeed = 2.0f;
    public float turnDistance = 2.0f;

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
        //IState<CharacterController> turn = new CharacterTurn();

        dicState.Add(CharState.Idle, idle);
        dicState.Add(CharState.Move, move);
        //dicState.Add(BikeState.Turn, turn);

        sm = new StateMachine<CharacterController>(this, dicState[CharState.Idle]);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            sm.SetState(dicState[CharState.Move]);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            CurrentTurnDirection = Direction.Right;
            //sm.SetState(dicState[CharState.Turn]);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            CurrentTurnDirection = Direction.Left;
            //sm.SetState(dicState[BikeState.Turn]);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sm.SetState(dicState[CharState.Idle]);
        }

        sm.DoOperateUpdate();
    }
}

