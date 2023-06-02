using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Events/Bet Button Type Channel")]
public class BetTypeEventSO : ScriptableObject{
    ///<summary>
    /// this will add and raise event of type Bet button type
    /// </summary>
    public UnityAction<BetButtonType> onEventRaised;

    public void RaiseEvent(BetButtonType type) {
        onEventRaised?.Invoke(type);
    }
}
