using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public MovementController playerControllerMovement; 
    private Animator animatorController;

    // Start is called before the first frame update
    void Start()
    {
        animatorController = gameObject.GetComponent<Animator>();
    }

    // función que llamará el animation Event, que a su vez llamará al sript movement y detendrá el movimiento.
    void ToggleMovementPlayer()
    {
        playerControllerMovement.ToggleMovement();
    }
}
