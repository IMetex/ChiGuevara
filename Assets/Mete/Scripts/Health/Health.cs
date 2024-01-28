using System.Collections;
using UnityEngine;

namespace Mete.Scripts.Health
{
    public class Health : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private float startingHealt;

        [Header("Components")]
        [SerializeField] private Behaviour[] components;
    
        private Animator animator;
        public bool isDead; // GameOver
        public float currentHealth { get; private set; }

        public SpriteRenderer _Sprite;

        private void Awake()
        {
            currentHealth = startingHealt;
            animator = GetComponent<Animator>();
        }


        public void TakeDamage(float _damage)
        {
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealt);

            if (currentHealth > 0)
            {
                // player hit
                StartCoroutine(FlashSpriteColor(Color.red, 0.5f));
            }
            else
            {
                if (!isDead)
                {
                //    animator.SetTrigger("IsDead");
                
                    Destroy(gameObject);

                    isDead = true;
                }
            }

        }

        private IEnumerator FlashSpriteColor(Color flashColor, float duration)
        {
            _Sprite.color = flashColor;
            
            yield return new WaitForSeconds(duration);
            
            _Sprite.color = Color.white;
        }
    }
}
