using UnityEngine;

public class EnemyBoss : Enemy
{
    public float attackCooldown = 2f;
    private float nextAttackTime;

    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Debug.Log("Boss atacou!");
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public override void Die()
    {
        Debug.Log("O Boss foi derrotado!");
        base.Die();
    }
}
