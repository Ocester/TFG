using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pruebaControlador : MonoBehaviour
{
    public Vector2 MovementSpeed = new Vector2();
    public Rigidbody2D playerRb; 
    public Animator playerAnim;
    public bool isMoving = true;
   private Vector2 dist;
    private Vector2 playerPosition;
    private Vector2 inputVector = new Vector2(0.0f, 0.0f);
    
    private void Start()
    {
        
    }

    void Update()
    {
        // Se controla los input del teclado
        // Se utiliza normalized para que en diagonal no corra más
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        
        playerAnim.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        playerAnim.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        playerAnim.SetFloat("movSpeed", inputVector.magnitude);
        
        if (Input.GetMouseButtonDown(0))
        {
            Dig();
        }

    }
    void FixedUpdate()
    {
        if (isMoving)
        {
            playerRb.MovePosition(playerRb.position + (inputVector * MovementSpeed * Time.fixedDeltaTime));
        }

    }
    
    public void Dig()
    {
        ToggleMovement();
        // Se calcula la diferencia de posición (dist) entre el clic y el Player. Se compara la magnitud de dist.x y dist.y para saber si el clic
        // está a la derecha, izquierda, arriba o abajo del Player.
        
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPosition = new Vector2(transform.position.x, transform.position.y);
        dist = clickPosition - playerPosition;
        
        if (Mathf.Abs(dist.x) > Mathf.Abs(dist.y))
        {
            if (dist.x > 0)
            { playerAnim.Play("Dig_right"); }
            else
            { playerAnim.Play("Dig_left"); }
        }
        else
        {
            if (dist.y > 0)
            { playerAnim.Play("Dig_up"); }
            else
            { playerAnim.Play("Dig_down"); }
        }
    }
    
    // cambio de estado Mover a No Mover para paralizar movimiento si realizo alguna acción con el ratón
    public void ToggleMovement()
    {
        isMoving = !isMoving;
        Debug.Log(" Entro ");
    }
}
