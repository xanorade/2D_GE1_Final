using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;
    float currentMagnet;

    #region Current Stats Properties

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

    void Awake()
    {
        CurrentHealth = characterData.MaxHealth;
        CurrentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentMight = characterData.Might;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
        CurrentMagnet = characterData.Magnet;
    }
    void Start() 
    {
        GameManager.instance.AssignChosenCharacterUI(characterData);

        UpdateHealthBar();
        UpdateExpBar();
        UpdateLevelText(); 
    }

    void Update() 
    {
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

    public void IncreaseExperience(int amount)
    {
        experience += amount;

        LevelUpChecker();

        UpdateExpBar();
    }

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


    public void TakeDamage(float dmg) 
    {
        if (!isInvincible) 
        {

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            CurrentHealth -= dmg;
            if (CurrentHealth <= 0)
            {
                Kill();
            }

            UpdateHealthBar();
        }
    }

    void UpdateExpBar() 
    {
        expBar.fillAmount = (float)experience / experienceCap;
    }

    void UpdateLevelText() 
    {
        levelText.text = "LV " + level.ToString();
    }


    void UpdateHealthBar() 
    {
        healthBar.fillAmount = currentHealth / characterData.MaxHealth;
    }

    void Recover() 
    {
        if (CurrentHealth < characterData.MaxHealth) 
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;

            if (CurrentHealth > characterData.MaxHealth) 
            { 
            CurrentHealth = characterData.MaxHealth;
            }
        }
    }

    public void Kill() 
    {
        if (!GameManager.instance.isGameOver) 
        {
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.GameOver();
        }
    }



}
