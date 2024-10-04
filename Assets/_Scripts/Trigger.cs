using System;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Action<string, int> OnTriggerAction;

    public void TriggerEnter(string tag, int value)
    {
         OnTriggerAction?.Invoke(tag, value);
    }
    
}
