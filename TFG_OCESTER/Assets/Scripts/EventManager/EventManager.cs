using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    // Se implementa una acción que devuelve un objeto ToolsSO a la que se suscribirán los observer
    public static Action<ToolsSO> OnSelectedTool;
    public static Action OnEvent2;

    public static void SelectedToolEvent(ToolsSO tool)
    {
        OnSelectedTool?.Invoke(tool);
    }

    public static void TriggerEvent2()
    {
        OnEvent2?.Invoke();
    }

    
}
