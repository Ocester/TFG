using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Hit : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator playerAnim;
    private Vector2 dist;
    private Vector2 playerPosition;
    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            // Se finaliza cualquier animación en curso
            
            playerAnim.StopPlayback();
            
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
        
        
    }
}