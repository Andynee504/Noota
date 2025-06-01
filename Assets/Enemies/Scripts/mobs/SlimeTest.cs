using UnityEngine;

public class EnemySlime : Enemy
{
    private void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}