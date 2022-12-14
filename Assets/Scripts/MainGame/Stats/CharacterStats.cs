using UnityEngine;

public class DamageStats
{
    public float minDamage;
    public float maxDamage;
    public bool isMiss = false;
    public bool isCrititcal = false;
}

public class CharacterStats : MonoBehaviour
{
    public PointStat health;
    public PointStat mana;

    public Stat minDamage;
    public Stat maxDamage;
    public Stat attackSpeed;

    public Stat armor;

    public Stat strength;
    public Stat agility;
    public Stat spirit;
    public Stat accuracy;

    public Stat maxHandleWeight;

    float healthRegenerationSpeed = 1f;
    public Stat healthRegeneration;
    
    float manaRegenerationSpeed = 3f;
    public Stat manaRegeneration;


    public void Awake()
    {
        health.SetStatModifier(strength.GetValue());
        health.Initialize();

        mana.SetStatModifier(spirit.GetValue());
        mana.Initialize();

        OnChangeHealth();
        OnChangeMana();
    }

    private void Update()
    {
        _HandleRegeneration();
    }

    void _HandleRegeneration()
    {
        health.Increase(Time.deltaTime * healthRegenerationSpeed * (float)healthRegeneration.GetValue());
        mana.Increase(Time.deltaTime * manaRegenerationSpeed * (float)manaRegeneration.GetValue());

        OnChangeHealth();
    }

    public float GetMaxHealth()
    {
        return health.GetMaxValue();
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
        bool miss = _HandleMiss(enemyStats);

        if (miss)
        {
            _HandlePlayerNotify(damage: 0, isMiss: true, isCritical: false);
            return;
        }

        DamageStats enemyDamageStats = enemyStats.GetCalculatedDamages();

        _HandleArmor(enemyDamageStats);

        bool isCritical = _HandleCritical(enemyDamageStats, enemyStats);

        float damage = Random.Range(enemyDamageStats.minDamage, enemyDamageStats.maxDamage);

        damage = Mathf.Clamp(damage, 0, float.MaxValue);

        health.Decrease(damage);

        OnChangeHealth();

        _HandlePlayerNotify(damage: damage, isMiss: false, isCritical: isCritical);

        if (health.currentValue <= 0)
        {
            Die();
        }
    }

    private bool _HandleMiss(CharacterStats enemyStats)
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

    private void _HandleArmor(DamageStats enemyDamageStats)
    {
        int armorValue = armor.GetValue();

        float damagePercent = 100f / (100f + (float)armorValue);

        enemyDamageStats.minDamage *= damagePercent;
        enemyDamageStats.maxDamage *= damagePercent;
    }

    private bool _HandleCritical(DamageStats enemyDamageStats, CharacterStats enemyStats)
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

    private void _HandlePlayerNotify(float damage, bool isMiss, bool isCritical)
    {
        if (!gameObject.CompareTag("Player"))
        {
            NotifyManager.instance.ShowAttackNotify((int)damage, isMiss, isCritical);
        }
    }

    public virtual void OnChangeHealth() { }

    public virtual void OnChangeMana() { }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
    }
}
