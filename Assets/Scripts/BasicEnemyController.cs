using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    public int maxHealth = 3;
    public EnemyHealthBar healthBar;

    private int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        if (healthBar == null)
        {
            healthBar = GetComponentInChildren<EnemyHealthBar>();
        }

        if (healthBar != null)
        {
            healthBar.SetFill(1f);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (healthBar != null)
        {
            healthBar.SetFill(Mathf.Clamp01((float)currentHealth / maxHealth));
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        WaveManager wm = FindAnyObjectByType<WaveManager>();
        if (wm != null)
        {
            wm.EnemyDefeated();
        }

        Destroy(gameObject);
    }
}
