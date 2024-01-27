using UnityEngine;

namespace Mete.Scripts
{
    public class EnemyPatrol : MonoBehaviour
    {
        [Header("Patrol Points")] [SerializeField]
        private Transform leftEdge;

        [SerializeField] private Transform rightEdge;
        
        [Header("Enemy")] [SerializeField] private Transform enemy;
        
        [Header("Movement Parameters")] [SerializeField]
        private float enemySpeed;

        private Vector3 initScale;
        public bool movingLeft;


        [Header("Idle Behaviour")] [SerializeField]
        private float idleDuration;

        private float idleTimer;


        [Header("Enemy Animator")] [SerializeField]
        private Animator anim;

        public bool isPlayer = false;


        private void Awake()
        {
            initScale = enemy.localScale;
        }

        private void OnDisable()
        {
            anim.SetBool("IsMoving", false);
        }

        private void Update()
        {
            if (!isPlayer)
            {
                if (movingLeft)
                {
                    if (enemy.position.x >= leftEdge.position.x)
                        MoveInDirection(-1);
                    else
                        DirectionChange();
                }
                else
                {
                    if (enemy.position.x <= rightEdge.position.x)
                        MoveInDirection(1);
                    else
                        DirectionChange();
                }
            }
        }

        private void DirectionChange()
        {
            anim.SetBool("IsMoving", false);
            idleTimer += Time.deltaTime;

            if (idleTimer > idleDuration)
                movingLeft = !movingLeft;
        }

        private void MoveInDirection(int _direction)
        {
            idleTimer = 0;
            anim.SetBool("IsMoving", true);

            // Move enemy face direction
            enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
                initScale.y, initScale.z);

            // Move in that direction
            enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * enemySpeed,
                enemy.position.y, enemy.position.z);
        }
    }
}