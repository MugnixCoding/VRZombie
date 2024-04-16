using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeSpan=5f;
    [SerializeField] private int damage = 1;
    [SerializeField] private LayerMask impactLayer;
    [SerializeField] private LayerMask destroyLayer;
    [SerializeField] private LayerMask damageLayer;
    private void Awake()
    {
        Destroy(gameObject, lifeSpan);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (impactLayer == (impactLayer | (1 << collision.gameObject.layer)))
        {
            BulletImpactManager.Instance?.CreateBulletImpact(collision.GetContact(0));
            Destroy(gameObject);
            return;
        }
        else if ( destroyLayer == (destroyLayer |(1<<collision.gameObject.layer)))
        {
            Destroy(gameObject);
            return;
        }
        else if (damageLayer == (damageLayer | (1 << collision.gameObject.layer)))
        {
            IDamageable damageable;
            if (collision.gameObject.TryGetComponent<IDamageable>(out damageable))
            {
                damageable.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
