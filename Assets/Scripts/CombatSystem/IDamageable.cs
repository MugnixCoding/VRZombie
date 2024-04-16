
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage);
    void TakeDamage(int damage,Transform attacker);
    Transform GetTransform();
}