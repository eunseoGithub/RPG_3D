using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove_ver2 : MonoBehaviour
{
    Animator anim;
    CharacterController characterCon;
    private Vector3 destinationPoint;
    private Vector3 moveDirection;
    Vector3 currentTargetPosition;
    private bool shouldMove;
    public float Speed = 4.0f;
    public float rotateSpeed = 10.0f;
    public LayerMask clickLayer;
    void Start()
    {
        anim = GetComponent<Animator>();
        characterCon = GetComponent<CharacterController>();
        shouldMove = false;
        clickLayer = LayerMask.GetMask("Terrains");
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1f);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, clickLayer))
            {
                Debug.Log($"Clicked: {hit.collider.name}");
                destinationPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                shouldMove = true;
            }
        }
    }

    void HandleMovement()
    {
        if (shouldMove)
        {
            Vector3 direction = destinationPoint - transform.position;
            direction.y = 0;

            if (direction.magnitude > 0.1f)
            {
                // 회전
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

                // 이동
                moveDirection = direction.normalized * Speed;
                characterCon.Move(moveDirection * Time.deltaTime);
            }
            else
            {
                shouldMove = false;
                //anim.SetTrigger("Idle"); // 필요시 애니메이션 전환
            }
        }
    }
}
