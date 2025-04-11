using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }

    public AudioClip playerAttack_leftclick;
    public AudioClip playerAttack_Q;
    public AudioClip playerAttack_W;
    public AudioClip playerAttack_E;
    public AudioClip playerAttack_R;

    public AudioClip catusAttack;
    public AudioClip mushroomAttack;

    public AudioClip BossSkill1;
    public AudioClip BossSkill2;
    public AudioClip BossSkill3;
    public AudioClip BossSkill4;

    AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f;
    }

    public void PlaySound(AudioClip audioClip)
    {
        if (audioClip == null)
        {
            return;
        }

        audioSource.PlayOneShot(audioClip);
    }
}
