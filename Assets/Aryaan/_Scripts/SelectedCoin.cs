using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCoin : MonoBehaviour {

    [SerializeField] BetCoinDataSO coinData;
    private void OnMouseDown() {
        if(Input.GetMouseButtonDown(0)) {
            BetManager.Instance.SetBetCoinData(coinData.coinValue);
        }

    }
}
