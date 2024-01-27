using UnityEngine;

namespace Mete.Scripts
{
    public class EnemyDamage : MonoBehaviour
    {
        [SerializeField] protected float damage;

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
                collision.GetComponent<Health.Health>().TakeDamage(damage);
        }
    }
}
