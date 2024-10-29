using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public HealthUIManager healthUIManager;
    public int maxHealth = 5;
    private int currentHealth;

    public float invulnerabilityDuration = 2f;
    private bool isInvulnerable = false;
    public float knockbackForce = 10f;
    public Color damageColor = Color.red;
    public float colorChangeDuration = 0.2f;
    public float flashInterval = 0.1f;
    private bool isKnockedback = false;

    public LayerMask finalBossLayer;
    public LayerMask enemyLayer;
    public LayerMask drownLayer;
    public LayerMask spikeLayer;
    public Animator animator;
    public string deathAnimationName = "Death";

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private bool isDead = false;
    public ParticleSystem damageParticles;
    public ParticleSystem drownParticles;

    void Start()
    {
        currentHealth = maxHealth;  // Inicializa la salud actual
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        healthUIManager.UpdateHearts(currentHealth); // Inicializa la UI de salud
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(1, Vector2.left);  // Para pruebas, daña al jugador
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            if (!isInvulnerable && !isDead)
            {
                Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
                TakeDamage(1, knockbackDirection);
                damageParticles.Play();
            }
        }

        if (((1 << collision.gameObject.layer) & finalBossLayer) != 0)
        {
            if (!isInvulnerable && !isDead)
            {
                Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
                TakeDamage(4, knockbackDirection);
                damageParticles.Play();
            }
        }

        if (((1 << collision.gameObject.layer) & spikeLayer) != 0)
        {
            if (!isInvulnerable && !isDead)
            {
                Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
                TakeDamage(3, knockbackDirection);
                damageParticles.Play();
            }
        }

        if (((1 << collision.gameObject.layer) & drownLayer) != 0)
        {
            if (!isInvulnerable && !isDead)
            {
                Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
                TakeDamage(5, knockbackDirection);
                drownParticles.Play();
                AudioManager.Instance.PlaySound("Water");
            }
        }

        // Verifica si colisiona con un Heart para recuperar salud
        if (collision.gameObject.CompareTag("Heart"))
        {
            RestoreHealth(1);  // Recupera 1 de salud
            Destroy(collision.gameObject);  // Destruye el objeto Heart
        }


    }

    public void TakeDamage(int damage, Vector2 knockbackDirection)
    {
        if (!isInvulnerable)
        {
            AudioManager.Instance.PlaySound("Damage");
            currentHealth -= damage;
            healthUIManager.UpdateHearts(currentHealth); // Actualiza la UI de salud
            if (currentHealth <= 0)
            {
                Die();

            }
            else
            {
                if (!isKnockedback)
                {
                    rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }
                StartCoroutine(InvulnerabilityFlash());
            }
        }
    }

    public void RestoreHealth(int healAmount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += healAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            healthUIManager.UpdateHearts(currentHealth); // Actualiza la UI de salud
        }
    }

    void ApplyKnockback(Vector2 direction)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }

    private IEnumerator ChangeColorOnDamage()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(colorChangeDuration);
        spriteRenderer.color = originalColor;
    }

    private IEnumerator InvulnerabilityFlash()
    {
        isInvulnerable = true;
        float invulnerabilityTimer = 0f;

        while (invulnerabilityTimer < invulnerabilityDuration)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashInterval);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashInterval);

            invulnerabilityTimer += flashInterval * 2;
        }

        isInvulnerable = false;
    }

    void Die()
    {
        if (!isDead)
        {
            isDead = true;
            Debug.Log("Jugador ha muerto");

            // Reproduce el sonido de Game Over antes de cambiar de escena
            AudioManager.Instance.PlaySound("GameOver");

            // Cambia a la escena Game Over
            SceneManager.LoadScene("GameOver"); // Asegúrate de que la escena GameOver esté guardada y en Build Settings

            // Si tienes animaciones de muerte, agrégalas aquí (aunque no será visible en Game Over)
            if (animator != null)
            {
                animator.SetTrigger(deathAnimationName);
            }

            // Inicia la corutina para manejar la animación de muerte
            StartCoroutine(HandleDeathAnimation());
        }
    }

    private IEnumerator HandleDeathAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}