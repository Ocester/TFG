using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseBtn : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(CloseSettings);
    }

    private void CloseSettings()
    {
        settingsMenu.SetActive(false);
        
    }

}
