using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableBase : MonoBehaviour
{
    private Rigidbody rb;
  //  public int amount;
    public enum collectableTypes
    {
        Null,
        Log,
        Egg
    }
   
    
    public collectableTypes collectableType;
    public virtual void  Start()
    {
        rb = GetComponent<Rigidbody>();
        ThrowOnSpawn();
    }
    protected virtual void Update()
    {
        Rotate();
    }

    public virtual void Collected()
    {

    }
   
    private void Rotate()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
    }
    private void ThrowOnSpawn()
    {
        rb.AddRelativeForce(new Vector3(Random.Range(-70f, 70f), 150f, Random.Range(-70f, 70f)));
    }

    public IEnumerator AnimateCollect()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = PlayerMovement.instance.transform.position;
        Vector3 controlPoint = (startPosition + endPosition) / 2 + Vector3.up * 2f; // Parabol için kontrol noktası
        float duration = 0.8f; // Animasyon süresi
        float elapsedTime = 0f;

        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Parabol interpolasyonu (quadratic Bezier curve)
            Vector3 currentPosition =
                Mathf.Pow(1 - t, 2) * startPosition +
                2 * (1 - t) * t * controlPoint +
                Mathf.Pow(t, 2) * endPosition;

            // Ölçek interpolasyonu
            Vector3 currentScale = Vector3.Lerp(startScale, endScale, t);

            // Pozisyon ve ölçeği uygula
            transform.position = currentPosition;
            transform.localScale = currentScale;

            yield return null; // Bir sonraki frame'e geç
        }
        Sound.instance.CollectSound();
        Destroy(gameObject);

    }
}
