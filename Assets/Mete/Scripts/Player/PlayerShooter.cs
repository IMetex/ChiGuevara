using UnityEngine;

namespace Mete.Scripts.Player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private float attackCooldown;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject[] eggs;

        private Animator _animator;
        private PlayerMovement _playerMovement;
        private float _cooldownTimer = Mathf.Infinity;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && _cooldownTimer > attackCooldown && _playerMovement.canAttack())
                Attack();

            _cooldownTimer += Time.deltaTime;
        }

        private void Attack()
        {
            _animator.SetTrigger("IsAttack");
            _cooldownTimer = 0;

            eggs[FindBullet()].transform.position = spawnPoint.position;
            eggs[FindBullet()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        }

        private int FindBullet()
        {
            for (int i = 0; i < eggs.Length; i++)
            {
                if (!eggs[i].activeInHierarchy)
                    return i;
            }

            return 0;
        }
    }
}