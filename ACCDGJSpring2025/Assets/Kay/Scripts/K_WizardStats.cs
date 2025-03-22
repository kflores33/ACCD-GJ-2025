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
    [Tooltip("Sets the wizard's mana regeneration delay")] public float ManaRegenDelay;

    [Tooltip("Sets the rate at which the wizard's mana depletes")] public float ManaDepletionRate;
    [Tooltip("Sets the delay before mana starts depleting")] public float ManaDepletionDelay;

    [Header("Player Actions")]
    [Tooltip("Amount of damage dealt by the wizard's basic attack")] public float BasicAttackDamage;
    [Tooltip("Duration of time that 'Brace for Impact' lasts for")]public float BraceForImpactDuration;
    [Tooltip("Length of cooldown for 'Brace for Impact'")]public float BraceForImpactCooldown;
}
