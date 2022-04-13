using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float JumpForce = 10;
    public float Velocity = 10;
    public int mode = 0;

    private SpriteRenderer _spriteRenderer; // null
    private Rigidbody2D _rb;
    private Animator _animator;

    private static readonly string ANIMATOR_STATE = "State";
    private static readonly int ANIMATION_IDLE = 0;
    private static readonly int ANIMATION_RUN = 2;
    private static readonly int ANIMATION_JUMP = 1;

    private static int LEFT = -1;

    public bool JumpFlag = false;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 0)
        {
            ChangeAnimation(ANIMATION_IDLE);
        }
        else if (mode == 1)
        {
            Desplazarse();
        }
        else if (mode == 2)
        {
            if (JumpFlag)
            {
                Saltar();
            }
        }
        else if (mode == 3)
        {
            if (JumpFlag)
            {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
                Saltar();
                Direccion(LEFT);
                LEFT = LEFT * -1;
            }
        }
        /*switch (mode)
        {
            case 0:
                ChangeAnimation(ANIMATION_IDLE);
                break;
            case 1:
                Desplazarse();
                break;
            case 2:
                if (JumpFlag)
                {
                    Saltar();
                }
                break;
            case 3:
                if (JumpFlag)
                {
                    _rb.velocity = new Vector2(0, _rb.velocity.y);
                    Saltar();
                    Direccion(LEFT);
                    LEFT = LEFT * -1;
                }
                break;
        }*/
    }

    private void Desplazarse()
    {
        ChangeAnimation(ANIMATION_RUN);
    }
    private void Direccion(int direccion)
    {
        var aux = Velocity * direccion;
        this._rb.AddForce(new Vector2(aux,0), ForceMode2D.Impulse);
    }
    private void Saltar()
    {
        this._rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        ChangeAnimation(ANIMATION_JUMP);
        JumpFlag = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Piso")
        {
            JumpFlag = true;
        }
    }
    private void ChangeAnimation(int animation)
    {
        _animator.SetInteger(ANIMATOR_STATE, animation);
    }
}
