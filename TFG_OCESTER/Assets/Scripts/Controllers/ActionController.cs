using System.Collections;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    private Animator _playerAnim;
    private Transform _playerTr;
    private Collider2D _playerCollider;
    [SerializeField] private ToolsSO grab;
    [SerializeField] private ToolsSO pointer;
    [SerializeField] private ToolsSO dig;
    [SerializeField] private ToolsSO cut;
    [SerializeField] private ToolsSO speak;
    private Sprite _pointerSprite;
    private Vector2 _dist;
    private Vector2 _playerPosition;
    private ToolsSO _currentTool;
    [SerializeField]private bool actionPermitted;
    [SerializeField] private Sprite charImg;
    private ItemCollectableSO _item;
    private bool _startQuest, _completeQuest;

    public static ActionController Instance;
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
        EventController.OnSelectedTool += SelectTool;
        EventController.OnFinishLevel += FinishLevel;
    }

    private void FinishLevel()
    {
        Cursor.SetCursor(pointer.imgAction.texture, Vector2.zero, CursorMode.Auto);
    }

    private void OnDisable()
    {
        EventController.OnSelectedTool -= SelectTool;
        EventController.OnFinishLevel -= FinishLevel;
    }

    private void Start()
   {
       _playerAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();
       _playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
       _playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
       _currentTool = pointer;
       actionPermitted = true;
   }

   public ToolsSO GetTool()
   {
       return _currentTool;
   }
   
    public void SelectTool (ToolsSO toolSelected)
    {
        _currentTool = toolSelected;
        // cada vez que se cambia el tool se bloquea la acción hasta que no se compruebe que se puede
        // realizar con el nuevo tool seleccionado
        actionPermitted = false;
        
        // Se obtine la posición del collider y se mueve lo mínimo para que detecte el cambio de tool
        // el collider, ya que o collider Stay no lo gestiona como necesito.
        Vector2 colliderPositon = _playerCollider.offset;
        float newPos = 0.1f; 
        _playerCollider.offset = new Vector2(colliderPositon.x + newPos, colliderPositon.y);
        _playerCollider.offset = colliderPositon;
    }
    
    // Esta función se encarga de activar o desactivar la posibilidad de realizar la acción, dependiendo de 
    // si el objeto al que se intenta golpear se realiza con la herramienta correcta o no (se controla desde script (DigItem, GrabItem, ...)
    public void SetAction(bool isActionPermitted)
    {
        actionPermitted = isActionPermitted;
    }

    public void Action()
    {
        if (_currentTool.action == dig.action && actionPermitted) Dig();
        else if (_currentTool.action == cut.action && actionPermitted) Cut();
        else if (_currentTool.action == grab.action && actionPermitted) Grab();
        else if (_currentTool.action == pointer.action && actionPermitted) Point();
        else if (_currentTool.action == speak.action && actionPermitted) Speak();
    }

    private void Point()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider)
        {
            EventController.ChangeDialogPicEvent(charImg);
            EventController.PointObjectWriteEvent(hit.collider.gameObject.name);
            EventController.PointObjectSoundEvent(MusicController.ActionSound.PointSound);
            actionPermitted = false;
        }
        
    }

    private void Grab()
    {
        actionPermitted = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        
        if (!hit.collider || !hit.collider.gameObject.GetComponent<GrabItem>().GetItem())
        {
            return;
        }
        EventController.GrabObjectSoundEvent(MusicController.ActionSound.GrabSound);
        _item = hit.collider.gameObject.GetComponent<GrabItem>().GetItem();
        QuestController.Instance.GetItem(_item);
        hit.collider.gameObject.SetActive(false);
        if (_item.respawnTime==0) return;
        StartCoroutine(RespawnItem(hit.collider.gameObject, _item.respawnTime));
    }
    IEnumerator RespawnItem(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Respawn(obj);
    }
    private void Respawn(GameObject itemToRespawn)
    {
        itemToRespawn.SetActive(true);
    }

    private void Speak()
    {
        actionPermitted = false;// una vez realizada se pone a false para que se compruebe de nuevo si puede realizar la acción siguiente
        if (_startQuest)
        {
            EventController.QuestIconDeactivateEvent(QuestController.Instance.GetCurrentQuest());
            QuestController.Instance.StartQuest();
            _startQuest = false;
            return;
        }
        if (_completeQuest)
        {
            EventController.CompleteQuestEvent(QuestController.Instance.GetCurrentQuest());
            _completeQuest=false;
        }
    }
    public void SetStartQuest ()
    {
        _startQuest = true;
    }
    public void SetCompleteQuest()
    {
        _completeQuest=true;
    }

    private void Dig()
    {
        actionPermitted = false;// una vez realizada se pone a false para que se compruebe de nuevo si puede realizar la acción siguiente
        if (MovementController.Instance.isMoving)
        {
            MovementController.Instance.ToggleMovement();
        }
        
        // Se calcula la diferencia de posición (dist) entre el clic y el Player. Se compara la magnitud de dist.x y dist.y para saber si el clic
        // está a la derecha, izquierda, arriba o abajo del Player.
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _playerPosition = new Vector2(_playerTr.position.x, _playerTr.position.y);
        _dist = clickPosition - _playerPosition;
        
        if (Mathf.Abs(_dist.x) > Mathf.Abs(_dist.y))
        {
            if (_dist.x > 0)
            { _playerAnim.Play("Dig_right"); }
            else
            { _playerAnim.Play("Dig_left"); }
        }
        else
        {
            if (_dist.y > 0)
            { _playerAnim.Play("Dig_up"); }
            else
            { _playerAnim.Play("Dig_down"); }
        }
        
    }

    private void Cut()
    {
        actionPermitted = false; // una vez realizada se pone a false para que se compruebe de nuevo si puede realizar la acción siguiente
        if (MovementController.Instance.isMoving)
        {
            MovementController.Instance.ToggleMovement();
        }
        
        // Se calcula la diferencia de posición (dist) entre el clic y el Player. Se compara la magnitud de dist.x y dist.y para saber si el clic
        // está a la derecha, izquierda, arriba o abajo del Player.
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _playerPosition = new Vector2(_playerTr.position.x, _playerTr.position.y);
        _dist = clickPosition - _playerPosition;
        
        if (Mathf.Abs(_dist.x) > Mathf.Abs(_dist.y))
        {
            if (_dist.x > 0)
            { _playerAnim.Play("Cut_right"); }
            else
            { _playerAnim.Play("Cut_left"); }
        }
        else
        {
            if (_dist.y > 0)
            { _playerAnim.Play("Cut_up"); }
            else
            { _playerAnim.Play("Cut_down"); }
        }
    }
}
