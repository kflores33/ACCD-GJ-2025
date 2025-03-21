using UnityEngine;

[CreateAssetMenu(fileName = "K_WizardStats", menuName = "Scriptable Objects/K_WizardStats")]
public class K_WizardStats : ScriptableObject
{
    [Header("Movement")]
    [Tooltip("Sets the wizard's move speed"), Range(5,50)]public float Speed;
    [Tooltip("Sets the wizard's acceleration")]public float Acceleration;
    [Tooltip("Sets the wizard's deceleration")] public float Deceleration;

    [Header("Mana")]
    [Tooltip("Sets the wizard's max mana")] public float MaxMana;
    [Tooltip("Sets the wizard's mana regeneration rate")] public float ManaRegenRate;
    [Tooltip("Sets the rate at which the wizard's mana depletes")] public float ManaDepletionRate;

    [Header("Misc")]
    [Tooltip("Amount of damage dealt by the wizard's basic attack")] public float BasicAttackDamage;
}
