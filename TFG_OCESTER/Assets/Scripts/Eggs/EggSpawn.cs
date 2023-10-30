using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EggSpawn : MonoBehaviour
{
    
    [SerializeField] private ItemCollectableSO egg;
    [SerializeField] private Vector3 eggSize = new Vector3(2.5f, 2.5f, 0);
    private SpriteRenderer spriteRenderer;
   
    void Start()
    {
        // Se selecciona un tipo de huevo aleatoriamente
        Invoke("Activate", 5.0f);
    }
   

    // Update is called once per frame
    private void Activate()
    {
        // aquí hay que modificar la función, esto es a modo de prueba, hay que implementar con las quest que haga spawn
        
        // Se añade el Srpite renderer para que se visualice
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = egg.imgItem;
        spriteRenderer.color = Color.white;
        spriteRenderer.sortingOrder = 4;
        transform.localScale = eggSize;
        //gameObject.SetActive(true);
    }

   
}
