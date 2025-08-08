using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MonsterDamageable : MonoBehaviour
{
    //속박이나 도트데미지를 받을 객체인지 체크
    [SerializeField]
    private bool immuneToSnare = false;
    [SerializeField]
    private bool immuneToDot = false;
    [SerializeField]
    private bool immuneToSlow = false;

    private bool isSnared = false;
    private Coroutine dotCoroutine;
    private Coroutine slowCoroutine;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    public void TakeDamage(float amount)
    {
        this.GetComponent<Monster>().GetDamage(amount);
    }
    public void setSnare(bool result)
    {
        immuneToSnare = result;
    }
    public bool getSnare()
    {
        return immuneToSnare;
    }
    public void setDot(bool result)
    {
        immuneToDot = result;
    }
    public bool getDot()
    {
        return immuneToDot;
    }
    public void setSlow(bool result)
    {
        immuneToSlow = result;
    }
    public bool getSlow()
    {
        return immuneToSlow;
    }

    public void ApplySnare(float duration)
    {
        if (immuneToSnare || isSnared)
            return;
        StartCoroutine(SnareCoroutine(duration));
    }

    IEnumerator SnareCoroutine(float duration)
    {
        isSnared = true;
        var agent = GetComponent<NavMeshAgent>();
        if (agent)
            agent.isStopped = true;
        yield return new WaitForSeconds(duration);

        if (agent)
            agent.isStopped = false;
        isSnared = false;
    }

    public void ApplyDot(float tick,float interval, float duration)
    {
        if (immuneToDot || dotCoroutine != null)
            return;
        dotCoroutine = StartCoroutine(DotCoroutine(tick, interval, duration));
    }

    IEnumerator DotCoroutine(float damage, float interval, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }
        dotCoroutine = null;
    }

    public void ApplySlow(float slowAmount, float duration)
    {
        if (immuneToSlow || slowCoroutine != null)
            return;

        slowCoroutine = StartCoroutine(SlowCoroutine(slowAmount, duration));
    }

    IEnumerator SlowCoroutine(float slowAmount, float duration)
    {
        var agent = GetComponent<NavMeshAgent>();
        float originalSpeed = 0f;
        if(agent)
        {
            originalSpeed = agent.speed;
            agent.speed = agent.speed * (1f - slowAmount);
        }

        yield return new WaitForSeconds(duration);

        if (agent)
            agent.speed = originalSpeed;

        slowCoroutine = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
