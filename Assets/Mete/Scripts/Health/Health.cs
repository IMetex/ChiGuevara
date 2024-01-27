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
                animator.SetTrigger("IsHurt");
            }
            else
            {
                if (!isDead)
                {
                    animator.SetTrigger("IsDead");

                    //Deactivate all attached component classes
                    /*foreach (Behaviour component in components)
                    component.enabled = false;*/

                    isDead = true;
                }
            }

        }

        public void AddHealth(float _value)
        {
            // Collect Heart 
            currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealt);
        }
    }
}
