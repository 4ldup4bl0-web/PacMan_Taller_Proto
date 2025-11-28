using UnityEngine;
using UnityEngine;

public class Chainsaw : MonoBehaviour
{
    [Header("Power Up")]
    public bool hasPowerUp = false;
    public float powerUpDuration = 8f;

    private float timer;

    [Header("Animación")]
    public Animator animator;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hasPowerUp)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                DeactivatePowerUp();
            }
        }
    }

    public void ActivatePowerUp()
    {
        hasPowerUp = true;
        timer = powerUpDuration;

        // Activar animación especial
        animator.SetBool("IsPowered", true);

        Debug.Log("Power-Up ACTIVADO");
    }

    private void DeactivatePowerUp()
    {
        hasPowerUp = false;

        // Volver a la animación normal
        animator.SetBool("IsPowered", false);

        Debug.Log("Power-Up TERMINÓ");
    }
}
