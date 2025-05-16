using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Testing
public class Player : MonoBehaviour
{
    public Transform attackPointLeft;
    public Transform attackPointRight;
    public float attackRange = 1f;
    public LayerMask enemyLayers;

    private bool canAttack = true;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!canAttack) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spriteRenderer.flipX = true; // Face left
            StartCoroutine(Attack(attackPointLeft));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            spriteRenderer.flipX = false; // Face right
            StartCoroutine(Attack(attackPointRight));
        }
    }

    IEnumerator Attack(Transform attackPoint)
    {
        canAttack = false;

        //Trigger attack animation
        animator.SetTrigger("1Attack");

        // Hit check
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        bool hitSomething = false;

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Monster>()?.Defeat();
            hitSomething = true;
        }

        // Wait for attack animation to finish
        yield return new WaitForSeconds(0.5f);

        // If no enemies hit, wait an extra second before attacking again
        if (!hitSomething)
        {
            yield return new WaitForSeconds(0.5f); // Add delay on miss
        }

        canAttack = true;
    }

    void OnDrawGizmosSelected()
    {     
        if (attackPointLeft != null)
            Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
        if (attackPointRight != null)
            Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
    }

    void OnTriggerEnter2D(Collider2D other)
    {      // When Player Dies
        if (other.CompareTag("Monster"))
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}