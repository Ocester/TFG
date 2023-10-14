using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector2 MovementSpeed; 
    private Rigidbody2D playerRb; 
    private Vector2 inputVector = new Vector2(0.0f, 0.0f);
    private Animator playerAnim;

    void Awake()
    {
        // playerRb = gameObject.GetComponent<playerRb>();
       // playerRb = gameObject.AddComponent<playerRb>();
        //playerRb.angularDrag = 0.0f;
        //playerRb.gravityScale = 0.0f;
    }

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = gameObject.GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        // controlaremos los input
        // utilizamos normalized para que en diagonal no corra más
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        
        playerAnim.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        playerAnim.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        playerAnim.SetFloat("movSpeed", inputVector.magnitude);
        
    }

    void FixedUpdate()
    {
        // controlaremos las físicas
        playerRb.MovePosition(playerRb.position + (inputVector * MovementSpeed * Time.fixedDeltaTime));
    }
}
