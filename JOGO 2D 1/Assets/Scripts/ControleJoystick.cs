using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleJoystick : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool moveLeft;
    private bool moveRight;
    private float horizontalMove;
    public float speed = 15;
    public float jumpSpeed = 5;
    bool isGrounded;
    bool canDoubleJump;
    public float delayBeforeDoubleJump;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveLeft = false;
        moveRight = false;
    }

    // Método chamado quando o botão esquerdo é pressionado
    public void PointerDownLeft()
    {
        moveLeft = true;
    }

    // Método chamado quando o botão esquerdo é liberado
    public void PointerUpLeft()
    {
        moveLeft = false;
    }

    // Método chamado quando o botão direito é pressionado
    public void PointerDownRight()
    {
        moveRight = true;
    }

    // Método chamado quando o botão direito é liberado
    public void PointerUpRight()
    {
        moveRight = false;
    }

    // Atualiza o movimento do player a cada frame
    void FixedUpdate()
    {
        // Reseta o movimento horizontal a cada frame
        horizontalMove = 0;

        // Verifica se o botão esquerdo foi pressionado
        if (moveLeft)
        {
            horizontalMove = -speed;
        }
        // Verifica se o botão direito foi pressionado
        else if (moveRight)
        {
            horizontalMove = speed;
        }

        // Aplica o movimento horizontal
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);

        // Inverte a direção do personagem
        if (facingRight == false && horizontalMove > 0)
        {
            Flip();
        }
        else if (facingRight == true && horizontalMove < 0)
        {
            Flip();
        }
    }

    // Método para inverter a direção do personagem
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // Método chamado quando o player colide com o chão
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Chao"))
        {
            isGrounded = true;
            canDoubleJump = false;
        }
    }

    // Método para o pulo
    public void JumpButton()
    {
        if (isGrounded)
        {
            isGrounded = false;
            rb.velocity = Vector2.up * jumpSpeed; // Aplica uma força para o pulo
            Invoke("EnableDoubleJump", delayBeforeDoubleJump); // Habilita o double jump após o atraso
        }
        else if (canDoubleJump) // Verifica se o double jump é possível
        {
            rb.velocity = Vector2.up * jumpSpeed; // Aplica uma força para o double jump
            canDoubleJump = false;
        }
    }

    // Método para habilitar o double jump
    void EnableDoubleJump()
    {
        canDoubleJump = true;
    }
}
