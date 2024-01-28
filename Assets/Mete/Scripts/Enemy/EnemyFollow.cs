using UnityEngine;

namespace Mete.Scripts.Enemy
{
    public class EnemyFollow : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Transform _playerReference;
        [SerializeField] private float detectionRadius;
        [SerializeField] private float minDistanceToPlayer;
        private const string PlayerTag = "Player";

        private Animator _animator;
        private MeleEnemyController _meleEnemyController;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _meleEnemyController = GetComponent<MeleEnemyController>();
            _playerReference = GameObject.FindGameObjectWithTag(PlayerTag)?.transform;
        }


      
        void Update()
        {
            if (_playerReference == null)
                return;

            float distanceToPlayer = Vector2.Distance(transform.position, _playerReference.position);

            if (distanceToPlayer <= detectionRadius && distanceToPlayer > minDistanceToPlayer)
            {
                _animator.SetBool("IsMoving", true);

                Vector2 direction = (_playerReference.position - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                transform.position = Vector3.MoveTowards(transform.position, _playerReference.position,
                    speed * Time.deltaTime);

                // Set Y rotation to zero to prevent rotation around Y-axis
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
            else
            {
                _animator.SetBool("IsMoving", false);
            }
        }


        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, minDistanceToPlayer);
        }
    }
}