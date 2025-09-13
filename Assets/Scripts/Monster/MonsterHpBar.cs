using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * MonsterHpBar
 * 몬스터의 체력 바 위치를 화면상에 표시
 * World 위치를 Screen 좌표로 변환하여 UICanvas에 표시
 */
public class MonsterHpBar : MonoBehaviour
{
    RectTransform rectHp;

    public Vector3 offset = Vector3.zero;
    public Transform enemyTr;

    // Start is called before the first frame update
    void Start()
    {
        rectHp = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (enemyTr == null)
            return;
        
        var screenPos = Camera.main.WorldToScreenPoint(enemyTr.position + offset);
        rectHp.position = screenPos;
    }
}
