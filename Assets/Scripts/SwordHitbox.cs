using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public Collider hitCollider;
    public int damage = 25;
    public float activeTime = 0.2f;

    private void Reset()
    {
        hitCollider = GetComponent<Collider>();
        if (hitCollider) hitCollider.isTrigger = true;
    }

    private void Start()
    {
        if (hitCollider) hitCollider.enabled = false;
    }

    public void TriggerHit()
    {
        StopAllCoroutines();
        StartCoroutine(HitRoutine());
    }

    System.Collections.IEnumerator HitRoutine()
    {
        if (hitCollider) hitCollider.enabled = true;
        yield return new WaitForSeconds(activeTime);
        if (hitCollider) hitCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var e = other.GetComponent<EnemySimple>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }
}
