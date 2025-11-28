using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Chainsaw chainsaw = collision.GetComponent<Chainsaw>();

            if (chainsaw != null)
            {
                chainsaw.ActivatePowerUp();
            }

            // Destruir power-up
            Destroy(gameObject);
        }
    }
}
