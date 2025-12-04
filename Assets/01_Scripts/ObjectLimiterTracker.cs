using UnityEngine;

public class ObjectLimiterTracker : MonoBehaviour
{
    public ObjectLimiter limiter;

    private void OnDestroy()
    {
        if (limiter != null)
            limiter.OnObjectDestroyed();
    }
}
