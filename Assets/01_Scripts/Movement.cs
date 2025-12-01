using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8f;
    public float speedMultiplier = 1f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    public Rigidbody2D rgby { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }


    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 60f;
    public float dashingTime = 0.35f;
    public float dashingCooldown = 5f;

    [SerializeField] private TrailRenderer tr;

    private float horizontal;
    private float vertical;


    private Vector2 lastDirection = Vector2.right;


    private int normalLayer;
    [SerializeField] private string dashLayerName = "GhostDash";

    private void Awake()
    {
        rgby = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        normalLayer = gameObject.layer; 
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        speedMultiplier = 1f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startingPosition;
        rgby.isKinematic = false;
        rgby.bodyType = RigidbodyType2D.Dynamic;
        enabled = true;
    }

    private void Update()
    {
        if (isDashing) return;

        if (nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");


        if (Mathf.Abs(horizontal) > 0 && Mathf.Abs(vertical) == 0)
            lastDirection = new Vector2(horizontal, 0);
        else if (Mathf.Abs(vertical) > 0 && Mathf.Abs(horizontal) == 0)
            lastDirection = new Vector2(0, vertical);

        // Activar dash
        if (Input.GetKeyDown(KeyCode.Q) && canDash)
        {
            StartCoroutine(DoDash());
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rgby.position;
        Vector2 translation = speed * speedMultiplier * Time.fixedDeltaTime * direction;

        rgby.MovePosition(position + translation);
        if (isDashing) return;

    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position,
            Vector2.one * 0.75f,
            0f,
            direction,
            1.5f,
            obstacleLayer
        );

        return hit.collider != null;
    }

    IEnumerator DoDash()
    {
        canDash = false;
        isDashing = true;

        tr.emitting = true;


        gameObject.layer = LayerMask.NameToLayer(dashLayerName);

        float originalGravity = rgby.gravityScale;
        rgby.gravityScale = 0f;

        rgby.linearVelocity = lastDirection * dashingPower;

        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;
        rgby.gravityScale = originalGravity;
        isDashing = false;

        gameObject.layer = normalLayer;

        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }
}
