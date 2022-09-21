using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D _rb;

    bool _isPressingScreen;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _isPressingScreen = Input.GetMouseButton(0);
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
}
