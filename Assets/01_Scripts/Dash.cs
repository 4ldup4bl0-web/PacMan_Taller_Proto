using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 60f;
    private float dashingTime = 0.35f;
    private float dashingCooldown = 5f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private float speed;
    private float horizontal;

    void Update()
    {
        if (isDashing) return;

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Q) && canDash)
        {
            Debug.Log("Q detectada y canDash = TRUE");
            StartCoroutine(DoDash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing) return;

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    IEnumerator DoDash()
    {
        Debug.Log("Dash iniciado");

        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        float dashDir = horizontal != 0 ? horizontal : Mathf.Sign(transform.localScale.x);
        rb.linearVelocity = new Vector2(dashDir * dashingPower, 0f);

        tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;

        Debug.Log("Dash terminado, cooldown...");

        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
        Debug.Log("Dash disponible otra vez");
    }
}
