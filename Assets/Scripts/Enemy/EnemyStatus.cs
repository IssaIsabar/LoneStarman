using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public float enemyHealth = 5f;
    public float distance = 0f;
    public float mergeSpeed = 0f;
    public HealthBar healthBar;
    public GameObject mergedObject;

    private bool canMerge = false;
    private int ID;
    private Collider2D col;
    private Transform pos;
    private Transform thisEnemy;
    private Transform collisionTarget;
    void Start()
    {
        healthBar.SetMaxHealth(enemyHealth);
        pos = GetComponent<Transform>();
        col = GetComponent<Collider2D>();
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
        else if (collision.gameObject.CompareTag("LaserBullet") && enemyHealth == 1)
        {
            TakeDamage(1);
            Destroy(gameObject);
            GameManager.Instance.playerScore++;
            ItemSpawner.Instance.SpawnNewItem(pos.position.x, pos.position.y);
        }
        else if (!collision.gameObject.CompareTag("Enemy"))
        {
            thisEnemy = transform;
            collisionTarget = collision.transform;
            canMerge = true;
        }



    }
    public void MoveTowards()
    {
        if (canMerge)
        {
            transform.position = Vector2.MoveTowards(thisEnemy.position, collisionTarget.position, mergeSpeed);
            if (Vector2.Distance(thisEnemy.position, collisionTarget.position) < distance)
            {
                Debug.Log("Yel");
                Destroy(gameObject);
            }
        }
        canMerge = false;
    }



}
