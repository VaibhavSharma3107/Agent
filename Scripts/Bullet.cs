using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour

{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float bulletSpeed = 10f; // Speed of the bullet

    private bool isFiring = false;
    public   AudioSource bulletSound;
    public ZombieAi zozo;

    // Update is called once per frame
    void Update()
    {
        if ((OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch) ||
            OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)) && !isFiring)
        {
            StartCoroutine(FireBulletsWithDelay());
        }

       
    }

    IEnumerator FireBulletsWithDelay()
    {
        isFiring = true; // Set flag to prevent multiple coroutine calls

        while (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch) ||
               OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            SpawnBullet();
            yield return new WaitForSeconds(1f); // Spawn a bullet with a one-second delay
        }

        isFiring = false; // Reset flag when trigger is released
    }

    void SpawnBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

        Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            Vector3 bulletDirection = spawnPoint.forward * bulletSpeed;
            bulletRigidbody.velocity = bulletDirection;

            bulletSound.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Zombie")
        {
            Debug.LogError("GRuuuuu");
        }
    }
}


