using UnityEngine;
using System;
using UnityEngine.UI;

public class DigItem : MonoBehaviour
{
    
    //public event Action<string> OnDigItem;
    [SerializeField] private ToolsSO tools;
    [SerializeField] private VegetableSO vegetable;
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
        if (selectedAction.getTool() != tools.dig) return;
        
        if (other.gameObject.name == "UpHit" || other.gameObject.name == "DownHit" || other.gameObject.name == "RightHit" || other.gameObject.name == "LeftHit")
        {
            Debug.Log("DIG HIT!!!!");
            Invoke("Activate", vegetable.respawnTime);
            gameObject.SetActive(false);
        };
    }

    private void Activate()
    {
        gameObject.SetActive(true);
        
    }



}
