using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private GameObject[] _toolBtns;
    private GameObject _uiToolsBar;
    private Sprite _currentToolSprite;
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
    }

    private void OnDisable()
    {
        EventController.OnSelectedTool -= ToggleSelection;
    }

    void Start()
    {
        // Se buscan todos los botones de la tool bar
        _toolBtns = GameObject.FindGameObjectsWithTag("toolBtn");
        _uiToolsBar = GameObject.FindGameObjectWithTag("UI_ToolsBar");
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
    public void DeactivateTools()
    {
        _uiToolsBar.SetActive(false);
    }
    public void ActivateTools()
    {
        _uiToolsBar.SetActive(true);
    }

}
