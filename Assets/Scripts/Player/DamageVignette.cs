using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
/*
 * DamageVignette
 * 플레이어 피격 시 화면에 붉은 vignette 효과를 적용
 */
public class DamageVignette : MonoBehaviour
{
    public PostProcessVolume volume;
    private Vignette vignette;

    public float damageIntensity = 0.3f;
    public float recoverySpeed = 1.5f;

    private void Start()
    {
        if(volume != null && volume.profile.TryGetSettings(out vignette))
        {
            vignette.intensity.value = 0f;
        }
        else
        {
            Debug.LogError("Vignette를 찾을 수 없습니다.");
        }
    }
    public void TakeDamage()
    {
        if(vignette != null)
        {
            vignette.color.value = Color.red;
            vignette.intensity.value = damageIntensity;
        }
    }
    private void Update()
    {
        if(vignette != null && vignette.intensity.value>0f)
        {
            vignette.intensity.value = Mathf.MoveTowards(vignette.intensity.value, 0f, recoverySpeed * Time.deltaTime);
        }
    }

}
