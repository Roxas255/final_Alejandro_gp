using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float speedIncrement = 2f;
    public Transform player;
    public GameObject bloodEffectPrefab;
    
    private Vector3 originalPosition;
    private bool isAlive = true;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();            
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); 
    }

    void Update()
    {    // Monsters follow the player 
        if (isAlive && player != null)
        {
            MoveTowardPlayer();
        }
    }

    void MoveTowardPlayer()
    {
        Vector2 direction = ((Vector2)player.position - rb.position).normalized;
        Vector2 newPos = rb.position + direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(newPos);

        
        spriteRenderer.flipX = direction.x > 0;

        // Play walk animation 
        if (animator != null)
            animator.Play("Walk"); 
    }

    public void Defeat()
    {
        if (!isAlive) return; 

        isAlive = false;

        
        rb.linearVelocity = Vector2.zero;

        // Add score
        Score.Instance?.AddScore(1);

        // Play blood effect
        if (bloodEffectPrefab != null)
        {
            GameObject blood = Instantiate(bloodEffectPrefab, transform.position, Quaternion.identity);
            Destroy(blood, 2f);
        }

        
       
        
        Invoke(nameof(HideAfterDeath), 0.8f); 
    }

    void HideAfterDeath()
    {
        gameObject.SetActive(false);
        Invoke(nameof(Respawn), 2f);
    }

    void Respawn()
    {    //Spawn the monster back to where they originally spawned 
        transform.position = originalPosition;
        moveSpeed += speedIncrement;
        isAlive = true;
        gameObject.SetActive(true);
    }
}