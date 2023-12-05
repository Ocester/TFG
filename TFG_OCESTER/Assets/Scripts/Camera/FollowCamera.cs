using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform playerTracking; // Asigna el objeto del personaje que se desea seguir en el Inspector
    [SerializeField] private Vector3 offset;// Un offset opcional para ajustar la posición de la cámara
    private Camera _camera;
    private float zoomSpeed;
    public float originalZoom;
    private float maxZoom;
    private float scrollWheel;

    private void Start()
    {
        offset = new Vector3(0f, 0f, -10f);
        _camera = gameObject.GetComponent<Camera>();
        zoomSpeed = 15f;
        originalZoom = _camera.orthographicSize;
        maxZoom = 12f;
    }
    void Update()
    {
        if (Input.mouseScrollDelta.y != 0f)
        { 
            scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            // Ajusto el orthographicSize para simular el zoom
            _camera.orthographicSize -= scrollWheel * zoomSpeed * Time.deltaTime;
            // Limito el tamaño para evitar zoom por exceso y por defecto
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, originalZoom, maxZoom);
            //EventController.HintRequestEvent();
        }
        
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
