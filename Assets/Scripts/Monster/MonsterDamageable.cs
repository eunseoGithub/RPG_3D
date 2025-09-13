using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
 * MonsterDamageable
 * 몬스터 또는 보스에게 적용되는 상태 이상 및 디버프 처리
 * TakeDamage() : 일반 데메지 처리
 * ApplySnare() : 이동 불가 상태 적용
 * ApplyDot() : 일정 시간 동안 지속 피해 적용
 * ApplySlow() : 이동 속도 감소 상태 적용
 * 면역 여부 설정/확인 기능 제공
 */
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
    public bool isBoss=false;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void TakeDamage(float amount)
    {
        if(isBoss)
        {
            Dragon boss = GetComponent<Dragon>();
            if (boss != null)
                boss.GetDamage(amount);
        }
        else
        {
            Monster mon = GetComponent<Monster>();
            if (mon != null)
                mon.GetDamage(amount);
        }
    }

    public void setImmuneSnare(bool result)
    {
        immuneToSnare = result;
    }
    public bool getImmuneSnare()
    {
        return immuneToSnare;
    }
    public void setImmuneDot(bool result)
    {
        immuneToDot = result;
    }
    public bool getImmuneDot()
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

        if (isBoss)
            StartCoroutine(SnareCoroutineBoss(duration));
        else
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

    IEnumerator SnareCoroutineBoss(float duration)
    {
        isSnared = true;
        Dragon boss = GetComponent<Dragon>();
        float originalSpeed = 0f;
        if(boss != null)
        {
            originalSpeed = boss.speed;
            boss.speed = 0f;
        }
        yield return new WaitForSeconds(duration);

        if (boss != null)
            boss.speed = originalSpeed;

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

        if(isBoss)
            slowCoroutine = StartCoroutine(SlowCoroutineBoss(slowAmount, duration));
        else
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
    IEnumerator SlowCoroutineBoss(float slowAmount, float duration)
    {
        Dragon boss = GetComponent<Dragon>();
        float originalSpeed = 0f;
        if(boss != null)
        {
            originalSpeed = boss.speed;
            boss.speed = boss.speed * (1f - slowAmount);
        }
        
        yield return new WaitForSeconds(duration);

        if (boss != null)
            boss.speed = originalSpeed;
        slowCoroutine = null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
