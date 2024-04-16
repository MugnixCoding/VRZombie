using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIHeadFollowing : MonoBehaviour
{
    [SerializeField] private Transform Head;
    [SerializeField] private float distance =  3f;

    private void Update()
    {
        transform.position = Head.position + new Vector3(Head.forward.x,0,Head.forward.z).normalized * distance;
        transform.LookAt(new Vector3(Head.position.x,transform.position.y,Head.position.z));
        transform.forward *= -1;
    }
}
