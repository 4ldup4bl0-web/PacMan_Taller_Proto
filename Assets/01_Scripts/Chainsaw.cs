using UnityEngine;

public class Chainsaw : MonoBehaviour
{
    [Header("Power Up")]
    public bool hasPowerUp = false;
    public float powerUpDuration = 8f;
    public GameObject PowerUp;

    private float timer;

    [Header("Animación")]
    public Animator animator;

    [Header("Velocidad")]
    public float boostedSpeed = 10f;      // velocidad aumentada
    private float originalSpeed;          // velocidad real del jugador
    public Movement playerMovement;       // referencia al script de movimiento

    [Header("Sprites")]
    public Sprite powerUpSprite;          // sprite potenciado
    private Sprite defaultSprite;         // sprite normal

    private SpriteRenderer sr;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();
        defaultSprite = sr.sprite;

        PowerUp.SetActive(false);

    }

    private void Start()
    {
        // Guardamos la velocidad inicial del movimiento del jugador
        if (playerMovement != null)
            originalSpeed = playerMovement.speed;
    }

    private void Update()
    {
        if (hasPowerUp)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                PowerUp.SetActive(false);
                DeactivatePowerUp();
            }
        }
    }

    public void ActivatePowerUp()
    {
        hasPowerUp = true;
        timer = powerUpDuration;
        PowerUp.SetActive(true);

        // Activar animación especial
        animator.SetBool("IsPowered", true);

        // Cambiar velocidad del jugador
        if (playerMovement != null)
            playerMovement.speed = boostedSpeed;

        // Cambiar sprite a potenciado
        sr.sprite = powerUpSprite;

        Debug.Log("Power-Up ACTIVADO");
    }

    private void DeactivatePowerUp()
    {
        hasPowerUp = false;

        // Volver a la animación normal
        animator.SetBool("IsPowered", false);

        // Restaurar velocidad original
        if (playerMovement != null)
            playerMovement.speed = originalSpeed;

        // Restaurar sprite normal
        sr.sprite = defaultSprite;

        Debug.Log("Power-Up TERMINÓ");
    }
}
