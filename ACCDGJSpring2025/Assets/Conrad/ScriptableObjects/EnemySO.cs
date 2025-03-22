using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Monster")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public Sprite enemySprite;
    public float enemyReward;
    public float enemyDamage;
    public float enemySize;
}
