using UnityEngine;

public class EnemyBat : Enemy
{
    private float flyHeight = 1f;
    private float startY;
    private float frequency = 2f;

    private void Start()
    {
        startY = transform.position.y;
    }

    private void Update()
    {
        float offsetY = Mathf.Sin(Time.time * frequency) * flyHeight;
        transform.position += Vector3.left * moveSpeed * Time.deltaTime + Vector3.up * offsetY * Time.deltaTime;
    }
}