using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PayoutController : MonoBehaviour {
    public static PayoutController Instance;
    public int WonAmount {  get; private set; }
    public event EventHandler<OnCompleteWagerAmountArgs> OnCoompletedWagerAmount;
    private int indexCalled = 1;
    public class OnCompleteWagerAmountArgs : EventArgs {
        public int totalAmount; 
    }

    private void Awake() {
        Instance = this;
    }

    public void BetAmountWonForTableSlot(List<BoardData> normalTableNumbers, int betAmount) {
        if(normalTableNumbers.Count == 0) {
            return;
        }
        int count = normalTableNumbers.Count;
        WonAmount += ((35) * betAmount) + betAmount;
    }
    public void BetAmountWonForSpecialSlot(SpecialSlotType specialSlotType, int betAmount) {
        //Debug.Log("Bet amount : " + betAmount + " Index : " + indexCalled);
        if(specialSlotType == SpecialSlotType.EVEN || specialSlotType == SpecialSlotType.ODD || specialSlotType == SpecialSlotType.RED ||
            specialSlotType == SpecialSlotType.BLACK || specialSlotType == SpecialSlotType.LOW  || specialSlotType == SpecialSlotType.HIGH) {
            WonAmount += (betAmount * 1) + betAmount;
        }
        else if(specialSlotType == SpecialSlotType.FIRST_ROW || specialSlotType == SpecialSlotType.SECOND_ROW || 
            specialSlotType == SpecialSlotType.THIRD_ROW || specialSlotType == SpecialSlotType.FIRST_COLOUMB || specialSlotType == SpecialSlotType.SECOND_COLOUMB 
            ||specialSlotType == SpecialSlotType.THIRD_COLOUMB) {
            WonAmount += (betAmount * 2) + betAmount;
        }
        indexCalled++;
    }
    public void CompleteWagerCalculation() {
        OnCoompletedWagerAmount?.Invoke(this, new OnCompleteWagerAmountArgs {
            totalAmount = WonAmount
        });
        ResetWonAmount();
    }
    public void ResetWonAmount() {
        WonAmount = 0;
    }
}
