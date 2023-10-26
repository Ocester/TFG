using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EggCollect : MonoBehaviour
{
    
    [SerializeField] private EggSO[] eggs;
    [SerializeField] private Vector3 eggSize = new Vector3(2.5f, 2.5f, 0);
    private EggSO egg;
    private SpriteRenderer spriteRenderer;
   
    void Start()
    {
        // Se selecciona un tipo de huevo aleatoriamente
        SelectRandomEgg();
        Invoke("Activate", egg.respawnTime);
    }

    // Update is called once per frame
    private void Activate()
    {
        // aquí hay que modificar la función, esto es a modo de prueba, hay que implementar con las quest que haga spawn
        
        // Se añade el Srpite renderer para que se visualice
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = egg.imgEgg;
        spriteRenderer.color = Color.white;
        spriteRenderer.sortingOrder = 4;
        gameObject.transform.localScale = eggSize;
        //gameObject.SetActive(true);
    }

    private void SelectRandomEgg()
    {
        // Comprueba si la lista tiene elementos
        if (eggs.Length > 0)
        {
            // Selecciona un índice aleatorio dentro del rango de la lista
            int randomIndex = Random.Range(0, eggs.Length);
            // Accede al elemento aleatorio
            egg = eggs[randomIndex];
        }
        else
        {
            Debug.LogWarning("La lista de elementos está vacía.");
        }
        
    }
}
