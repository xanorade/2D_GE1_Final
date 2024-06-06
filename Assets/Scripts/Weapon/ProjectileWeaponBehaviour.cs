using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData; // reference to the weapon data scriptable object

    protected Vector3 direction; // direction of the projectile

    public float destroyAfterSeconds; // projectile destroy time

    // current stats of the projectile
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    public static int bonusPierce; // static variable to hold bonus pierce value for all projectiles

    void Awake()
    {
        // initializing current stats from the weapon data
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce + bonusPierce; // adds bonus pierce
    }

    // get the current damage of the projectile
    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight; // Multiply damage by player's might
    }

    // method to increase the bonus pierce value
    public static void IncreasePierce(int amount)
    {
        bonusPierce += amount;
    }

    // called when the projectile is instantiated
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds); // destroy the projectile after a certain duration
    }

    // called when the projectile collides with another collider
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        // if the collider belongs to an enemy, deal damage and reduce pierce
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
            ReducePierce(); 
        }
    }

    // reducing pierce value and destroy the projectile if its 0
    void ReducePierce()
    {
        currentPierce--; 
        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
