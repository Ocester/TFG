using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prueba : MonoBehaviour
{
    public CollectableVegetablesSO vegetal;
    private SpriteRenderer img;
    
    // Start is called before the first frame update
    private void Start()
    {
        img = gameObject.GetComponent<SpriteRenderer>();
        img.sprite = vegetal.aubergineImg;
    }
    
}
