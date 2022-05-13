using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public AudioSource audioSource;
    public AudioClip shootingAudioClip;
    private float fireSpeed = 0.1f;
    private float canFire = 0.5f;
    public bool rapidFire = false;


    public float bulletForce = 20f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rapidFire)
        {
            if (Input.GetButton("Fire1") && Time.time > canFire)
            {
                Shoot();
                canFire = Time.time + fireSpeed;
            }
        }
        else if(!rapidFire)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        audioSource.PlayOneShot(shootingAudioClip);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

    }
}
