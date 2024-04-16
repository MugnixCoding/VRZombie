using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class CircularTerrianGenerator : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] bool LookAtCenter = true;
    [SerializeField] GenerateGameObject[] generateList;
    public void Generate()
    {
        Clear();
        Transform generateParent;
        if (parent!=null)
        {
            generateParent = parent;
        }
        else
        {
            generateParent = transform;
        }
        for (int i = 0; i < generateList.Length;i++)
        {
            for (int j = 0; j < generateList[i].generateNumber;j++)
            {
                Vector3 circular = RandomAngularPosition();
                float distance = Random.Range(generateList[i].MinDistance, generateList[i].MaxDistance);
                Vector3 randomPosition = circular * distance;
                randomPosition.y = 0;
                GameObject newGameObject = Instantiate(generateList[i].prefabs[Random.Range(0, generateList[i].prefabs.Length)], randomPosition, Quaternion.identity, generateParent);
                newGameObject.layer = generateParent.gameObject.layer;
                if (LookAtCenter)
                {
                    newGameObject.transform.LookAt(generateParent);
                }
            }
        }
    } 
    public void Clear()
    {
        Transform generateParent;
        if (parent != null)
        {
            generateParent = parent;
        }
        else
        {
            generateParent = transform;
        }
        while (generateParent.childCount>0)
        {
            foreach (Transform child in generateParent)
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }
    private Vector3 RandomAngularPosition()
    {
        float angle = Random.Range(1, 360);
        Vector3 spawnPosition = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        return spawnPosition;
    }
}

[Serializable]
public class GenerateGameObject
{
    public GameObject[] prefabs;
    public float MinDistance;
    public float MaxDistance;
    public int generateNumber;
    
}
