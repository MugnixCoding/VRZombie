using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class MonsterAudioSource
{
    public AudioSource audioSource;
    public AudioClip[] walkFootstep;
    public AudioClip[] sprintFootstep;
    public AudioClip[] attackSound;
    public AudioClip[] hurtedSound;
    public AudioClip[] deathSound;
    public AudioClip[] lurkSound;
    public AudioClip[] roarSound;

    public void PlayWalkFootstep()
    {
        if (walkFootstep.Length > 0)
        {
            audioSource.PlayOneShot(walkFootstep[Random.Range(0, walkFootstep.Length - 1)]);
        }
    }
    public void PlaySprintFootstep()
    {
        if (sprintFootstep.Length > 0)
        {
            audioSource.PlayOneShot(sprintFootstep[Random.Range(0, sprintFootstep.Length - 1)]);
        }
    }
    public void PlayAttackSound()
    {
        if (attackSound.Length > 0)
        {
            audioSource.PlayOneShot(attackSound[Random.Range(0, attackSound.Length - 1)]);
        }
    }
    public void PlayHurtedSound()
    {
        if (hurtedSound.Length > 0)
        {
            audioSource.PlayOneShot(hurtedSound[Random.Range(0, hurtedSound.Length - 1)]);
        }
    }
    public void PlayDeathSound()
    {
        if (deathSound.Length > 0)
        {
            audioSource.PlayOneShot(deathSound[Random.Range(0, deathSound.Length - 1)]);
        }
    }
    public void PlayLurkSound()
    {
        if (lurkSound.Length > 0)
        {
            audioSource.PlayOneShot(lurkSound[Random.Range(0, lurkSound.Length - 1)]);
        }
    }
    public void PlayRoarSound()
    {
        if (roarSound.Length > 0)
        {
            audioSource.PlayOneShot(roarSound[Random.Range(0, roarSound.Length - 1)]);
        }
    }
    
}
