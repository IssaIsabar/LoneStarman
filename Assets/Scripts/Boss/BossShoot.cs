using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    public Transform firePoint;
    Transform target;
    public GameObject bulletPrefab;
    public AudioSource audioSource;
    public AudioClip shootingAudioClip;

    public float bulletForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        InvokeRepeating("Shoot", 5, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsTarget();
    }

    private void Shoot()
    {
        audioSource.PlayOneShot(shootingAudioClip);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
    private void RotateTowardsTarget()
    {
        Vector2 direction = target.position - firePoint.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
