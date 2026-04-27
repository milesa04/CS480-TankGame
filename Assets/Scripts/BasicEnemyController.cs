using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    // --- Stats ---
    public int health = 3; 

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject); 
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        FindAnyObjectByType<WaveManager>().EnemyDefeated();
        
        Destroy(gameObject); 
    }
}