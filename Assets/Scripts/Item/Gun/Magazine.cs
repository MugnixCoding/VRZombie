using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    [SerializeField]
    private int numberOfBullet = 8;
    public int NumberOfBullet => numberOfBullet;

    public void ConsumeBullet()
    {
        if (numberOfBullet>0)
        {
            numberOfBullet -= 1;
        }
    }
}
