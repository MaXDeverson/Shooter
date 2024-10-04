using System;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    public Action<string> AnimationEventInvoke;
    public void AnimationEvent(string parameter)
    {
        AnimationEventInvoke?.Invoke(parameter);
    }
}
