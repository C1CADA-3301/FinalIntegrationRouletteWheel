
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="GameEvent", fileName ="GameEvent")]
public class GameEvent : ScriptableObject {
    ///<summary>
    /// this list of listner will notify when the event is raised
    /// </summary>
    public List<GameEventListener> listener = new List<GameEventListener>(); 

    public void Raise(BetButtonType type) {
        for(int i = listener.Count - 1; i >= 0; i--) {
            listener[i].OnEventRaised(type);
        }
    } 

    public void Register(GameEventListener gameEvent) {
        if (!listener.Contains(gameEvent)) { listener.Add(gameEvent); }
    }
    public void Unregister(GameEventListener gameEvent) {
        if (listener.Contains(gameEvent)) { listener.Remove(gameEvent); }
    }
}
