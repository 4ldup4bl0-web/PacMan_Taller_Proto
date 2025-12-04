using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }

    public AudioSource oneShotSource; // para sonidos puntuales (PlayOneShot)
    public AudioSource loopingSource; // para sonidos que se repiten / background 

    [Header("Clips")]
    public AudioClip pointClip;
    public AudioClip powerUpClip;
    public AudioClip enemyEliminatedClip;
    public AudioClip enemyDetectClip;
    public AudioClip dashClip;
    public AudioClip playerHurtClip;
    public AudioClip enemyMovementClip; // clip corto para reproducir a intervalos

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayPoint() => PlayOnce(pointClip);
    public void PlayPowerUp() => PlayOnce(powerUpClip);
    public void PlayEnemyEliminated() => PlayOnce(enemyEliminatedClip);
    public void PlayEnemyDetect() => PlayOnce(enemyDetectClip);
    public void PlayDash() => PlayOnce(dashClip);

    public void PlayPlayerHurt() => PlayOnce(playerHurtClip);

    public void PlayOnce(AudioClip clip)
    {
        if (oneShotSource != null && clip != null)
            oneShotSource.PlayOneShot(clip);
    }

    // Reproduce un clip corto en la fuente looping pero con PlayOneShot para que suene repetidamente sin cortar
    public void PlayEnemyMovementTick()
    {
        if (oneShotSource != null && enemyMovementClip != null)
            oneShotSource.PlayOneShot(enemyMovementClip);
    }
}