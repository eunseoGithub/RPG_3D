using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOffAttack : MonoBehaviour
{
    public GameObject warningPrefeb;
    public GameObject explosionPrefeb;
    public GameObject coliderPrefeb;
    public Transform bossTransform;
    public float warningDuration = 0.5f;
    public float spacing = 4f;
    public int gridCount = 5;
    public float delayBetWeenSpawns = 0.3f;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    public LayerMask groundLayer;
    private void OnEnable()
    {
        StartCoroutine(SpawnGridExplosions());
    }

    private void OnDisable()
    {
        foreach(GameObject obj in spawnedObjects)
        {
            if (obj != null)
                Destroy(obj);
        }
        spawnedObjects.Clear();
    }
    
    private IEnumerator SpawnGridExplosions()
    {
        for(int i = 0; i< gridCount; i++)
        {
            Vector3 spawnPos = bossTransform.position + new Vector3(0, 0, i * spacing - 10f);
            Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
            yield return StartCoroutine(SpawnWarningAndExplosion(spawnPos, rotation));
        }

        for(int i = 0; i < gridCount; i++)
        {
            Vector3 spawnPos = bossTransform.position + new Vector3(i * spacing - 10f, 0, 0);
            Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
            yield return StartCoroutine(SpawnWarningAndExplosion(spawnPos, rotation));
        }
    }

    private IEnumerator SpawnWarningAndExplosion(Vector3 basePos, Quaternion rotation)
    {
        float yPos = GetGroundY(basePos);
        Vector3 warningPosition = new Vector3(basePos.x, yPos, basePos.z);

        GameObject warning = Instantiate(warningPrefeb, warningPosition, rotation);
        spawnedObjects.Add(warning);

        yield return new WaitForSeconds(warningDuration);

        Transform rightTransform = warning.transform.Find("Area/Right");
        Vector3 explosionPos = rightTransform != null ? rightTransform.position : warningPosition;

        GameObject explosion = Instantiate(explosionPrefeb, explosionPos, rotation);
        GameObject coliderObj = Instantiate(coliderPrefeb, explosionPos, rotation);

        spawnedObjects.Add(explosion);
        spawnedObjects.Add(coliderObj);

        spawnedObjects.Remove(warning);
        Destroy(warning);

        yield return new WaitForSeconds(delayBetWeenSpawns);
    }

    private float GetGroundY(Vector3 position)
    {
        Ray ray = new Ray(position + Vector3.up * 50f, Vector3.down);
        if(Physics.Raycast(ray, out RaycastHit hit,100f, groundLayer))
        {
            return hit.point.y - 0.8f;
        }
        return position.y;
    }
}

