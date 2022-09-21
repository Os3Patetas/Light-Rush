using UnityEngine;
using UnityEngine.InputSystem;

namespace com.icypeak.player 
{
    public class Player : MonoBehaviour
    {
        Rigidbody2D _rb;
        SpriteRenderer _sr;
        PlayerInputActions _inputActions;

        [SerializeField] Color particleColor;
        [SerializeField] Color waveColor;

        bool _isPressingScreen;
        bool _transformActionPressed => _inputActions.Actions.Transform.WasPerformedThisFrame();

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
            _inputActions = new PlayerInputActions();
        }

        void Start()
        {
            this.tag = "Particle";
            _sr.color = particleColor;
        }

        void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.Actions.Propulsion.started += StartReadPropulsion;
            _inputActions.Actions.Propulsion.canceled += StopReadPropulsion;
        }
        void OnDisable()
        {
            _inputActions.Actions.Propulsion.started -= StartReadPropulsion;
            _inputActions.Actions.Propulsion.canceled -= StopReadPropulsion;
            _inputActions.Disable();
        }

        void Update()
        {
            if (_transformActionPressed)
            {
                if (this.CompareTag("Particle"))
                {
                    this.tag = "Wave";
                    _sr.color = waveColor;
                }
                else
                {
                    this.tag = "Particle";
                    _sr.color = particleColor;

                }
            }
        }

        void FixedUpdate()
        {
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y, -10, 10));

            if (_isPressingScreen)
            {
                _rb.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(this.tag))
            {

            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        void StartReadPropulsion(InputAction.CallbackContext ctx) => _isPressingScreen = true;
        void StopReadPropulsion(InputAction.CallbackContext ctx) => _isPressingScreen = false;
    }
}

