using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHpBar : MonoBehaviour
{
    private Camera uiCam;
    private Canvas canvas;
    private RectTransform recParent;
    private RectTransform rectHp;

    public Vector3 offset = Vector3.zero;
    public Transform enemyTr;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        uiCam = canvas.worldCamera;
        recParent = canvas.GetComponent<RectTransform>();
        rectHp = this.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTr == null)
            return;
        var screenPos = Camera.main.WorldToScreenPoint(enemyTr.position + offset);
        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(recParent, screenPos, uiCam, out localPos);
        rectHp.localPosition = localPos;
    }
}
