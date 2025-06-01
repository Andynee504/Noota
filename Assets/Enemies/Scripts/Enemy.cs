using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float moveSpeed = 2f;
    public int damage = 1;

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }
}
