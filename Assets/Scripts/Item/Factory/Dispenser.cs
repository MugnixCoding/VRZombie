using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    [SerializeField] private GameObject spawnPrefabs;
    [SerializeField] private float spawnTime = 15f;
    [SerializeField] private bool produceAtStart = false;

    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Canvas prefabsIcon;

    private float spawnTimer;
    void Start()
    {
        if (produceAtStart)
        {
            SpawnObject();
            ShowIcon();
            spawnTimer = spawnTime;
        }
        else
        {
            HideIcon();
            spawnTimer = 0f;
        }
    }

    void Update()
    {
        if (spawnTimer< spawnTime)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnTime)
            {
                SpawnObject();
                ShowIcon();
            }
        }
        else
        {
            if (!HasSupplies())
            {
                HideIcon();
                spawnTimer = 0;
            }
        }
    }
    private void SpawnObject()
    {
        Instantiate(spawnPrefabs, spawnPosition.position,Quaternion.identity,spawnPosition);
    }
    private bool HasSupplies()
    {
        if (spawnPosition.childCount==0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void ShowIcon()
    {
        prefabsIcon.gameObject.SetActive(true);
    }
    private void HideIcon()
    {
        prefabsIcon.gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.IsChildOf(spawnPosition))
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}
