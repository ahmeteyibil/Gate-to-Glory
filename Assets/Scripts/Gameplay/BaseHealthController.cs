using System;
using UnityEngine;

public abstract class BaseHealthController : MonoBehaviour
{
    protected float currentHealth;
    // Karakterin o anki statlarýný tutan referans
    CharacterStats stats;
    public static Action<GameObject, float, float> OnHealthChanged;

    public virtual void Awake()
    {
        stats = GetComponent<CharacterInfo>().RuntimeStats;
        if (stats == null)
        {
            Debug.Log($"{gameObject.name} için stats null.");
        }
    }
    public void InitializeHealth()
    {
        if (stats == null) stats = GetComponent<CharacterInfo>().RuntimeStats;

        if (stats != null)
        {
            currentHealth = stats.maxHealth.value;
            Debug.Log($"{gameObject.name} için atanan can deðeri: {currentHealth}");
            OnHealthChanged?.Invoke(gameObject, currentHealth, stats.maxHealth.value);
        }
    }
    public virtual void TakeDamage(float damage)
    {
        // Zýrhý burada hesaba katabilirsin
        //float netDamage = Mathf.Max(1, damage - stats.armor);
        currentHealth -= damage;

        OnHealthChanged?.Invoke(gameObject, currentHealth, stats.maxHealth.value);
        if (currentHealth <= 0) Die();
    }
    public virtual void AddHealth(float value)
    {
        currentHealth += value;
        currentHealth = Mathf.Min(stats.maxHealth.value, currentHealth); // Can deðeri max deðerin üstüne çýkmasýn
        OnHealthChanged?.Invoke(gameObject, currentHealth, stats.maxHealth.value);
    }
    protected abstract void Die();
    public virtual float GetHealth() { return currentHealth; }
}
