using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementController : MonoBehaviour
{
    public Vector2 MovementSpeed = new Vector2(5f,5f);
    //public GameObject player;
    public Rigidbody2D playerRb; 
    public Animator playerAnim;
    public bool isMoving = true;
    private ActionController executeAction;
    private Vector2 dist;
    private Vector2 playerPosition;
    private Vector2 inputVector = new Vector2(0.0f, 0.0f);
    
    private void Start()
    {
        executeAction = gameObject.GetComponent<ActionController>();
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
            //executeAction.Dig();
            executeAction.Cut();
        }
    }
    void FixedUpdate()
    {
        
        if (isMoving)
        {
            playerRb.MovePosition(playerRb.position + (inputVector * MovementSpeed * Time.fixedDeltaTime));
        }
    }
    
    // cambio de estado Mover a No Mover para paralizar movimiento si realizo alguna acción con el ratón
    public void ToggleMovement()
    {
        isMoving = !isMoving;
    }
  
}
