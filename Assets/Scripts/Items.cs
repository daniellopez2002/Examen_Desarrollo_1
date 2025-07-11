using UnityEngine;

public class Items : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && this.CompareTag("Danger"))
        {
            collision.GetComponent<CharacterController>().TakeDamage(30f);
            Destroy(gameObject); // Destroy the item after damage
        }
        else if (collision.CompareTag("Player") && this.CompareTag("Fruit"))
        {
            collision.GetComponent<CharacterController>().Score += 1; // Increase score by 1
            Destroy(gameObject); // Destroy the item after collection
        }
        else if (collision.CompareTag("Floor") && this.CompareTag("Danger"))
        {
            Destroy(gameObject); // Destroy the item if it collides with the floor
        }
        else if (collision.CompareTag("Floor") && this.CompareTag("Fruit"))
        {
            // Handle collision with enemy if needed
            GameManager.Instance.Damage(20f); // Example damage to the enemy
            Destroy(gameObject); // Destroy the item if it collides with the floor
        }
    }

}
