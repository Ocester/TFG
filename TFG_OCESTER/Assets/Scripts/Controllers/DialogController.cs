using System.Collections;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    [SerializeField] private GameObject dialogObjet;
    [SerializeField] private GameObject charSpeakImg;
    [SerializeField] private GameObject charSpeakNameText;
    [SerializeField] private GameObject arrowSprite;
    [SerializeField] private TextMeshProUGUI _textUI;
    [SerializeField]private NpcSO player;
    private string [] _dialogText;
    private int _lineIndex;
    private bool _dialogStarted;
    public bool _dialogFinished;
    private bool _lineFinished;
    private bool _pointerTextWritten;
   
    
    public static DialogController Instance;
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
    private void Start()
    {
        EventController.DialogTextWrite += WriteText;
        EventController.DialogTextWrite += ChangeCharSpeakText;
        EventController.PointObjectWrite += WritePointerText;
        EventController.ChangeDialogPic += ChangeCharacterPic;
        _textUI.enabled = false;
        _dialogStarted = false;
        _dialogFinished = true;
        _lineFinished = false;
        _pointerTextWritten = false;
    }

    private void OnDisable()
    {
        EventController.DialogTextWrite -= WriteText;
        EventController.DialogTextWrite -= ChangeCharSpeakText;
        EventController.PointObjectWrite -= WritePointerText;
        EventController.ChangeDialogPic -= ChangeCharacterPic;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _dialogStarted && !_dialogFinished && _lineFinished)
        {
            NextLine();
        }else if (Input.GetMouseButtonDown(0) && _dialogStarted && _dialogFinished && !_pointerTextWritten)
        {
            EndDialog();
        }
    }

    private void EndDialog()
    {
        dialogObjet.GetComponent<SpriteRenderer>().enabled=false;
        arrowSprite.SetActive(false);
        _textUI.enabled = false;
        _dialogStarted = false;
        _pointerTextWritten = false;
        if (QuestController.Instance.allQuestsFinished)
        {
            EventController.FinishLevelEvent();
            EventController.FinishLevelSoundEvent(MusicController.ActionSound.EndLevelSound);
        }
    }
    private void ChangeCharacterPic(Sprite charImg)
    {
        charSpeakImg.GetComponent<SpriteRenderer>().sprite = charImg;
    }
    private void WritePointerText(string txt)
    {
        dialogObjet.GetComponent<SpriteRenderer>().enabled = true;
        _textUI.enabled = true;
        _textUI.text = txt;
        _pointerTextWritten = true;
        charSpeakNameText.GetComponent<TextMeshProUGUI>().text = player.nameNpc;
    }
    
    private void NextLine()
    {
        _lineFinished = false;
        _lineIndex++;
        if (_lineIndex < _dialogText.Length)
        {
            StartCoroutine(WriteLine());
        }
        else
        {
            _dialogFinished = true;
            UIController.Instance.ActivateTools();
            EndDialog();
        }
    }

    private IEnumerator WriteLine( )
    {   
        Time.timeScale = 0f;
        _textUI.text = string.Empty;
        foreach (char c in _dialogText[_lineIndex])
        {
            _textUI.text += c;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        if (_lineIndex == _dialogText.Length-1)
        {
            arrowSprite.SetActive(false);
        }
        else if (_lineIndex < _dialogText.Length-1)
        {
            arrowSprite.SetActive(true);
        }
        _lineFinished = true;
        Time.timeScale = 1f;
    }
    
    private void WriteText(QuestSO quest)
    {
        if (!_dialogFinished)
        {
            return;
        }
        EventController.DialogSoundEvent(MusicController.ActionSound.DialogSound);
        UIController.Instance.DeactivateTools();
        ChangeCharacterPic(quest.startingNPC.imgNpc);
        arrowSprite.SetActive(false);
        _dialogStarted = true;
        _dialogFinished = false;
        _lineIndex = 0;
        if (quest.finished)
        {
            _dialogText = quest.finishedQuestText;
        }
        else if (quest.itemsColected)
        {
            _dialogText = quest.allIngredientsQuestText;
        }
        else
        {
            _dialogText = quest.npcText;
        }
        dialogObjet.GetComponent<SpriteRenderer>().enabled=true;
        _textUI.enabled = true;
        StartCoroutine(WriteLine());
    }

    private void ChangeCharSpeakText(QuestSO quest)
    {
        charSpeakNameText.GetComponent<TextMeshProUGUI>().text = quest.startingNPC.nameNpc;
    }
}
