using UnityEngine;
using UnityEngine.UI;
public class SettingsButton : MonoBehaviour
{
   [SerializeField] private GameObject settingsMenu;
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ShowSettings);
    }

    private void ShowSettings()
    {
        settingsMenu.SetActive(true);
    }
}
