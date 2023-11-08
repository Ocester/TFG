
using System;
using Unity.VisualScripting;
using UnityEditor;
using System.Collections;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    public Animator playerAnim;
    public Transform playerTr;
    public Collider2D playerCollider;
    [SerializeField] private ToolsSO grab;
    [SerializeField] private ToolsSO pointer;
    [SerializeField] private ToolsSO dig;
    [SerializeField] private ToolsSO cut;
    [SerializeField] private ToolsSO speak;
    private MovementController movement;
    private QuestController questController;
    private Vector2 dist;
    private Vector2 playerPosition;
    private ToolsSO currentTool;
    [SerializeField]private bool action = true;
    [SerializeField] private Sprite CharImg;
    private bool isGrabable = false;
    private ItemCollectableSO item;
    public static ActionController instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
    }

    private void OnDisable()
    {
        EventController.OnSelectedTool -= SelectTool;
    }
   void Start()
    {
        movement = gameObject.GetComponent<MovementController>();
        questController = GameObject.FindObjectOfType<QuestController>();
        currentTool = pointer;
    }

   public ToolsSO getTool()
   {
       return currentTool;
   }
   
    public void SelectTool (ToolsSO toolSelected)
    {
        currentTool = toolSelected;
        // cada vez que se cambia el tool se bloquea la acción hasta que no se compruebe que se puede
        // realizar con el nuevo tool seleccionado
        action = false;
        
        // Se obtine la posición del collider y se mueve lo mínimo para que detecte el cambio de tool
        // el collider, ya que o collider Stay no lo gestiona como necesito.
        Vector2 colliderPositon = playerCollider.offset;
        float newPos = 0.1f; 
        playerCollider.offset = new Vector2(colliderPositon.x + newPos, colliderPositon.y);
        playerCollider.offset = colliderPositon;
    }
    
    // Esta función se encarga de activar o desactivar la posibilidad de realizar la acción, dependiendo de 
    // si el objeto al que se intenta golpear se realiza con la herramienta correcta o no (se controla desde script (DigItem, GrabItem, ...)
    public void SetAction(bool setAction)
    {
        action = setAction;
        Debug.Log("Set Action -> " + action);
    }

    public void Action()
    {
        //Debug.Log("Action -> " + action);
        if (currentTool.action == dig.action && action) Dig();
        else if (currentTool.action == cut.action && action) Cut();
        else if (currentTool.action == grab.action && action) Grab();
        else if (currentTool.action == pointer.action && action) Point();
        else if (currentTool.action == speak.action && action) Speak();
    }

    public void Point()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider)
        {
            EventController.ChangeDialogPicEvent(CharImg);
            EventController.WriteDialogTextEvent(hit.collider.gameObject.name);
            action = false;
        }
        
    }
    public void Grab()
    {
        action = false;
        Debug.Log("Grab Action!!");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        
        if (!hit.collider || !hit.collider.gameObject.GetComponent<GrabItem>().GetItem())
        {
            Debug.Log("No hit collider Grab.");
            return;
        }
        item = hit.collider.gameObject.GetComponent<GrabItem>().GetItem();
        questController.GetItem(item);
        //Destroy(hit.collider.gameObject);
        hit.collider.gameObject.SetActive(false);
        if (item.respawnTime==0) return;
        StartCoroutine(RespawnItem(hit.collider.gameObject, item.respawnTime));
    }
    IEnumerator RespawnItem(GameObject obj, float delay)
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(delay);

        // Llamar a la función con el parámetro
        Respawn(obj);
    }
    private void Respawn(GameObject itemToRespawn)
    {
        itemToRespawn.SetActive(true);
    }

    public void Speak()
    {
        action = false;// una vez realizada se pone a false para que se compruebe de nuevo si puede realizar la acción siguiente
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPosition = new Vector2(playerTr.position.x, playerTr.position.y);
        dist = clickPosition - playerPosition;
        Debug.DrawLine(clickPosition,playerPosition,Color.red,5.0f);
        //Debug.Log("Speak action!");
        
    }

    public void Dig()
    {
        action = false;// una vez realizada se pone a false para que se compruebe de nuevo si puede realizar la acción siguiente
        movement.ToggleMovement();
        
        // Se calcula la diferencia de posición (dist) entre el clic y el Player. Se compara la magnitud de dist.x y dist.y para saber si el clic
        // está a la derecha, izquierda, arriba o abajo del Player.
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPosition = new Vector2(playerTr.position.x, playerTr.position.y);
        dist = clickPosition - playerPosition;
        
        Debug.DrawLine(clickPosition,playerPosition,Color.red,5.0f);
        
        if (Mathf.Abs(dist.x) > Mathf.Abs(dist.y))
        {
            if (dist.x > 0)
            { playerAnim.Play("Dig_right"); }
            else
            { playerAnim.Play("Dig_left"); }
        }
        else
        {
            if (dist.y > 0)
            { playerAnim.Play("Dig_up"); }
            else
            { playerAnim.Play("Dig_down"); }
        }
        
    }
    
    public void Cut()
    {
        action = false; // una vez realizada se pone a false para que se compruebe de nuevo si puede realizar la acción siguiente
        movement.ToggleMovement();
        
        // Se calcula la diferencia de posición (dist) entre el clic y el Player. Se compara la magnitud de dist.x y dist.y para saber si el clic
        // está a la derecha, izquierda, arriba o abajo del Player.
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPosition = new Vector2(playerTr.position.x, playerTr.position.y);
        dist = clickPosition - playerPosition;
        
        Debug.DrawLine(clickPosition,playerPosition,Color.red,5.0f);
        
        if (Mathf.Abs(dist.x) > Mathf.Abs(dist.y))
        {
            if (dist.x > 0)
            { playerAnim.Play("Cut_right"); }
            else
            { playerAnim.Play("Cut_left"); }
        }
        else
        {
            if (dist.y > 0)
            { playerAnim.Play("Cut_up"); }
            else
            { playerAnim.Play("Cut_down"); }
        }
        
    }

    

}
