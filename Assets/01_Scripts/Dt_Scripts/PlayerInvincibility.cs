using UnityEngine;
using System.Collections;

public class PlayerInvincibility : MonoBehaviour
{
    [Header("Config")]
    public float invincibleDuration = 2f;  // Segundos de invincibilidad
    public float blinkInterval = 0.2f;     // ¿Cada cuánto parpadea?
    public bool blinkEffect = true;

    private Collider2D col;
    private SpriteRenderer sr;
    private bool isInvincible = false;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void TriggerInvincibility()
    {
        if (!isInvincible)
            StartCoroutine(InvincibilityRoutine());
    }

    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        col.enabled = false;  // Desactivar colisiones no recibe daño

        float timer = 0f;
        bool visible = true;

        while (timer < invincibleDuration)
        {
            if (blinkEffect && sr != null)
            {
                visible = !visible;
                sr.enabled = visible;
            }

            timer += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        // Restaurar
        if (sr != null) sr.enabled = true;
        col.enabled = true;
        isInvincible = false;
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }
}