using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    [SerializeField] private int health = 10;
    [SerializeField] private float damageGapTime = 1f;

    private AudioSource playerAudio;
    [SerializeField] private AudioClip hurtedSound;
    [SerializeField] private AudioClip deathSound;

    public event EventHandler<DamageEventArgs> OnTakeDamage;
    public event EventHandler OnDead;

    private bool isDamageable;
    private bool isDead;

    private void Start()
    {
        isDamageable = true;
        isDead = false;
        TryGetComponent<AudioSource>(out playerAudio);
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void TakeDamage(int damage)
    {
        if (isDamageable && !isDead)
        {
            StartCoroutine(DamageGapTimeCorutine());
            health -= damage;
            if (health <= 0)
            {
                if (playerAudio && deathSound)
                {
                    playerAudio.PlayOneShot(deathSound);
                }
                Dead();
            }
            else
            {
                if (playerAudio && hurtedSound)
                {
                    playerAudio.PlayOneShot(hurtedSound);
                }
                OnTakeDamage?.Invoke(this, new DamageEventArgs(damage));
            }
        }
    }
    public void TakeDamage(int damage,Transform attacker)
    {
        if (isDamageable && !isDead)
        {
            StartCoroutine(DamageGapTimeCorutine());
            health -= damage;
            if (health <= 0)
            {
                if (playerAudio && deathSound)
                {
                    playerAudio.PlayOneShot(deathSound);
                }
                Dead();
            }
            else
            {
                if (playerAudio && hurtedSound)
                {
                    playerAudio.PlayOneShot(hurtedSound);
                }
                Vector3 direction = attacker.position-transform.position;
                OnTakeDamage?.Invoke(this, new DamageEventArgs(damage,direction));
            }
        }
    }
    private IEnumerator DamageGapTimeCorutine()
    {
        isDamageable = false;
        yield return new WaitForSeconds(damageGapTime);
        isDamageable = true;
    }
    private void Dead()
    {
        isDead = true;
        OnDead?.Invoke(this, EventArgs.Empty);
    }
    public int GetPlayerHealth()
    {
        return health;
    }
}
public class DamageEventArgs : EventArgs
{
    public int Damage { get; }
    public Vector3 DirectionDamgeFrom { get; }
    public DamageEventArgs(int damage)
    {
        Damage = damage;
        DirectionDamgeFrom = Vector3.zero;
    }
    public DamageEventArgs(int damage, Vector3 directionDamgeFrom)
    {
        Damage = damage;
        DirectionDamgeFrom = directionDamgeFrom;
    }
}
