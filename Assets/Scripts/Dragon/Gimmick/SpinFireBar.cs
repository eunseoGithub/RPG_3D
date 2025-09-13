using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * SpinFireBar
 * 회전 화염 막대 보스 기믹 처리
 * CreateRotateBar() : 중심 기준으로 4개의 화염 막대 생성
 * Update() : 막대 생성 후 회전
 */
public class SpinFireBar : MonoBehaviour
{
    public GameObject fireBarPrefab;
    public float radius = 1.5f;
    public float rotationSpeed = 30f;
    public Transform Center;
    public bool rotateOn;
    public bool barOn;
    // Start is called before the first frame update
    void Start()
    {
        rotateOn = false;
        barOn = false;
        //CreateRotateBar();
    }
    void CreateRotateBar()
    {
        Vector3 center = transform.position;
        for(int i = 0; i<4;i++)
        {
            float angleDeg = i * 90f;
            float angleRad = angleDeg * Mathf.Deg2Rad;

            Vector3 offset = new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad)) * radius;
            Vector3 barWorldPos = center + offset;

            GameObject barParent = new GameObject("Bar_" + i);
            barParent.transform.position = barWorldPos;

            barParent.transform.LookAt(center);
            barParent.transform.Rotate(0, 180, 0);

            GameObject bar = Instantiate(fireBarPrefab, barParent.transform);

            bar.transform.localPosition = Vector3.zero;

            barParent.transform.SetParent(this.transform);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        if(rotateOn)
        {
            if (!barOn)
            {
                CreateRotateBar();
                barOn = true;
            }
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
            
    }
}
