using UnityEngine;

namespace com.icypeak.obstacle
{
    public class Obstacle : MonoBehaviour
    {
        Rigidbody2D _rb;

        [SerializeField] float _baseSpeed;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();    
        }

        void Start()
        {
            _rb.velocity = Vector2.left * _baseSpeed;
        }
    }
}
