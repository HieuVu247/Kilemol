using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;
    
    // Attack configurations
    public float normalAttackDamage = 10f;
    public float heavyAttackDamage = 20f;
    public float qBaseDamage = 15f;
    public float qDamagePerSecond = 5f;
    public float qMaxHoldTime = 3f;
    public float rBaseDamage = 30f;
    public float rDamagePerSecond = 10f;
    public float rMaxHoldTime = 5f;
    public float healAmount = 20f;

// Cooldowns
    public float leftClickCooldown = 1f;
    public float rightClickCooldown = 2f;
    public float qCooldown = 10f;
    public float eCooldown = 15f;
    public float rCooldown = 30f;

// Timers
    private float leftClickTimer = 0f;
    private float rightClickTimer = 0f;
    private float qTimer = 0f;
    private float eTimer = 0f;
    private float rTimer = 0f;

// Hold times
    private float qHoldTime = 0f;
    private float rHoldTime = 0f;

// States
    private bool isAttacking = false;
    private bool isChargingQ = false;
    private bool isChargingR = false;

// References
    private PlayerStats playerStats;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        
        leftClickTimer = Mathf.Max(leftClickTimer - Time.deltaTime, 0);
        rightClickTimer = Mathf.Max(rightClickTimer - Time.deltaTime, 0);
        qTimer = Mathf.Max(qTimer - Time.deltaTime, 0);
        eTimer = Mathf.Max(eTimer - Time.deltaTime, 0);
        rTimer = Mathf.Max(rTimer - Time.deltaTime, 0);
        
        if (!isAttacking)
        {
            // Your existing movement code here, including:
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

            bool isMoving = moveDirection != Vector2.zero;
            bool isRunning = Input.GetKey(KeyCode.LeftShift) && isMoving;

            if (moveX > 0)
            {
                facingRight = true;
            }
            else if (moveX < 0)
            {
                facingRight = false;
            }

            if (!isRunning)
            {
                transform.localScale = new Vector3(facingRight ? 1 : -1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            animator.SetBool("toWalk", isMoving && !isRunning);
            animator.SetBool("toRun", isRunning);
            animator.SetBool("facingRight", facingRight);

            float speed = isRunning ? runSpeed : (isMoving ? walkSpeed : 0);
            animator.SetFloat("moveSpeed", speed);
            rb.linearVelocity = moveDirection * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
        if (!isAttacking)
        {
            if (Input.GetMouseButtonDown(0) && leftClickTimer <= 0)
            {
                StartCoroutine(NormalAttack());
            }
            else if (Input.GetMouseButtonDown(1) && rightClickTimer <= 0)
            {
                StartCoroutine(HeavyAttack());
            }
            else if (Input.GetKeyDown(KeyCode.E) && eTimer <= 0)
            {
                StartCoroutine(Heal());
            }
        }

// Handle Q
        if (Input.GetKeyDown(KeyCode.Q) && qTimer <= 0 && !isAttacking)
        {
            isChargingQ = true;
            isAttacking = true;
            qHoldTime = 0f;
            animator.SetBool("isChargingQ", true);
        }
        if (isChargingQ)
        {
            qHoldTime += Time.deltaTime;
            if (qHoldTime > qMaxHoldTime) qHoldTime = qMaxHoldTime;
            if (Input.GetKeyUp(KeyCode.Q))
            {
                isChargingQ = false;
                animator.SetBool("isChargingQ", false);
                StartCoroutine(QAttack());
            }
        }

// Handle R
        if (Input.GetKeyDown(KeyCode.R) && rTimer <= 0 && !isAttacking)
        {
            isChargingR = true;
            isAttacking = true;
            rHoldTime = 0f;
            animator.SetBool("isChargingR", true);
        }
        if (isChargingR)
        {
            rHoldTime += Time.deltaTime;
            if (rHoldTime > rMaxHoldTime) rHoldTime = rMaxHoldTime;
            if (Input.GetKeyUp(KeyCode.R))
            {
                isChargingR = false;
                animator.SetBool("isChargingR", false);
                StartCoroutine(RAttack());
            }
        }
    }
    private IEnumerator NormalAttack()
    {
        isAttacking = true;
        animator.SetTrigger("normalAttack");
        yield return new WaitForSeconds(0.5f); // Adjust based on animation length
        DealDamage(normalAttackDamage);
        leftClickTimer = leftClickCooldown;
        isAttacking = false;
    }

    private IEnumerator HeavyAttack()
    {
        isAttacking = true;
        animator.SetTrigger("heavyAttack");
        yield return new WaitForSeconds(0.5f);
        DealDamage(heavyAttackDamage);
        rightClickTimer = rightClickCooldown;
        isAttacking = false;
    }

    private IEnumerator QAttack()
    {
        float damage = qBaseDamage + qHoldTime * qDamagePerSecond;
        animator.SetTrigger("qAttack");
        yield return new WaitForSeconds(0.5f);
        ReleaseSwordEnergy(damage);
        qTimer = qCooldown;
        isAttacking = false;
    }

    private IEnumerator RAttack()
    {
        float damage = rBaseDamage + rHoldTime * rDamagePerSecond;
        animator.SetTrigger("rAttack");
        yield return new WaitForSeconds(0.5f);
        PerformArcSlash(damage);
        rTimer = rCooldown;
        isAttacking = false;
    }

    private IEnumerator Heal()
    {
        isAttacking = true;
        animator.SetTrigger("heal");
        yield return new WaitForSeconds(0.5f);
        playerStats.Heal(healAmount);
        eTimer = eCooldown;
        isAttacking = false;
    }

// Placeholder functions
    private void DealDamage(float damage)
    {
        Debug.Log("Dealing damage: " + damage);
        // Implement actual damage logic here
    }

    private void ReleaseSwordEnergy(float damage)
    {
        Debug.Log("Releasing sword energy with damage: " + damage);
        // Implement sword energy logic here
    }

    private void PerformArcSlash(float damage)
    {
        Debug.Log("Performing arc slash with damage: " + damage);
        // Implement arc slash logic here
    }
}