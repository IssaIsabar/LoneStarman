using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public HealthBar healthBar;
    private readonly float fadeOutTime = 1f;
    private PlayerMovement playerMovement;
    private Shooting shooting;
    // Start is called before the first frame update
    void Start()
    {
        shooting = GetComponent<Shooting>();
        playerMovement = GetComponent<PlayerMovement>();
        healthBar.SetMaxHealth(PlayerStats.Instance.playerHealth);
    }

    public void TakeDamage(float damage)
    {
        PlayerStats.Instance.playerHealth -= damage;
        UIManager.Instance.ActivatePickedItem("-1 hp");
        healthBar.SetHealth(PlayerStats.Instance.playerHealth);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HealthPills"))
        {
            PlayerStats.Instance.playerHealth++;
            healthBar.SetHealth(PlayerStats.Instance.playerHealth);
            UIManager.Instance.ActivatePickedItem("+1 health");
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("SpeedDrink") && playerMovement.MovementSpeed < 12)
        {
            playerMovement.MovementSpeed++;
            UIManager.Instance.ActivatePickedItem("+1 speed");
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("RapidFire"))
        {
            shooting.rapidFire = true;
            UIManager.Instance.ActivatePickedItem("Rapid fire enabled");
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            healthBar.SetHealth(PlayerStats.Instance.playerHealth * 0);
            StartCoroutine(FadeOut(GetComponent<SpriteRenderer>()));
            GameManager.Instance.EndGame();
            playerMovement.enabled = false;
        }
        else if (collision.gameObject.CompareTag("Enemy") && PlayerStats.Instance.playerHealth == 1)
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
            GameManager.Instance.EndGame();
            StartCoroutine(FadeOut(GetComponent<SpriteRenderer>()));
            playerMovement.enabled = false;
        }
        else if (collision.gameObject.CompareTag("BossLaserBullet") && PlayerStats.Instance.playerHealth == 1)
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
            GameManager.Instance.EndGame();
            StartCoroutine(FadeOut(GetComponent<SpriteRenderer>()));
            playerMovement.enabled = false;
        }
        else
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    IEnumerator FadeOut(SpriteRenderer sr)
    {
        Color tmpColor = sr.color;

        while (tmpColor.a > 0f)
        {
            tmpColor.a -= Time.deltaTime / fadeOutTime;
            sr.color = tmpColor;

            if (tmpColor.a <= 0f)
                tmpColor.a = 0.0f;


            yield return null;
        }

        sr.color = tmpColor;
    }
    
}
