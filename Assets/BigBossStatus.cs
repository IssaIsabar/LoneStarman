using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossStatus : MonoBehaviour
{
    public float speed;
    public float bossHealth = 40f;
    public float distance = 0f;
    public float mergeSpeed = 0f;
    public float bulletDamage = 0f;
    public GameObject MergedObject;
    public HealthBar healthBar;

    private Transform firstBoss;
    private Transform secondBoss;
    private Transform target;
    private bool canMerge;
    private int ID;

    // Start is called before the first frame update
    void Start()
    {
        ID = GetInstanceID();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        healthBar.SetMaxHealth(bossHealth);
    }
    public void TakeDamage(float damage)
    {
        bossHealth -= damage;
        healthBar.SetHealth(bossHealth);
    }
    private void FixedUpdate()
    {
        MoveTowards();
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
        else if (collision.gameObject.CompareTag("BigBoss"))
        {
            firstBoss = transform;
            secondBoss = collision.transform;
            canMerge = true;
            Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
            Destroy(GetComponent<Rigidbody2D>());
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            bossHealth += collision.gameObject.GetComponent<EnemyStatus>().enemyHealth;
        }
    }
    public void MoveTowards()
    {
        if (canMerge)
        {
            transform.position = Vector2.MoveTowards(firstBoss.position, secondBoss.position, mergeSpeed);
            if (Vector2.Distance(firstBoss.position, secondBoss.position) < distance)
            {
                if (ID < secondBoss.gameObject.GetComponent<BigBossStatus>().ID) { return; }
                GameObject O = Instantiate(MergedObject, transform.position, Quaternion.identity) as GameObject;
                Destroy(secondBoss.gameObject);
                Destroy(gameObject);
            }
        }
    }
    void Update()
    {
        if (!GameManager.Instance.transitionScene)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
