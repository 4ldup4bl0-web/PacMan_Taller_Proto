using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;          
    public float speed = 3f;          
    public float stopDistance = 1.5f; 
    public float obstacleDetectionDistance = 1f; 
    public LayerMask obstacleMask;    

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, obstacleDetectionDistance, obstacleMask);

        if (hit.collider != null)
        {
            rb.linearVelocity = Vector2.zero;
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
        else
        {

            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                rb.linearVelocity = direction * speed;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }

            Debug.DrawRay(transform.position, direction * obstacleDetectionDistance, Color.green);
        }

        if (rb.linearVelocity.x > 0.1f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (rb.linearVelocity.x < -0.1f)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
