using UnityEngine;

public class Tree : MonoBehaviour
{
    public int remainingHits = 5;
    public float minimumTimeBetweenHits = 1.0f;
    private float lastHitAt;

    public void Chop()
    {
        if (Time.time < lastHitAt + minimumTimeBetweenHits) return;

        remainingHits--;
        if (remainingHits <= 0)
        {
            Destroy(gameObject);
        }
    }
}
