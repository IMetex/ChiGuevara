using UnityEngine;

namespace Mete.Scripts.Ground
{
    public class GroundCheck : MonoBehaviour
    {
        private BoxCollider2D coll;
        [SerializeField] private LayerMask Grounded;
        void Awake()
        {
            coll = GetComponent<BoxCollider2D>();
        }
    
        public  bool IsGrounded()
        {
        
            return Physics2D.BoxCast(coll.bounds.center, 
                coll.bounds.size, 0f, Vector2.down, .1f, Grounded);
        }
    }
}
