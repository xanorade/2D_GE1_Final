using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObjects/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    // icon representing the character
    [SerializeField]
    Sprite icon;
    public Sprite Icon { get => icon; private set => icon = value; }

    // name of the character
    [SerializeField]
    new string name;
    public string Name { get => name; private set => name = value; }

    // starting weapon of the character
    [SerializeField]
    GameObject startingWeapon;
    public GameObject StartingWeapon { get => startingWeapon; private set => startingWeapon = value; }

    // maximum health of the character
    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    // recovery rate of the character
    [SerializeField]
    float recovery;
    public float Recovery { get => recovery; private set => recovery = value; }

    // movement speed of the character
    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    // might attribute of the character
    [SerializeField]
    float might;
    public float Might { get => might; private set => might = value; }

    // mrojectile speed of the character
    [SerializeField]
    float projectileSpeed;
    public float ProjectileSpeed { get => projectileSpeed; private set => projectileSpeed = value; }

    // magnet attribute of the character
    [SerializeField]
    float magnet;
    public float Magnet { get => magnet; private set => magnet = value; }
}
