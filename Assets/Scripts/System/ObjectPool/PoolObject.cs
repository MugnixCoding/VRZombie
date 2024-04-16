using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    public event EventHandler OnDisableObject;
    public abstract void StartActive();
    protected virtual void DisableObject(EventArgs e)
    {
        OnDisableObject?.Invoke(this, e);
    }
    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
