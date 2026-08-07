using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float maxHealth = 50f;
    public float moveSpeed = 3.5f;
    public float detectRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public int damage = 10;
}