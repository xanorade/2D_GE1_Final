using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    AudioManager audioManager;

    // current player stats
    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;
    float currentMagnet;

    #region Current Stats Properties

    // property for current health, updates the UI when health changes
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "Health: " + currentHealth;
                }
            }
        }
    }

    // property for current recovery rate (not used in game but would used in future updates maybe)
    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            if (currentRecovery != value)
            {
                currentRecovery = value;
            }
        }
    }

    // property for current move speed
    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
            }
        }
    }

    // property for current might (not used too)
    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            if (currentMight != value)
            {
                currentMight = value;
            }
        }
    }

    // property for current projectile speed (not used too)
    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
            }
        }
    }

    // property for current magnet (pickup radius)
    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            if (currentMagnet != value)
            {
                currentMagnet = value;
            }
        }
    }
    #endregion

    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;
    public int experienceCapIncrease;

    PlayerCollector collector;

    void Awake()
    {
        // initializing the player's current stats from the character data
        CurrentHealth = characterData.MaxHealth;
        CurrentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentMight = characterData.Might;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
        CurrentMagnet = characterData.Magnet;

        // get the PlayerCollector component and set its radius
        collector = GetComponentInChildren<PlayerCollector>();
        collector.SetRadius(characterData.Magnet);

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        // assigning the character's UI and update initial values
        GameManager.instance.AssignChosenCharacterUI(characterData);
        UpdateHealthBar();
        UpdateExpBar();
        UpdateLevelText();
    }

    void Update()
    {
        // managing invincibility timer (or your health bar melts immediately)
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }
        
        Recover();
    }

    // increasing experience and checking for level up
    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
        UpdateExpBar();
    }

    // checks if the player has enough experience to level up
    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;
            experienceCap += experienceCapIncrease;

            if (GameManager.instance != null)
            {
                GameManager.instance.currentLevel.text = "Current Level: " + level;
            }
            if (GameManager.instance != null)
            {
                GameManager.instance.collectedExperience.text = "Needed Experience: " + (experienceCap - experience);
            }

            UpdateLevelText();
            ProjectileWeaponBehaviour.IncreasePierce(1); // increase the pierce value of projectiles
        }
    }

    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    [Header("UI")]
    public Image healthBar;
    public Image expBar;
    public TMPro.TMP_Text levelText;

    // taking damage and updating health
    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            CurrentHealth -= dmg;

            if (CurrentHealth <= 0)
            {
                audioManager.PlaySFX(audioManager.death);
                Kill();
            }
            audioManager.PlaySFX(audioManager.damageTaken);
            UpdateHealthBar();
        }
    }

    // experience bar fill amount updater
    void UpdateExpBar()
    {
        expBar.fillAmount = (float)experience / experienceCap;
    }

    // level text updater
    void UpdateLevelText()
    {
        levelText.text = "LV " + level.ToString();
    }

    // health bar fill amount updater
    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / characterData.MaxHealth;
    }

    // recover health over time
    void Recover()
    {
        if (CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;

            if (CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }

            UpdateHealthBar();
        }
    }

    // gameover checker
    public void Kill()
    {
        if (!GameManager.instance.isGameOver)
        {

            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.GameOver();
        }
    }
}
