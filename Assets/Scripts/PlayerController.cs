using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float JumpForce = 10;
    public float Velocity = 10;

    private SpriteRenderer _spriteRenderer; // null
    private Rigidbody2D _rb;
    private Animator _animator;

    private static readonly string ANIMATOR_STATE = "State";
    private static readonly int ANIMATION_RUN = 0;
    private static readonly int ANIMATION_JUMP = 1;
    private static readonly int ANIMATION_DEAD = 2;

    private static bool Muerto = false;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var velocidadActualY = _rb.velocity.y;

        Desplazarse();

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Saltar();
        }

        if (Muerto)
        {
            IsDead();
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Enemy")
        {
            Muerto = true;
        }
    }

    private void Desplazarse()
    {
        _rb.velocity = new Vector2(Velocity, _rb.velocity.y);
        ChangeAnimation(ANIMATION_RUN);
    }

    private void Saltar()
    {
        _rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        ChangeAnimation(ANIMATION_JUMP);
    }

    private void IsDead()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        ChangeAnimation(ANIMATION_DEAD);
    }

    private void ChangeAnimation(int animation)
    {
        _animator.SetInteger(ANIMATOR_STATE, animation);
    }
}
