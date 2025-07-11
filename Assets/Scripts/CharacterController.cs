using System.Runtime.CompilerServices;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Speed of character movement
    public float Health = 100f; // Health of the character
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public int Score = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        anim.SetBool("IsWalking", movement != 0);
        if (movement < 0) spriteRenderer.flipX = true;
        else if (movement > 0) spriteRenderer.flipX = false;
        
        Vector3 moveDirection = new Vector3(movement, 0, 0);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        anim.SetTrigger("IsHit");
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle character death (e.g., play animation, disable controls)
        Debug.Log("Character died");
    }
}
