using UnityEngine;
using UnityEngine.UI;

public class AccessibilityButton : MonoBehaviour
{
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite disabledSprite;
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ChangeUI);
    }

    private void ChangeUI()
    {
        if (UIController.Instance.currentUIElementsSize == UIController.UIElementsSize.S)
        {
            gameObject.GetComponent<Image>().sprite = activeSprite;
            EventController.SelectedChangeAccessibilityEvent(UIController.UIElementsSize.M);
            return;
        }
        gameObject.GetComponent<Image>().sprite = disabledSprite;
        EventController.SelectedChangeAccessibilityEvent(UIController.UIElementsSize.S);
    }
}
