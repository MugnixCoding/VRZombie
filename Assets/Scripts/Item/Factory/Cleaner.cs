using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    [SerializeField] private Vector3 center = Vector3.zero;
    [SerializeField] private Vector3 size = Vector3.zero;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float checkTime = 5f;

    private float checkTimer;
    void Start()
    {
        checkTimer = 0;
    }

    void Update()
    {
        if (checkTimer>=checkTime)
        {
            checkTimer = 0;
            Clean();
        }
        else
        {
            checkTimer += Time.deltaTime;
        }
    }
    private void Clean()
    {
        Collider[] colliders = Physics.OverlapBox(center + transform.position, size / 2, Quaternion.identity, targetLayer);

        foreach (Collider collider in colliders)
        {
            Destroy(collider.gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center + transform.position, size);
    }
}
