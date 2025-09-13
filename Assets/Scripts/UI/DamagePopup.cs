using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * DamagePopup
 * 데미지 표시 팝업을 관리
 * 기능 요약 :
 * - 공격 시 발생한 데미지를 텍스트로 화면에 표시
 * - 팝업이 위로 이동하며 서서히 투명해지는 효과 적용
 * - 투명도가 0이면 자동으로 게임오브젝트 제거
 */
public class DamagePopup : MonoBehaviour
{
    public Text damageText;
    public float moveSpeed = 1f;
    public float fadeSpeed = 2f;

    private Color textColor;
    public void Setup(float damage)
    {
        damageText.text = damage.ToString();
        textColor = damageText.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        textColor.a -= fadeSpeed * Time.deltaTime;
        damageText.color = textColor;

        if(textColor.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
