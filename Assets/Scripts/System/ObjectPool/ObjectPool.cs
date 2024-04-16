using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private PoolObject prefab;
    [SerializeField]
    private int poolSize = 5;

    private HashSet<PoolObject> poolObject;//avoid the object that dont belong this pool being add in
    private Queue<PoolObject> availableObject;
    public int AvailableObjectCount => availableObject.Count;
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        availableObject = new Queue<PoolObject>();
        poolObject = new HashSet<PoolObject>();
        for (int i = 0;i<poolSize;i++)
        {
            PoolObject newGameObject = Instantiate(prefab,this.transform);
            newGameObject.name += i;
            newGameObject.OnDisableObject += DisableObject;
            newGameObject.SetActive(false);
            availableObject.Enqueue(newGameObject);
            poolObject.Add(newGameObject);
        }
    }
    public GameObject EnableObject()
    {
        if (availableObject.Count>0)
        {
            PoolObject newEnableObject = availableObject.Dequeue();
            newEnableObject.SetActive(true);
            newEnableObject.StartActive();
            return newEnableObject.gameObject;
        }
        return null;
    }
    public GameObject EnableObject(Vector3 activePosition)
    {
        if (availableObject.Count > 0)
        {
            PoolObject newEnableObject = availableObject.Dequeue();
            newEnableObject.transform.position = activePosition;
            newEnableObject.SetActive(true);
            newEnableObject.StartActive();
            return newEnableObject.gameObject;
        }
        return null;
    }

    public void DisableObject(object sender,EventArgs e)
    {
        PoolObject recycleObject = (PoolObject)sender;
        if (poolObject.Contains(recycleObject))
        {
            recycleObject.SetActive(false);
            availableObject.Enqueue(recycleObject);
        }
    }
}
