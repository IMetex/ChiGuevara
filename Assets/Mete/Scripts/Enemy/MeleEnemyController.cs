using UnityEngine;

namespace Mete.Scripts
{
    public class MeleEnemyController : MonoBehaviour
    {
        [Header("Attack Parameters")] [SerializeField]
        private float attackCooldown;

        [SerializeField] private float range;
        [SerializeField] private int damage;


        [Header("Collider Parameters")]
        [SerializeField] private float colliderDistance;

        [SerializeField] private BoxCollider2D boxCollider;


        [Header("Player Layer")] [SerializeField]
        private LayerMask playerLayer;

        private float _cooldownTimer = Mathf.Infinity;


        //Referances
        private Animator _anim;
        private Health.Health playerHealth;
        private EnemyPatrol enemyPatrol;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            enemyPatrol = GetComponentInParent<EnemyPatrol>();
        }

        private void Update()
        {
            _cooldownTimer += Time.deltaTime;
            
            //Attack only when player in sight?
            if (PlayerInSight())
            {
                if (_cooldownTimer >= attackCooldown)
                {
                    _cooldownTimer = 0;
                    _anim.SetTrigger("IsMeleAttack");
                    Debug.Log("Work");
                }
            }

            /*if (enemyPatrol != null)
                enemyPatrol.enabled = !PlayerInSight();*/
        }

        private bool PlayerInSight()
        {
            RaycastHit2D hit =
                Physics2D.BoxCast(
                    boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                    new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                    0, Vector2.left, 0, playerLayer);

            if (hit.collider != null)
                playerHealth = hit.transform.GetComponent<Health.Health>();

            return hit.collider != null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(
                boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        }

        private void DamagePlayer()
        {
            // animation ref
            // If player still in range damage 
            if (PlayerInSight())
                playerHealth.TakeDamage(damage);
        }
    }
}