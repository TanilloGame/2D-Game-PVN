using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public float knockbackDuration = 0.2f;
    public float knockbackSpeed = 5f;
    private bool isKnockedBack = false;
    private float knockbackTime;
    private Vector2 knockbackDirection;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isDead = false;

    public ParticleSystem enemyHit;
    public ParticleSystem attackParticles;
    public DoTweenHardEnemy doTweenHardEnemy;

    public int playerDamage = 1; // Daño al jugador al colisionar
    public float knockbackToPlayer = 10f; // Empuje al jugador
    private GameObject player;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (isKnockedBack && !isDead)
        {
            KnockbackUpdate();
        }
    }

    public void TakeDamage(int damage, Vector2 knockbackDir)
    {
        if (isDead) return;

        currentHealth -= damage;

        // Iniciar empuje
        knockbackDirection = knockbackDir;
        isKnockedBack = true;
        knockbackTime = 0f;
        enemyHit.Play();
        attackParticles.Play();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void KnockbackUpdate()
    {
        knockbackTime += Time.deltaTime;

        if (knockbackTime < knockbackDuration)
        {
            rb.velocity = knockbackDirection * knockbackSpeed * (1f - (knockbackTime / knockbackDuration));
        }
        else
        {
            isKnockedBack = false;
            rb.velocity = Vector2.zero;
        }
    }

    void Die()
    {
        if (doTweenHardEnemy != null)
        {
            doTweenHardEnemy.movementStop();
        }

        isDead = true;

        animator.SetTrigger("Die");

        GetComponent<Collider2D>().enabled = false;

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        AudioManager.Instance.PlaySound("EnemyDead");

        // Si es el Final Boss, iniciar fin de la partida
        if (CompareTag("FinalBoss"))
        {
            StartCoroutine(EndGameAfterDelay());
        }
        else
        {
            Destroy(this.gameObject, 1.5f);
        }
    }

    private IEnumerator EndGameAfterDelay()
    {
        yield return new WaitForSeconds(3f); // Espera 3 segundos antes de ir al menú principal
        SceneManager.LoadScene("MainMenu"); // Cambia "MainMenu" por el nombre exacto de tu escena de menú principal
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el enemigo colisiona con el jugador
        if (collision.gameObject.CompareTag("Player") && !isDead)
        {
            // Aplica daño y empuje al jugador
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                playerHealth.TakeDamage(playerDamage, knockbackDir * knockbackToPlayer);
                AudioManager.Instance.PlaySound("PlayerHit");
            }
        }
    }
}