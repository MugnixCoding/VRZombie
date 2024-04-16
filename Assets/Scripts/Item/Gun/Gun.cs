using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")][SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")][SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")][SerializeField] private float ejectPower = 150f;
    [Range(0,1)][SerializeField] private float shotInterval = 0.5f;
    private bool canShoot;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private AudioClip noAmmoSound;

    [SerializeField] private XRBaseInteractor socketInteractor;
    [SerializeField] private Magazine magazine;
    private bool hasSlided;

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.AddListener(AddMagazine);
            socketInteractor.selectExited.AddListener(RemoveMagazine);
        }
        if (magazine != null)
        {
            hasSlided = true;
        }
        else
        {
            hasSlided = false;
        }
        canShoot = true;
    }
    
    public void PullTheTrigger()
    {
        if (!canShoot)
        {
            return;
        }
        if (magazine!=null && magazine.NumberOfBullet > 0 && hasSlided)
        {
            canShoot = false;
            StartCoroutine(ShotIntervalTimer());
            gunAnimator.SetTrigger("Fire");
        }
        else
        {
            audioSource.PlayOneShot(noAmmoSound);
        }
    }
    private IEnumerator ShotIntervalTimer()
    {
        yield return new WaitForSeconds(shotInterval);
        canShoot = true;
    }

    public void AddMagazine(SelectEnterEventArgs e)
    {
        hasSlided = false;
        magazine = e.interactableObject.transform.GetComponent<Magazine>();
        audioSource.PlayOneShot(reloadSound);
    }
    public void RemoveMagazine(SelectExitEventArgs e)
    {
        magazine = null;
        audioSource.PlayOneShot(reloadSound);
    }
    public void Slide()
    {
        if (!hasSlided)
        {
            hasSlided = true;
            audioSource.PlayOneShot(reloadSound);
        }
        else if (magazine==null)
        {
            audioSource.PlayOneShot(reloadSound);
        }
    }

    //This function creates the bullet behavior
    void Shoot()
    {
        magazine.ConsumeBullet();
        audioSource.PlayOneShot(shootSound);
        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

}
