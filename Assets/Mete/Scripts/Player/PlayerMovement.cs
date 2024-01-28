using System;
using UnityEngine;

namespace Mete.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpPower;
        [SerializeField] private LayerMask groundLayer;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private BoxCollider2D _boxCollider;
        private float _horizontalInput;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _animator.SetBool("IsMoving", _horizontalInput != 0);
            Flip();
            
            if (Input.GetKey(KeyCode.Space))
                Jump();
        }

        private void Flip()
        {
            if (_horizontalInput > 0.01f)
                transform.localScale = Vector3.one;
            else if (_horizontalInput < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);
        }

        private void Jump()
        {
            if (isGrounded())
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpPower);
                //anim.SetTrigger("jump");
            }
        }

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = new Vector2(_horizontalInput * speed * Time.fixedDeltaTime,
                _rigidbody2D.velocity.y);
        }

        private bool isGrounded()
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(
                _boxCollider.bounds.center, _boxCollider.bounds.size,
                0, Vector2.down, 0.1f, groundLayer);
            return raycastHit.collider != null;
        }
        
        public bool canAttack()
        {
            return _horizontalInput == 0 && isGrounded();
        }
    }
}