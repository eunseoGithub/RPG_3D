using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
