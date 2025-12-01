using UnityEngine;
using System.Collections;

public class EnemySoundEmitter : MonoBehaviour
{
    public float interval = 1.2f; // cada cuanto se reproduce el sfx
    public bool enabledOnStart = true;

    private Coroutine loop;

    private void OnEnable()
    {
        if (enabledOnStart)
            loop = StartCoroutine(LoopSound());
    }

    private IEnumerator LoopSound()
    {
        while (true)
        {
            if (SFXManager.Instance != null)
                SFXManager.Instance.PlayEnemyMovementTick();

            yield return new WaitForSeconds(interval);
        }
    }

    private void OnDisable()
    {
        if (loop != null) StopCoroutine(loop);
    }
}