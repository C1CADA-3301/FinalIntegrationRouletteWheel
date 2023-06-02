using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSlotController : BaseSlotNumber {
    //public int specialBetAmount;
    private List<GameObject> allCurrentSlotNumbers;
    [SerializeField] BetTypeEventSO betButtonTypeEvent;
    [SerializeField]private List<GameObject> selectedSlotNumbers;
    private void Start() {
        HoverPanel = transform.GetChild(transform.childCount - 1);
        allCurrentSlotNumbers = new List<GameObject>();
        allCurrentSlotNumbers = BetManager.Instance.GetSlotDetails();
        DetermineRequiredSlotNumbers();
    }
    private void OnEnable() {
        betButtonTypeEvent.onEventRaised += UpdateBoardData;
    }
    private void OnDisable() {
        betButtonTypeEvent.onEventRaised -= UpdateBoardData;
    }
    public void UpdateBoardData(BetButtonType betButton) {
        if (tablePlacedCoin != null) {
            betAmount = tablePlacedCoin.ChipCurrentValue;
        }
        if (betAmount > 0) {
            BetManager.Instance.UpdateSpecialDataFromTable(new SpecialSlotData(betAmount, slotType, selectedSlotNumbers));

}
        if (betButton == BetButtonType.SPIN) {
            //TODO some task related to spin
        } else if (betButton == BetButtonType.RESET) {
            betAmount = 0;
            Destroy(placedCoin);
            placedCoin = null;
            tablePlacedCoin = null;
        }
    }

    protected override void OnMouseEnter() {
        base.OnMouseEnter();
        if (BetManager.gameState != GameState.BET_STATE) return;
        UpdateTheHoverGameobject(true);
    }

    protected override void OnMouseExit() {
        base.OnMouseExit();
        if (BetManager.gameState != GameState.BET_STATE) return;
        UpdateTheHoverGameobject(false);
    }



    private void UpdateTheHoverGameobject(bool state) {
        foreach (var slot in selectedSlotNumbers) {
            slot.GetComponent<TableSlotNumberController>().UpdateHoverState(state);
        }
    }

    public void DetermineRequiredSlotNumbers() {
        switch (slotType) {
            case SpecialSlotType.ODD:
                foreach(var slotNumbers in allCurrentSlotNumbers) {
                    if(slotNumbers.GetComponent<TableSlotNumberController>().boardData.OddOrEvenData == OddOrEven.ODD) {
                        selectedSlotNumbers.Add(slotNumbers);
                    }
                }
                break;
            case SpecialSlotType.EVEN:
                foreach (var slotNumbers in allCurrentSlotNumbers) {
                    if (slotNumbers.GetComponent<TableSlotNumberController>().boardData.OddOrEvenData == OddOrEven.EVEN) {
                        selectedSlotNumbers.Add(slotNumbers);
                    }
                }
                break;
            case SpecialSlotType.HIGH:
                foreach (var slotNumbers in allCurrentSlotNumbers) {
                    if (slotNumbers.GetComponent<TableSlotNumberController>().boardData.highOrLowData == HighOrLow.HIGH) {
                        selectedSlotNumbers.Add(slotNumbers);
                    }
                }
                break;
            case SpecialSlotType.LOW:
                foreach (var slotNumbers in allCurrentSlotNumbers) {
                    if (slotNumbers.GetComponent<TableSlotNumberController>().boardData.highOrLowData == HighOrLow.LOW) {
                        selectedSlotNumbers.Add(slotNumbers);
                    }
                }
                break;
            case SpecialSlotType.RED:
                foreach (var slotNumbers in allCurrentSlotNumbers) {
                    if (slotNumbers.GetComponent<TableSlotNumberController>().boardData.RedOrBlackData == RedOrBlack.RED) {
                        selectedSlotNumbers.Add(slotNumbers);
                    }
                }
                break;
            case SpecialSlotType.BLACK:
                foreach (var slotNumbers in allCurrentSlotNumbers) {
                    if (slotNumbers.GetComponent<TableSlotNumberController>().boardData.RedOrBlackData == RedOrBlack.BLACK) {
                        selectedSlotNumbers.Add(slotNumbers);
                    }
                }
                break;
            case SpecialSlotType.FIRST_ROW:
                foreach (var slotNumbers in allCurrentSlotNumbers) {
                    if (slotNumbers.GetComponent<TableSlotNumberController>().boardData.multiRowData == MultiRow.FRIST_12) {
                        selectedSlotNumbers.Add(slotNumbers);
                    }
                }
                break;
            case SpecialSlotType.SECOND_ROW:
                foreach (var slotNumbers in allCurrentSlotNumbers) {
                    if (slotNumbers.GetComponent<TableSlotNumberController>().boardData.multiRowData == MultiRow.SEC_12) {
                        selectedSlotNumbers.Add(slotNumbers);
                    }
                }
                break;
            case SpecialSlotType.THIRD_ROW:
                foreach (var slotNumbers in allCurrentSlotNumbers) {
                    if (slotNumbers.GetComponent<TableSlotNumberController>().boardData.multiRowData == MultiRow.THIRD_12) {
                        selectedSlotNumbers.Add(slotNumbers);
                    }
                }
                break;
            case SpecialSlotType.FIRST_COLOUMB:
                foreach (var slotNumbers in allCurrentSlotNumbers) {
                    if (slotNumbers.GetComponent<TableSlotNumberController>().boardData.multiColoumbData == MultiColoumb.FIRST_12) {
                        selectedSlotNumbers.Add(slotNumbers);
                    }
                }
                break;
            case SpecialSlotType.SECOND_COLOUMB:
                foreach (var slotNumbers in allCurrentSlotNumbers) {
                    if (slotNumbers.GetComponent<TableSlotNumberController>().boardData.multiColoumbData == MultiColoumb.SECOUND_12) {
                        selectedSlotNumbers.Add(slotNumbers);
                    }
                }
                break;
            case SpecialSlotType.THIRD_COLOUMB:
                foreach (var slotNumbers in allCurrentSlotNumbers) {
                    if (slotNumbers.GetComponent<TableSlotNumberController>().boardData.multiColoumbData == MultiColoumb.THIRD_12) {
                        selectedSlotNumbers.Add(slotNumbers);
                    }
                }
                break;
            default: 
                break;
        }
    }

}
public enum SpecialSlotType {
    ODD, // 1 : 1
    EVEN, // 1 : 1
    HIGH, // 2 : 1
    LOW, // 2 : 1
    RED, // 1 : 1
    BLACK, // 1 : 1
    FIRST_ROW, //2:1
    SECOND_ROW, //2:1
    THIRD_ROW, //2:1
    FIRST_COLOUMB,
    SECOND_COLOUMB,
    THIRD_COLOUMB,
    DEFAULT
}
