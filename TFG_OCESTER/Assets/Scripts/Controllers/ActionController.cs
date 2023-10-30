
using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    public Animator playerAnim;
    public Transform playerTr;
    
    [SerializeField] private ToolsSO grab;
    [SerializeField] private ToolsSO pointer;
    [SerializeField] private ToolsSO dig;
    [SerializeField] private ToolsSO cut;
    
    private MovementController movement;
    private Vector2 dist;
    private Vector2 playerPosition;
    private ToolsSO currentTool;
    private bool action = true;

    private void OnEnable()
    {
        // Se suscribe al evento OnSelectedTool
        EventManager.OnSelectedTool += SelectTool;
    }

    private void OnDisable()
    {
        EventManager.OnSelectedTool -= SelectTool;
    }
   void Start()
    {
        movement = gameObject.GetComponent<MovementController>();
        currentTool = pointer;
    }

   public ToolsSO getTool()
   {
       return currentTool;
   }
   
    public void SelectTool (ToolsSO toolSelected)
    {
        currentTool = toolSelected;
    }

    // Esta función se encarga de activar o desactivar la posibilidad de realizar la acción, dependiendo de 
    // si el objto al que se intenta golpear se realiza con la herramienta correcta o no (se controla desde script (DigItem, GrabItem, ...)
    public void SetAction(bool setAction)
    {
        action = setAction;
    }


    public void Action()
    {
        Debug.Log("Action = " + action);
        if (currentTool.action == dig.action && action) Dig();
        else if (currentTool.action == cut.action && action) Cut();
        else if (currentTool.action == grab.action && action) Grab();
        else if (currentTool.action == pointer.action && action) Point();
    }

    public void Point()
    {
        action = false;// una vez realizada se pone a false para que se compruebe de nuevo si puede realizar la acción siguiente
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPosition = new Vector2(playerTr.position.x, playerTr.position.y);
        dist = clickPosition - playerPosition;
        Debug.DrawLine(clickPosition,playerPosition,Color.red,5.0f);
        Debug.Log("Point action!");
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

    public void Grab()
    {
        action = false; // una vez realizada se pone a false para que se compruebe de nuevo si puede realizar la acción siguiente
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPosition = new Vector2(playerTr.position.x, playerTr.position.y);
        dist = clickPosition - playerPosition;
        Debug.DrawLine(clickPosition,playerPosition,Color.red,5.0f);
        
        Debug.Log("Grab action!");
    }

}
