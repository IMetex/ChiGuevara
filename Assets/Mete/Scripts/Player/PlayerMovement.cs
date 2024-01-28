using System;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dashSpeed = 15f; // Dash hızı
    public float dashDuration = 0.2f; // Dash süresi
    private bool isJumping = false;
    private int jumpCount = 0;
    private bool canDash = true; // Dash yapma izni kontrolü
    public GameObject bulletPrefab; // Ateş objesi prefab'ı
    public float minYPosition = -5f; // Alt sınır

    private bool canFire = true; // Fire yapma izni kontrolü
    private Animator _animator;
    public SpriteRenderer sprite;
    public Transform _spawnPos;
    public float attackCooldown;
    public float minXPosition = -10f;
    public float maxXPosition = 10f;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Sağa ve sola hareket
        float horizontalInput = Input.GetAxis("Horizontal");
        
        Vector2 moveDirection = new Vector2(horizontalInput, 0f);

        if (horizontalInput > 0 || horizontalInput < 0)
        {
            // Sprite left and right move
            sprite.flipX = horizontalInput < 0 ? true : false;
            _animator.SetBool("IsMoving", true);
        }
        else
        {
            // Idle Animation
            _animator.SetBool("IsMoving", false);
        }

        // Dash kontrolü
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        // Normal hareket veya dash hareketi
        float currentMoveSpeed = canDash ? dashSpeed : moveSpeed;
        transform.Translate(moveDirection * (currentMoveSpeed * Time.deltaTime));
        

        // Ateş etme
        if (Input.GetKey(KeyCode.Space) && canFire)
        {
            // Yatay gidişatı belirle (sağa veya sola)
            float bulletDirection = 0f;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                bulletDirection = -1f; // Sola ateş et
                _animator.SetTrigger("IsAttack");
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                bulletDirection = 1f; // Sağa ateş et
                _animator.SetTrigger("IsAttack");
            }

            if (bulletDirection != 0f)
            {
                Fire(bulletDirection);
                canFire = false; // Fire yapıldığında izni kapat

                // 0.5 saniye sonra izni tekrar aç
                Invoke("EnableFire", attackCooldown);
            }
        }

        // Havalanma
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 2)
        {
            Jump();
            _animator.SetTrigger("Jump");
            jumpCount++;
        }

        // Alt sınıra ulaşıldığında düşmeyi engelleme
        if (transform.position.y < minYPosition)
        {
            transform.position = new Vector2(transform.position.x, minYPosition);
            if (isJumping)
            {
                isJumping = false;
                jumpCount = 0;
            }
        }
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, jumpForce);
        isJumping = true;
    }

    void Fire(float bulletDirection)
    {
        
        GameObject bullet = Instantiate(bulletPrefab, _spawnPos.position, Quaternion.identity);
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();

        // Ateş objesini belirli bir yönde hareket ettir
        bulletScript.SetDirection(bulletDirection);
    }

    void EnableFire()
    {
        canFire = true; // Fire iznini aç
    }

    // Zeminle temas ettiğinde
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Graund"))
        {
            isJumping = false;
            jumpCount = 0;
        }
    }

    IEnumerator Dash()
    {
        canDash = false; // Dash iznini kapat
        float originalSpeed = moveSpeed; // Orijinal hareket hızını sakla

        // Dash süresince hızı arttır
        moveSpeed = dashSpeed;

        // Dash süresi boyunca beklet
        yield return new WaitForSeconds(dashDuration);

        // Dash sona erdiğinde hızı eski değerine geri getir
        moveSpeed = originalSpeed;

        canDash = true; // Dash iznini aç
    }
}