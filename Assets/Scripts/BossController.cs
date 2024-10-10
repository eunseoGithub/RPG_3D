using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    /*
    public Transform player;
    public int speed = 3;
    private int countPattern = 0;
    private Animator bossAnimator;

    public float inAreaDistance;
    public float outAreaDistance;
    public bool isMoving;
    public bool isAttacking;
    //public SkillStates currentSkillState;
    public float currentDistance;
    public enum SkillStates
    {
        none,
        InArea,
        OutArea,
    }

    IEnumerator LookAtPlayer()
    {
        if (player != null )
        {
            Vector3 dir = player.transform.position - transform.position;

            while (Vector3.Angle(transform.forward, dir) > 1f) 
            {
                this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * speed);

                dir = player.transform.position - transform.position;

                yield return new WaitForSeconds(1f);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bossAnimator = GetComponent<Animator>();
        inAreaDistance = 50.0f;
        outAreaDistance = 100.0f;
        //currentSkillState = SkillStates.none;
        isMoving = false;
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            isArea = true;
            bossAnimator.SetBool("IsInArea", true);
            bossAnimator.SetFloat("CountTime", 0.0f);
            Debug.Log("In Trigger");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            isArea = false;
            bossAnimator.SetBool("IsInArea", false);
            Debug.Log("Out Trigger");
        }
    }

    private float MeasureDistane(Vector3 playerPos,Vector3 bossPos)
    {
        float distance = (playerPos - bossPos).magnitude;
        return distance;
    }

    private SkillStates SetSkillStateRandom()
    {
        SkillStates state;
        state = SkillStates.InArea;
        int result = Random.Range(0, 2);
        if(result == 1)
        {
            state = SkillStates.InArea;
            currentDistance = inAreaDistance;
            Debug.Log("Set In");
        }
        else
        {
            state = SkillStates.OutArea;
            currentDistance = outAreaDistance;
            Debug.Log("Set Out");
        }
        return state; 
    }

    IEnumerator Chase(float distance)
    {
        while (Vector3.Distance(transform.position, player.position) > distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            yield return  null;
        }
        isMoving = false;
    }

    IEnumerator Retreat(float distance)
    {
        while (Vector3.Distance(transform.position, player.position) < distance)
        {
            Vector3 dir = (transform.position - player.position).normalized; 
            transform.position += dir * speed * Time.deltaTime;
            
            yield return null;
        }
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving && !isAttacking)
        {
            currentSkillState = SetSkillStateRandom();
            float measeDistance = MeasureDistane(player.position, this.transform.position);
            if(currentDistance>measeDistance)//chase
            {
                bossAnimator.SetBool("move", true);
                bossAnimator.SetFloat("moveDistance", measeDistance - currentDistance);
                isMoving = true;
                StartCoroutine("LookAtPlayer");
                StartCoroutine(Chase(currentDistance));
            }
            else//retreat
            {
                bossAnimator.SetBool("move", true);
                bossAnimator.SetFloat("moveDistance", currentDistance - measeDistance);
                isMoving = true;
                StartCoroutine(Retreat(currentDistance));
            }
        }
    }*/
}
    