using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Void Type Channel")]
public class VoidEventSO : ScriptableObject
{
    ///<summary>
    /// this will add and raise event of type Void
    /// </summary>
    public UnityAction onEventRaised;

    public void RaiseEvent() {
        onEventRaised?.Invoke();
    }
}
