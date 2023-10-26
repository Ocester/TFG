using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform playerTracking; // Asigna el objeto del personaje que se desea seguir en el Inspector
    [SerializeField] private Vector3 offset;// Un offset opcional para ajustar la posición de la cámara

    private void Start()
    {
        offset = new Vector3(0f, 0f, -10f);
    }

    private void LateUpdate()
    {
        if (playerTracking)
        {
            // Se obtiene la posición del player y se aplica el offset
            Vector3 newPosition = playerTracking.position + offset;
            
            // Se asigna la nueva posición a la cámara
            transform.position = newPosition;
        }
    }
}
