using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsButton : MonoBehaviour
{
    [SerializeField] private Sprite btnImg;
    [SerializeField] private GameObject settingsMenu;
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ShowSettings);
    }

    private void ShowSettings()
    {
        Debug.Log("Muestro Settings");
        settingsMenu.SetActive(true);
    }
}
