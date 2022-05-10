using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : MonoBehaviour
{

    public float speed;
    public float bossHealth = 40f;
    public float distance = 0f;
    public float mergeSpeed = 0f;
    public float bulletDamage = 0f;
    public GameObject MergedObject;
    public HealthBar healthBar;

    private Transform thisTarget;
    private Transform collisionTarget;
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
    private void FixedUpdate()
    {
        MoveTowards();
        healthBar.SetHealth(bossHealth);
    }
    public void TakeDamage(float damage)
    {
        bossHealth -= damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LaserBullet") && bossHealth > 1)
        {
            TakeDamage(bulletDamage);
        }
        else if (collision.gameObject.CompareTag("LaserBullet") && bossHealth <= 1)
        {
            TakeDamage(bulletDamage);
            GameManager.Instance.playerScore += 5;
            Destroy(gameObject);
            //GameManager.Instance.LevelComplete();
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            thisTarget = transform;
            collisionTarget = collision.transform;
            canMerge = true;
            Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
            Destroy(GetComponent<Rigidbody2D>());
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            bossHealth += collision.gameObject.GetComponent<EnemyStatus>().enemyHealth;
        }
        else if (!collision.gameObject.CompareTag("Boss") || !collision.gameObject.CompareTag("LaserBullet"))
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
                if (collisionTarget.gameObject.CompareTag("Boss"))
                {
                    if (ID < collisionTarget.gameObject.GetComponent<BossStatus>().ID) { return; }
                    GameObject O = Instantiate(MergedObject, transform.position, Quaternion.identity) as GameObject;
                    Destroy(collisionTarget.gameObject);
                }
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
