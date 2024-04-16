using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ObjectPool))]
public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float spawnTime=7f;
    [SerializeField]
    private float spawnDistance = 5f;
    public float SpawnDistance => spawnDistance;

    private ObjectPool gameObjectPool;

    private float spawnTimer;
    private void Awake()
    {
        spawnTimer = 0;
        gameObjectPool = GetComponent<ObjectPool>();
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer>spawnTime)
        {
            spawnTimer = 0;
            if (gameObjectPool.AvailableObjectCount>0)
            {
                gameObjectPool.EnableObject(SetNewSpawnPosition());
            }

        }
    }
    private Vector3 SetNewSpawnPosition()
    {
        float angle = Random.Range(1, 360);
        float radians = angle * Mathf.Deg2Rad;
        Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(radians) * spawnDistance, 1, Mathf.Sin(radians) * spawnDistance);
        //return new Vector3(spawnDistance, 1, 0);
        return spawnPosition;
    }
}
