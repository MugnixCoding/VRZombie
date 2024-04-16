using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpactManager : MonoBehaviour
{
    public static BulletImpactManager Instance;
    [SerializeField] private GameObject bulletImpact;
    [SerializeField] private float ImpactDestroyTime = 10f;
    [SerializeField] private float ImpactOffset = 0.001f;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void CreateBulletImpact(ContactPoint contact)
    {
        GameObject impact = Instantiate(bulletImpact);
        impact.transform.position = contact.point + (contact.normal * contact.separation) + contact.normal* ImpactOffset;
        impact.transform.forward = contact.normal;
        Destroy(impact, ImpactDestroyTime);
    }
    private void OnDestroy()
    {
        Instance = null;
    }
}
