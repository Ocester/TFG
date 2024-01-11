using UnityEngine;
using UnityEngine.EventSystems;

public class MovementController : MonoBehaviour
{
    public Vector2 movementSpeed = new Vector2(5f,5f);
    private Rigidbody2D _playerRb; 
    private Animator _playerAnim;
    public bool isMoving;
    private Vector2 _dist;
    private Vector2 _playerPosition;
    private Vector2 _inputVector = new Vector2(0.0f, 0.0f);
    public static MovementController Instance;
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
        _playerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        _playerAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();
        isMoving = false;
    }

    void Update()
    {
        // Se controla los input del teclado
        // Se utiliza normalized para que en diagonal no corra más
        _inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _playerAnim.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        _playerAnim.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        _playerAnim.SetFloat("movSpeed", _inputVector.magnitude);
        
        // verificamos si hacemos clic con el ratón y no estamos clicando en una UI
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            ActionController.Instance.Action();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit(); // Cierra la aplicación
        }
        
    }
    void FixedUpdate()
    {
        // Se controlan la físicas ya que Player Rb es afectado por estas y deben de controlrse en FixUpdate
        if (isMoving)
        {
            _playerRb.MovePosition(_playerRb.position + (_inputVector * movementSpeed * Time.fixedDeltaTime));
        }
    }
    
    // cambio de estado Mover a No Mover para paralizar movimiento si realizo alguna acción con el ratón
    public void ToggleMovement()
    {
        isMoving = !isMoving;
    }
 
}
