using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    // VARIABLES AÑADIDAS PARA EL POWER-UP
    public Sprite normalSprite;      // Asigna el sprite normal en el Inspector
    public Sprite powerUpSprite;     // Asigna el sprite potenciado en el Inspector
    
    [SerializeField]
    //private AnimatedSprite deathSequence;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private Movement movement;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        movement = GetComponent<Movement>();
        
        // Inicializar con el sprite normal
        spriteRenderer.sprite = normalSprite;
    }

    private void Update()
    {
        // ... (Lógica de movimiento existente) ...
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.SetDirection(Vector2.right);
        }

        // Rotate pacman to face the movement direction
        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }
    
    // NUEVO MÉTODO PÚBLICO: Cambia el sprite del jugador
    public void SetPowerUpVisuals(bool active)
    {
        if (active)
        {
            spriteRenderer.sprite = powerUpSprite;
        }
        else
        {
            spriteRenderer.sprite = normalSprite;
        }
    }
    
    // ... (El resto de métodos ResetState y DeathSequence permanecen iguales) ...

    public void ResetState()
    {
        enabled = true;
        spriteRenderer.enabled = true;
        circleCollider.enabled = true;
        //deathSequence.enabled = false;
        movement.ResetState();
        gameObject.SetActive(true);
        
        // Asegurar que el sprite sea el normal al resetear
        spriteRenderer.sprite = normalSprite; 
    }

    public void DeathSequence()
    {
        enabled = false;
        spriteRenderer.enabled = false;
        circleCollider.enabled = false;
        movement.enabled = false;
        //deathSequence.enabled = true;
        //deathSequence.Restart();
    }
}