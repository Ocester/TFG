using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItem : MonoBehaviour
{
    //public event Action<string> OnDigItem;
    [SerializeField] private ToolsSO tools;
    [SerializeField] private EggSO egg;
    private ActionController selectedAction;
    
    // Start is called before the first frame update
    void Start()
    {
        selectedAction = GameObject.FindObjectOfType<ActionController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (selectedAction.getTool() != tools.grab) return;
        
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("GRAB HIT!!!!");
            gameObject.SetActive(false);
            Invoke("Activate", egg.respawnTime);
        };
    }

    private void Activate()
    {
        gameObject.SetActive(true);
        
    }
}
