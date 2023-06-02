using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSlotNumberController : BaseSlotNumber {

    [SerializeField] public BoardData boardData;
    [SerializeField] BetTypeEventSO betButtonTypeEvent;

    private void Start() {
        HoverPanel = transform.GetChild(transform.childCount - 1);
        index = boardData.IndexSlotNumber;
    }
    private void OnEnable() {
        betButtonTypeEvent.onEventRaised += UpdateBoardData;
    }
    private void OnDisable() {
        betButtonTypeEvent.onEventRaised -= UpdateBoardData;
    }

    public void UpdateHoverState(bool state) {
        if (HoverPanel == null) return;
        HoverPanel.gameObject.SetActive(state);
    }

    public void UpdateBoardData(BetButtonType betButton) {
        if (tablePlacedCoin != null) {
            betAmount = tablePlacedCoin.ChipCurrentValue;
        }
        if(betAmount > 0) {
            BetManager.Instance.UpdateDataFromTable(new BoardData(boardData.IndexSlotNumber, betAmount,
                boardData.OddOrEvenData, boardData.RedOrBlackData, boardData.multiRowData, boardData.highOrLowData, boardData.multiColoumbData));
        }
        if(betButton == BetButtonType.SPIN) {
            //TODO some task related to spin
        }
        else if(betButton == BetButtonType.RESET) {
            betAmount = 0;
            Destroy(placedCoin);
            placedCoin = null;
            tablePlacedCoin = null;
        }
    }


}
