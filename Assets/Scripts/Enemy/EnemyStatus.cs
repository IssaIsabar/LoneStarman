using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public float enemyHealth = 5f;
    public HealthBar healthBar;
    private Collider2D col;
    private Transform pos;
    void Start()
    {
        healthBar.SetMaxHealth(enemyHealth);
        pos = GetComponent<Transform>();
        col = GetComponent<Collider2D>();
    }
    void Update()
    {
        Physics2D.IgnoreLayerCollision(7, 8);
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
            PlayerStats.Instance.playerScore++;
            ItemSpawner.Instance.SpawnNewItem(pos.position.x, pos.position.y);
        }


    }



}
