using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerPlayer : MonoBehaviour
{
    private Rigidbody2D _player;
    public float _speed = 5f;
    public float _jumpForce = 10f;
    private bool _estaNoChao;
    private bool _viradoParaDireita = true;

    void Start()
    {
        _player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Pular();
    }

    void FixedUpdate()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        _player.velocity = new Vector2(movimentoHorizontal * _speed, _player.velocity.y);

        if (!_viradoParaDireita && movimentoHorizontal > 0)
        {
            Virar();
        }
        else if (_viradoParaDireita && movimentoHorizontal < 0)
        {
            Virar();
        }
    }

    void Pular()
    {
        if (Input.GetButtonDown("Jump") && _estaNoChao)
        {
            _player.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
            _estaNoChao = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            _estaNoChao = true;
        }
    }

    void Virar()
    {
        _viradoParaDireita = !_viradoParaDireita;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
}

