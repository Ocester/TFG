using UnityEngine;
using UnityEngine.UI;
using Wilberforce;

public class UIController : MonoBehaviour
{
    private GameObject[] _toolBtns;
    [SerializeField]private GameObject _uiToolsBar;
    [SerializeField]private GameObject _uiToolsBarM;
    [SerializeField]private GameObject _MainCamera;
    private Sprite _currentToolSprite;
    public UIElementsSize currentUIElementsSize;
    public static UIController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        // Se suscribe al evento OnSelectedTool
        EventController.OnSelectedTool += ToggleSelection;
        EventController.OnChangeAccessibility += ChangeUISize;
    }
    private void OnDisable()
    {
        EventController.OnSelectedTool -= ToggleSelection;
        EventController.OnChangeAccessibility -= ChangeUISize;
    }

    void Start()
    {
        // Se buscan todos los botones de la tool bar
        _toolBtns = GameObject.FindGameObjectsWithTag("toolBtn");
        currentUIElementsSize = UIElementsSize.S;
    }
    private void ToggleSelection(ToolsSO selectedTool)
    {
        foreach (var tool in _toolBtns)
        {
            if (tool.GetComponent<ToolSelection>().tool.action.ToString() != selectedTool.action.ToString())
            {
                tool.GetComponent<Image>().color = Color.white;
            }
            else
            {
                _currentToolSprite = selectedTool.imgAction;
                Cursor.SetCursor(_currentToolSprite.texture, Vector2.zero, CursorMode.Auto);
            }
        }
    }
    private void ResetToolSelection()
    {
        foreach (var tool in _toolBtns)
        {
            tool.GetComponent<Image>().color = Color.white;
        }
    }
    
    public void DeactivateTools()
    {
        switch (currentUIElementsSize)
        {
            case UIElementsSize.S:
                _uiToolsBar.SetActive(false);
                return;
            case UIElementsSize.M:
                _uiToolsBarM.SetActive(false);
                return;
        }
    }
    public void ActivateTools()
    {
        switch (currentUIElementsSize)
        {
            case UIElementsSize.S:
                _uiToolsBar.SetActive(true);
                return;
            case UIElementsSize.M:
                _uiToolsBarM.SetActive(true);
                return;
        }
    }

    private void ChangeUISize(UIElementsSize newUIElementsSize)
    {
        currentUIElementsSize = newUIElementsSize;
        switch (currentUIElementsSize)
        {
            case UIElementsSize.S:
                _uiToolsBar.SetActive(true);
                _uiToolsBarM.SetActive(false);
                break;
            case UIElementsSize.M:
                _uiToolsBarM.SetActive(true);
                _uiToolsBar.SetActive(false);
                break;
        }
        _toolBtns = null;
        _toolBtns = GameObject.FindGameObjectsWithTag("toolBtn");
        ResetToolSelection();
    }

    public void ChangeColorblindMode(UIColorblindMode colorblindMode)
    {
        EventController.SelectedColorblindMode(colorblindMode);
        switch (colorblindMode)
        {
            case UIColorblindMode.Base:
                _MainCamera.GetComponent<Colorblind>().Type = 0;
                break;
            case UIColorblindMode.Protanopia:
                _MainCamera.GetComponent<Colorblind>().Type = 1;
                break;
            case UIColorblindMode.Deutanopia:
                _MainCamera.GetComponent<Colorblind>().Type = 2;
                break;
            case UIColorblindMode.Tritanopia:
                _MainCamera.GetComponent<Colorblind>().Type = 3;
                break;
        }
    }
    
    public enum UIColorblindMode
    {
        Base,
        Protanopia,
        Tritanopia,
        Deutanopia
    }
    
    public enum UIElementsSize
    {
        S,
        M,
    }

}
