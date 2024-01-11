using UnityEngine;
using UnityEngine.UI;
public class CloseBtn : MonoBehaviour
{
    [SerializeField] private GameObject informationPanel;
    
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(CloseSettings);
    }
    private void CloseSettings()
    {
        informationPanel.SetActive(false);
        MovementController.Instance.isMoving=true;
    }
}
