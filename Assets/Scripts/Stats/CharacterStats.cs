using UnityEngine;

public class DamageStats
{
    public int minDamage;
    public int maxDamage;
    public bool isMiss = false;
    public bool isCrititcal = false;
}

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    private int initHealth = 100;

    public int currentHealth { get; private set; }

    public Stat minDamage;
    public Stat maxDamage;
    public Stat attackSpeed;

    public Stat armor;

    public Stat strength;
    public Stat agility;
    public Stat spirit;
    public Stat accuracy;


    public void Awake()
    {
        currentHealth = GetMaxHealth();

        OnChangeHealth();
    }

    public int GetMaxHealth()
    {
        return initHealth + (strength.GetValue() * 50);
    }

    public DamageStats GetCalculatedDamages()
    {
        int calculatedMinDamage = minDamage.GetValue() + strength.GetValue() * 3;
        int calculatedMaxDamage = maxDamage.GetValue() + strength.GetValue() * 3;

        DamageStats damageStats = new() { minDamage = calculatedMinDamage, maxDamage = calculatedMaxDamage };

        return damageStats;
    }

    public void TakeDamage(CharacterStats enemyStats)
    {
        bool miss = HandleMiss(enemyStats);

        if (miss)
        {
            HandlePlayerNotify(0, true, false);
            return;
        }

        DamageStats enemyDamageStats = enemyStats.GetCalculatedDamages();

        HandleArmor(enemyDamageStats);

        bool isCritical = HandleCritical(enemyDamageStats, enemyStats);

        int damage = Random.Range(enemyDamageStats.minDamage, enemyDamageStats.maxDamage);

        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;

        OnChangeHealth();

        HandlePlayerNotify(damage, false, isCritical);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private bool HandleMiss(CharacterStats enemyStats)
    {
        int agilityValue = agility.GetValue();
        int enemyAccuracy = enemyStats.accuracy.GetValue();

        if (agilityValue >= enemyAccuracy) return false;

        int diff = enemyAccuracy - agilityValue;

        float chancePercent = diff * 0.5f;

        if (chancePercent > 95f) chancePercent = 95f;

        float randomChance = Random.Range(0f, 100f);

        if (randomChance < chancePercent) return true;

        return false;
    }

    private void HandleArmor(DamageStats enemyDamageStats)
    {
        enemyDamageStats.minDamage -= armor.GetValue();
        enemyDamageStats.maxDamage -= armor.GetValue();
    }

    private bool HandleCritical(DamageStats enemyDamageStats, CharacterStats enemyStats)
    {
        int spiritValue = spirit.GetValue();
        int enemyAccuracy = enemyStats.accuracy.GetValue();

        if (spiritValue >= enemyAccuracy) return false;

        int diff = enemyAccuracy - spiritValue;
        float chancePercent = diff * 0.5f;

        if (chancePercent > 95f) chancePercent = 95f;

        float randomChance = Random.Range(0f, 100f);

        if (randomChance < chancePercent)
        {
            enemyDamageStats.minDamage *= 2;
            enemyDamageStats.maxDamage *= 2;
            return true;
        }

        return false;
    }

    private void HandlePlayerNotify(int damage, bool isMiss, bool isCritical)
    {
        if (!gameObject.CompareTag("Player"))
        {
            NotifyManager.instance.ShowAttackNotify(damage, isMiss, isCritical);
        }
    }

    public virtual void OnChangeHealth()
    {
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
    }
}
