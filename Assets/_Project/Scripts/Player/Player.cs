using com.icypeak.managers;
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

        bool _isPressingScreen = false;
        bool _alternateStateActionExecuted => _inputActions.Actions.Transform.WasPerformedThisFrame();
        bool _isDead = false;

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
            GetComponent<SpriteRenderer>().sortingOrder = 1;
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
            if (_alternateStateActionExecuted)
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
            if (!collision.CompareTag(this.tag))
            {
                _isDead = true;
                Destroy(this.gameObject);
            }
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            if (!collision.CompareTag(this.tag))
            {
                _isDead = true;
                Destroy(this.gameObject);
            }    
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(this.tag) && _isDead == false)
            {
                ScoreManager.Instance.Score++;
                DifficultyManager.Instance.IncreaseDifficulty();
            }
        }

        void StartReadPropulsion(InputAction.CallbackContext ctx) => _isPressingScreen = true;
        void StopReadPropulsion(InputAction.CallbackContext ctx) => _isPressingScreen = false;
    }
}

