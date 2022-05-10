using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public float enemyHealth = 5f;
    public float distance = 0f;
    public float mergeSpeed = 0f;
    public HealthBar healthBar;
    public GameObject MergedObject;

    private bool canMerge = false;
    private int ID;
    private Transform pos;
    private Transform thisTarget;
    private Transform collisionTarget;
    void Start()
    {
        healthBar.SetMaxHealth(enemyHealth);
        pos = GetComponent<Transform>();
        ID = GetInstanceID();
    }
    void Update()
    {
        Physics2D.IgnoreLayerCollision(7, 8);
    }
    private void FixedUpdate()
    {
        MoveTowards();
    }
    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        healthBar.SetHealth(enemyHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LaserBullet") && enemyHealth > 1)
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("LaserBullet") && enemyHealth <= 1)
        {
            TakeDamage(1);
            Destroy(gameObject);
            GameManager.Instance.playerScore++;
            ItemSpawner.Instance.SpawnNewItem(pos.position.x, pos.position.y);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            thisTarget = transform;
            collisionTarget = collision.transform;
            canMerge = true;
            Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
            Destroy(GetComponent<Rigidbody2D>());
        }
        else if (!collision.gameObject.CompareTag("Enemy") || !collision.gameObject.CompareTag("LaserBullet"))
        {
            thisTarget = transform;
            collisionTarget = collision.transform;
            canMerge = true;
            Destroy(GetComponent<Rigidbody2D>());
        }



    }
    public void MoveTowards()
    {
        if (canMerge)
        {
            transform.position = Vector2.MoveTowards(thisTarget.position, collisionTarget.position, mergeSpeed);
            if (Vector2.Distance(thisTarget.position, collisionTarget.position) < distance)
            {
                if (collisionTarget.gameObject.CompareTag("Enemy"))
                {
                    if (ID < collisionTarget.gameObject.GetComponent<EnemyStatus>().ID) { return; }
                    GameObject O = Instantiate(MergedObject, transform.position, Quaternion.identity) as GameObject;
                    Destroy(collisionTarget.gameObject);
                }
                Destroy(gameObject);
            }
        }
    }



}
