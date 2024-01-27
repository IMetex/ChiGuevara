using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Hareket hızı
    public float moveSpeed = 5f;

    // Zıplama kuvveti
    public float jumpForce = 10f;

    // Zemin kontrolü için boş bir GameObject kullanın
    public Transform groundCheck;

    // Zeminin layer'ı
    public LayerMask groundLayer;

    // Ateş etme başlangıç noktası
    public Transform firePoint;

    // Ateş mermisi prefab'ı
    public GameObject bulletPrefab;

    // Zeminde mi kontrolü
    private bool isGrounded;

    // Double jump yapabilir mi kontrolü
    private bool canDoubleJump;

    // Dash yapılıyor mu kontrolü
    private bool isDashing;

    // Dash kuvveti
    public float dashForce = 20f;

    // Dash süresi
    public float dashDuration = 0.2f;

    // Dash cooldown süresi
    public float dashCooldown = 2f;

    // Dash cooldown süresini takip eden timer
    private float dashCooldownTimer;

    // Animator component'ini tutmak için
    private Animator animator;

    // Fiziksel özellikleri kontrol etmek için
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Çift zıplama kontrolünü başlangıçta true olarak ayarla
        canDoubleJump = true;

        // Dash cooldown süresini başlangıçta sıfırla
        dashCooldownTimer = 0f;

        // Animator component'ini al
        animator = GetComponent<Animator>();

        // Fiziksel özellikleri kontrol etmek için Rigidbody component'ini al
        rb = GetComponent<Rigidbody>();

        // Çift zıplama animasyonunu sıfırla
        animator.SetBool("IsDoubleJumping", false);
    }

    // Update is called once per frame
    void Update()
    {
        // Zeminde olup olmadığını kontrol et
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);

        // Yatay hareket
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f);
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Zıplama
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }
        }

        // Çift zıplama animasyonu
        if (Input.GetButtonDown("Jump") && !isGrounded && canDoubleJump)
        {
            DoubleJump();
        }

        // Ateş etme
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        // Dash
        if (Input.GetButtonDown("Dash") && !isDashing && dashCooldownTimer <= 0f)
        {
            StartCoroutine(Dash());
        }

        // Dash cooldown süresini güncelle
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    // Zıplama işlemi
    void Jump()
    {
        // Zıplama hızını sıfırla ve zıplama kuvvetini uygula
        rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // Çift zıplama işlemi
    void DoubleJump()
    {
        // Çift zıplama animasyonunu oynat
        animator.SetBool("IsDoubleJumping", true);

        // Zıplama hızını sıfırla ve zıplama kuvvetini uygula
        rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Çift zıplama yeteneğini kapat
        canDoubleJump = false;
    }

    // Ateş etme işlemi
    void Shoot()
    {
        // Ateş mermisini oluştur ve ateş etme noktasına yerleştir
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    // Dash işlemi
    IEnumerator Dash()
    {
        isDashing = true;
        float dashTime = 0f;

        while (dashTime < dashDuration)
        {
            rb.velocity = new Vector3(transform.localScale.x * dashForce, rb.velocity.y);
            dashTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        dashCooldownTimer = dashCooldown;
    }
}
