using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack03SkillTrigger : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public LayerMask collisionLayer;

    private ParticleSystem.Particle[] particles;

    void Update()
    {
        if (particleSystem == null) return;

        // 파티클 데이터 가져오기
        int particleCount = particleSystem.particleCount;
        if (particles == null || particles.Length < particleCount)
        {
            particles = new ParticleSystem.Particle[particleCount];
        }
        particleSystem.GetParticles(particles);

        for (int i = 0; i < particleCount; i++)
        {
            Vector3 particlePosition = particles[i].position;

            // 충돌 감지 (Trigger 포함)
            Collider[] hits = Physics.OverlapSphere(particlePosition, 0.5f, collisionLayer, QueryTriggerInteraction.Collide);
            foreach (var hit in hits)
            {
                //Debug.Log($"Particle collided with: {hit.gameObject.name}");
            }
        }
    }
}
