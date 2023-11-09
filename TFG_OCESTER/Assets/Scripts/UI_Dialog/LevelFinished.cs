using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinished : MonoBehaviour
{
    private void Start()
    {
        EventController.OnFinishLevel += ShowInformation;
    }
    private void OnDisable()
    {
        EventController.OnFinishLevel -= ShowInformation;
    }

    private void ShowInformation()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(true);

        }
        
    }
}
