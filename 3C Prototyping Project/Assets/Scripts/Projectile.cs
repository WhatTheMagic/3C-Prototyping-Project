using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody projectileBody;
    [SerializeField] private GameObject damageIndicatorPrefab;
    [SerializeField] private List<Collider> myColliders = new List<Collider>();

    public void Initialize()
    {
        projectileBody.AddForce(transform.forward * 5000f + transform.up * 300f);
    }

    public void Initialize(List<Collider> collider)
    {
        foreach (var item in collider)
        {
            foreach (var itemitem in myColliders)
            {
                Physics.IgnoreCollision(item, itemitem);

            }

        }

        projectileBody.AddForce(transform.forward * 5000f + transform.up * 300f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
        GameObject damageIndicator = Instantiate(damageIndicatorPrefab);
        damageIndicator.transform.position = collision.GetContact(0).point;

        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerHealth>().TakeDamage(5);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.transform.GetComponentInParent<EnemyAI>().TakeDamage(34);
        }
        FMODUnity.RuntimeManager.PlayOneShot("event:/Bullets/GetHit/HitSounds");
        Destroy(this.gameObject);
    }
}
