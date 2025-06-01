using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool facingRight = true;

    // Movimento e pulo
    public float speed;
    public float jumpForce;
    private float moveInput;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;
    private bool jumpRequested = false;

    
    // Pulo sensível à duração da tecla
    private bool isJumping;
    public float jumpTime = 0.35f;
    private float jumpTimeCounter;

    // Combate
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public LayerMask whatIsEnemies;

    public Transform attackPos;
    public float attackRange;
    public int damage;

    // Dano e invencibilidade
    public float invincibilityDuration = 1f;
    private bool isInvincible;
    private float invincibilityTimer;

    public int maxHealth = 5;
    private int currentHealth;

    // Dash
    public float dashSpeed;
    public float dashDuration;
    private bool isDashing;
    private float originalGravity;
    private int extraDashes;
    public int extraDashesValue;

    // Knockback
    public float knockbackForce = 40f;
    private Vector2 knockbackVector;
    private float knockbackTimer;
    public float knockbackDuration = 0.1f;
    private bool isKnockedBack;

    void Start()
    {

        jumpTimeCounter = jumpTime;

        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        originalGravity = rb.gravityScale;
        extraJumps = extraJumpsValue;
        extraDashes = extraDashesValue;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
                isInvincible = false;
        }

        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
            extraDashes = extraDashesValue;
        }

        
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.linearVelocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            extraJumps--;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.linearVelocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }


        if (!isDashing && extraDashes > 0 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector2 dashDirection = facingRight ? Vector2.right : Vector2.left;
            StartCoroutine(Dash(dashDirection));
            extraDashes--;
        }

        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    var enemy = enemiesToDamage[i].GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                        Debug.Log("Acertou: " + enemiesToDamage[i].name);
                    }
                }
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isKnockedBack)
        {
            rb.linearVelocity = knockbackVector;
            knockbackTimer -= Time.fixedDeltaTime;
            if (knockbackTimer <= 0)
            {
                knockbackVector = Vector2.zero;
                isKnockedBack = false;
            }
            return; // teste que ignora o resto enquanto estiver em knockback
        }

        if (!isDashing)
        {
            moveInput = 0f;
            if (Input.GetKey(KeyCode.A)) moveInput = -1f;
            if (Input.GetKey(KeyCode.D)) moveInput = 1f;

            Vector2 totalVelocity = rb.linearVelocity;
            totalVelocity.x = moveInput * speed;

            if (knockbackTimer > 0)
            {
                totalVelocity += knockbackVector;
                knockbackTimer -= Time.fixedDeltaTime;
            }
            else
            {
                knockbackVector = Vector2.zero;
            }

            rb.linearVelocity = totalVelocity;

            if (!facingRight && moveInput > 0) Flip();
            else if (facingRight && moveInput < 0) Flip();

            if (!isKnockedBack && jumpRequested)
            {
                if (extraJumps > 0)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                    extraJumps--;
                }
                else if (isGrounded)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                }

                jumpRequested = false;
            }
        }
    }

    private IEnumerator Dash(Vector2 direction)
    {
        isDashing = true;
        rb.gravityScale = 0;
        rb.linearVelocity = direction.normalized * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        isDashing = false;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (!isInvincible && enemy != null)
        {
            // Dmg
            TakeDamage(enemy.damage);

            // Knockback
            Vector2 knockbackDir = (transform.position - collision.transform.position).normalized;
            knockbackVector = knockbackDir * knockbackForce;
            knockbackTimer = knockbackDuration;
            isKnockedBack = true;

            isInvincible = true;
            invincibilityTimer = invincibilityDuration;
        }
    }

    private void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player tomou dano: -" + amount + " | Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player morreu!");
        gameObject.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPos == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
