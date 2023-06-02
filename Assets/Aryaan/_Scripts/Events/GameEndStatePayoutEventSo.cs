using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/GameEndState Payout Type Channel")]
public class GameEndStatePayoutEventSo : ScriptableObject{
    ///<summary>
    /// this will add and raise event of type GameEnd State and Payout to display
    /// </summary>
    public UnityAction<GameEndState, int> onEventRaised;

    public void RaiseEvent(GameEndState state , int amount) {
        onEventRaised?.Invoke(state, amount);
    }
}
