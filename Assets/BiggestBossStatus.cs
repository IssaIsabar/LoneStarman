using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggestBossStatus : MonoBehaviour
{
    public float speed;
    public float bossHealth = 40f;
    public float bulletDamage = 0f;
    public HealthBar healthBar;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        healthBar.SetMaxHealth(bossHealth);
    }
    public void TakeDamage(float damage)
    {
        bossHealth -= damage;
        healthBar.SetHealth(bossHealth);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LaserBullet") && bossHealth > 1)
        {
            TakeDamage(bulletDamage);
        }
        else if (collision.gameObject.CompareTag("LaserBullet") && bossHealth == 1)
        {
            TakeDamage(bulletDamage);
            Destroy(this.gameObject);
            GameManager.Instance.LevelComplete();
        }
        else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("BigBoss"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
                bossHealth += collision.gameObject.GetComponent<EnemyStatus>().enemyHealth;
            else if (collision.gameObject.CompareTag("Boss"))
                bossHealth += collision.gameObject.GetComponent<BossStatus>().bossHealth;
            else if (collision.gameObject.CompareTag("BiggBoss"))
                bossHealth += collision.gameObject.GetComponent<BigBossStatus>().bossHealth;
            Destroy(collision.gameObject);
        }
    }
    void Update()
    {
        if (!GameManager.Instance.transitionScene)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

    }
}
