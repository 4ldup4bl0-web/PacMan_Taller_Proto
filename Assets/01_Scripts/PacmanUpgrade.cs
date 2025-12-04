using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class PacmanUpgrade : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite powerUpSprite;

    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private Movement movement;
    private Animator animator;

    private bool isPoweredUp = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();

        spriteRenderer.sprite = normalSprite;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            movement.SetDirection(Vector2.up);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            movement.SetDirection(Vector2.down);
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            movement.SetDirection(Vector2.left);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            movement.SetDirection(Vector2.right);

        if (movement.direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
            transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        }
    }

    public void ActivatePowerUp()
    {
        if (isPoweredUp) return;

        isPoweredUp = true;

        if (animator != null)
            animator.enabled = false; // ✅ evita que tape el sprite

        spriteRenderer.sprite = powerUpSprite;

        StartCoroutine(PowerUpTimer());
    }

    private IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(8f); // ✅ duración del power-up

        DeactivatePowerUp();
    }

    public void DeactivatePowerUp()
    {
        isPoweredUp = false;
        spriteRenderer.sprite = normalSprite;

        if (animator != null)
            animator.enabled = true; // ✅ lo restauramos
    }

    public void ResetState()
    {
        enabled = true;
        spriteRenderer.enabled = true;
        circleCollider.enabled = true;
        movement.ResetState();
        gameObject.SetActive(true);
        DeactivatePowerUp();
    }

    public void DeathSequence()
    {
        enabled = false;
        spriteRenderer.enabled = false;
        circleCollider.enabled = false;
        movement.enabled = false;
    }
}