using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] GameEvent Event;
    [SerializeField] UnityEvent<BetButtonType> Response;

    private void OnEnable() {
        Event.Register(this);
    }
    private void OnDisable() {
        Event.Unregister(this);
    }
    public void OnEventRaised(BetButtonType type) {
        Response?.Invoke(type);
    }
}
