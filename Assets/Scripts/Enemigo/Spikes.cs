using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
   

    public float knockbackDuration = 0.2f;
    public float knockbackSpeed = 5f;
    private bool isKnockedBack = false;
    private float knockbackTime;
    private Vector2 knockbackDirection;

    private Rigidbody2D rb;
    
    private bool isDead = false;
    

    

    void Start()
    {
        
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
       
        // Iniciar empuje
        knockbackDirection = knockbackDir;
        isKnockedBack = true;
        knockbackTime = 0f;
        AudioManager.Instance.PlaySound("Damage");


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

    
}