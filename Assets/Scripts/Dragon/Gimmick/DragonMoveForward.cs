using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * DragonMoveForward
 * TimeLine에 있는 보스 전진 비행 처리
 * FlyForward() : 코루틴으로 일정 시간 동안 앞으로 이동
 * StartFlying() : 외부에서 전진 코루틴 시작
 */
public class DragonMoveForward : MonoBehaviour
{
    public float flyDuration = 3f;
    IEnumerator FlyForward()
    {
        float elapsed = 0f;
        while(elapsed <flyDuration )
        {
            transform.Translate(Vector3.forward * 5f * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    public void StartFlying()
    {
        StartCoroutine(FlyForward());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
