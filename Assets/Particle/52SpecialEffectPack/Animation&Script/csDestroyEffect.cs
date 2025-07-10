using UnityEngine;
using System.Collections;

public class csDestroyEffect : MonoBehaviour {

    private ParticleSystem[] particleSystems;

    void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {
        foreach (var ps in particleSystems)
        {
            if (ps != null && ps.IsAlive())
                return;
        }

        Destroy(gameObject); 
    }
}
