using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    private MovementController movement;
    public Animator playerAnim;
    private Vector2 dist;
    private Vector2 playerPosition;

    void Start()
    {
        movement = gameObject.GetComponent<MovementController>();
    }
    public void Dig()
    {
        movement.ToggleMovement();
        
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
    
    public void Cut()
    {
        movement.ToggleMovement();
        
        // Se calcula la diferencia de posición (dist) entre el clic y el Player. Se compara la magnitud de dist.x y dist.y para saber si el clic
        // está a la derecha, izquierda, arriba o abajo del Player.
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPosition = new Vector2(transform.position.x, transform.position.y);
        dist = clickPosition - playerPosition;
        
        if (Mathf.Abs(dist.x) > Mathf.Abs(dist.y))
        {
            if (dist.x > 0)
            { playerAnim.Play("Cut_right"); }
            else
            { playerAnim.Play("Cut_left"); }
        }
        else
        {
            if (dist.y > 0)
            { playerAnim.Play("Cut_up"); }
            else
            { playerAnim.Play("Cut_down"); }
        }
        
    }
    
    
    
}
