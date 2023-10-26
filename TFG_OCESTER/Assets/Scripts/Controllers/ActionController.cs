
using System;
using UnityEditor;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    public Animator playerAnim;
    public Transform playerTr;
    public CutSelection cutSelected;
    public DigSelection digSelected;
    public GrabSelection grabSelected;
    public PointerSelection pointerSelected;
    
    [SerializeField] private ToolsSO tools;
    private MovementController movement;
    private Vector2 dist;
    private Vector2 playerPosition;
    private string currentTool;

   void Start()
    {
        movement = gameObject.GetComponent<MovementController>();
        cutSelected.OnCutSelectedTool += SelectTool;
        digSelected.OnDigSelectedTool += SelectTool;
        grabSelected.OnGrabSelectedTool += SelectTool;
        pointerSelected.OnPointerSelectedTool += SelectTool;
        currentTool = tools.pointer;
    }

   public string getTool()
   {
       return currentTool;
   }

   public void Action()
    {
        if (currentTool == tools.dig) Dig();
        else if (currentTool == tools.cut) Cut();
        else if (currentTool == tools.grab) Grab();
        else if (currentTool == tools.pointer) Point();
    }
    public void SelectTool (string arg)
    {
        currentTool = arg;
    }

    public void Point()
    {
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPosition = new Vector2(playerTr.position.x, playerTr.position.y);
        dist = clickPosition - playerPosition;
        Debug.DrawLine(clickPosition,playerPosition,Color.red,5.0f);
        
        Debug.Log("Point action!");
    }

    public void Dig()
    {
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
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPosition = new Vector2(playerTr.position.x, playerTr.position.y);
        dist = clickPosition - playerPosition;
        Debug.DrawLine(clickPosition,playerPosition,Color.red,5.0f);
        
        Debug.Log("Grab action!");
    }

}
