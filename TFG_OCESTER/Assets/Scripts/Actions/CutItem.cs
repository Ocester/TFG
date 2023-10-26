using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutItem : MonoBehaviour
{
    [SerializeField] private ToolsSO tools;
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
        if (selectedAction.getTool() != tools.cut) return;
        
        if (other.gameObject.name == "UpHit" || other.gameObject.name == "DownHit" || other.gameObject.name == "RightHit" || other.gameObject.name == "LeftHit")
        {
            Debug.Log("CUT HIT!!");
            //eleiminar/obtener objeto en el equipo
        };
    }
}
