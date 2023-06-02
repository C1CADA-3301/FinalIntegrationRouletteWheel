using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;
using static BetManager;
using static PayoutController;

public class BetManager : MonoBehaviour {
    //Instance
    public static BetManager Instance;

    [Header("Variables")]
    [SerializeField] int totalWalletBalance;
    [HideInInspector] public GameObject betButton;
    [Space(15)]

    [Header("References")]
    [SerializeField] TextMeshProUGUI walletBalText;
    [SerializeField] TextMeshProUGUI betAmountText;
    [SerializeField] Ball ballscript;
    [SerializeField] BetTypeEventSO betButtonType;
    [SerializeField] GameEndStatePayoutEventSo gameEndStateEvent;
    [SerializeField] VoidEventSO displayEvent;
    [SerializeField] VoidEventSO playUIAnimationEvent;
    [SerializeField] VoidEventSO playCameraAnimationEvent;
    [Space(15)]

    [Header("Data Containers")]
    [SerializeField] private List<BoardData> boardDatas;
    [SerializeField] private List<SpecialSlotData> specialSlotDatas;
    [SerializeField] public List<ChangeLogOfData> changeLog;
    [SerializeField] List<GameObject> tableSlotNumbers = new List<GameObject>();
    [SerializeField] List<GameObject> specialSlotNumbers = new List<GameObject>();
    [Space(15)]

    // Private Varibales
    private int betCoinData = 0;
    private int rngNumberGenerated;
    private int wonAmount;
    private int lostAmount;
    private int winningRoundAmount = 0;
    private int betPerRound = 0;
    GameEndState gameEndState;
    public static GameState gameState;
    private int indexCall = 1;
    public static bool hasnobalance;
    //Property
    public int RndNumberGenerated { get { return rngNumberGenerated; } private set { rngNumberGenerated = value; } }
    public int WonAmount { get { return wonAmount; } private set { wonAmount = value; } }
    public int LostAmount { get { return lostAmount; } private set { lostAmount = value; } }



    private void Awake() {
        Instance = this;
        boardDatas = new List<BoardData>();
        specialSlotDatas = new List<SpecialSlotData>();
        changeLog = new List<ChangeLogOfData>();
    }
    private void Start() {
        betCoinData = 0;
        walletBalText.text = totalWalletBalance.ToString();
        betAmountText.text = betPerRound.ToString();
        PayoutController.Instance.OnCoompletedWagerAmount += ChangeWonAmount;
        gameState = GameState.GAME_START;
    }
    private void OnDisable() {
        PayoutController.Instance.OnCoompletedWagerAmount -= ChangeWonAmount;
    }

    public void SetWalletValue(int changeAmount) {
        if (gameState == GameState.BET_STATE) {
            totalWalletBalance += changeAmount;
            walletBalText.text = totalWalletBalance.ToString();
        }
    }
    public void SetBetValue(int betAmount) {
        if(gameState == GameState.BET_STATE) {
            betPerRound += betAmount; 
            betAmountText.text = betPerRound.ToString();
        }
    }
    public int GetWalletAmount() {
        return totalWalletBalance;
    }
    // Methords
    public List<GameObject> GetSlotDetails() {
        return tableSlotNumbers;
    }
    public void SetRandomNumber(int value) {
        rngNumberGenerated = value;
    }
    private void CheckGameOutCome() {
        int wagerAmount = 0;

        SpecialSlotType specialSlotType = SpecialSlotType.DEFAULT;
        if (boardDatas.Count > 0) {
            for (int i = 0; i < boardDatas.Count; i++) {
                if (boardDatas[i].IndexSlotNumber == rngNumberGenerated) {
                    wagerAmount = boardDatas[i].BetAmount;
                    //Debug.Log("Won on Slot Number : " + boardDatas[i].IndexSlotNumber + " with a bet amount of : " + wagerAmount);
                    PayoutController.Instance.BetAmountWonForTableSlot(boardDatas, wagerAmount);
                }
            }
        }
        if (specialSlotDatas.Count > 0) {
            for (int i = 0; i < specialSlotDatas.Count; i++) {
                switch (specialSlotDatas[i].betType) {
                    case SpecialSlotType.ODD:
                        for (int j = 0; j < specialSlotDatas[i].selectedSlots.Count; j++) {
                            if (specialSlotDatas[i].selectedSlots[j].GetComponent<TableSlotNumberController>().boardData.IndexSlotNumber == rngNumberGenerated) {
                                wagerAmount = specialSlotDatas[i].betAmount;
                                specialSlotType = SpecialSlotType.ODD;
                                Debug.Log("Won on Slot Number : " + specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber + " which is due to betting on " + specialSlotDatas[i].betType
                                + " with a bet amount of : " + wagerAmount);
                               // Debug.Log("Odd Called");
                                PayoutController.Instance.BetAmountWonForSpecialSlot(specialSlotType, wagerAmount);
                            }
                        }
                        break;
                    case SpecialSlotType.EVEN:
                        for (int j = 0; j < specialSlotDatas[i].selectedSlots.Count; j++) {
                            if (specialSlotDatas[i].selectedSlots[j].GetComponent<TableSlotNumberController>().boardData.IndexSlotNumber == rngNumberGenerated) {
                                wagerAmount = specialSlotDatas[i].betAmount;
                                specialSlotType = SpecialSlotType.EVEN;
                                Debug.Log("Won on Slot Number : " + specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber + " which is due to betting on " + specialSlotDatas[i].betType
                                + " with a bet amount of : " + specialSlotDatas[i].betAmount);
                                PayoutController.Instance.BetAmountWonForSpecialSlot(specialSlotType, wagerAmount);
                                //Debug.Log("Even Called");
                            }
                        }
                        break;
                    case SpecialSlotType.BLACK:
                        for (int j = 0; j < specialSlotDatas[i].selectedSlots.Count; j++) {
                            if (specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber == rngNumberGenerated) {
                                wagerAmount = specialSlotDatas[i].betAmount;
                                specialSlotType = SpecialSlotType.BLACK;
                                Debug.Log("Won on Slot Number : " + specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber + " which is due to betting on " + specialSlotDatas[i].betType
                                + " with a bet amount of : " + specialSlotDatas[i].betAmount);
                                PayoutController.Instance.BetAmountWonForSpecialSlot(specialSlotType, wagerAmount);
                                //Debug.Log("Black Called");
                            }
                        }
                        break;
                    case SpecialSlotType.RED:
                        for (int j = 0; j < specialSlotDatas[i].selectedSlots.Count; j++) {
                            if (specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber == rngNumberGenerated) {
                                wagerAmount = specialSlotDatas[i].betAmount;
                                specialSlotType = SpecialSlotType.RED;
                                Debug.Log("Won on Slot Number : " + specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber + " which is due to betting on " + specialSlotDatas[i].betType
                                + " with a bet amount of : " + specialSlotDatas[i].betAmount);
                                PayoutController.Instance.BetAmountWonForSpecialSlot(specialSlotType, wagerAmount);
                                //Debug.Log("Red Called");
                            }
                        }
                        break;
                    case SpecialSlotType.LOW:
                        Debug.Log("Index Called : " + indexCall);
                        indexCall++;
                        for (int j = 0; j < specialSlotDatas[i].selectedSlots.Count; j++) {
                            if (specialSlotDatas[i].selectedSlots[j].GetComponent<TableSlotNumberController>().boardData.IndexSlotNumber == rngNumberGenerated) {
                                wagerAmount = specialSlotDatas[i].betAmount;
                                specialSlotType = SpecialSlotType.LOW;
                                Debug.Log("Number Found : " + specialSlotDatas[i].selectedSlots[j].GetComponent<TableSlotNumberController>().boardData.IndexSlotNumber);
                                Debug.Log("Won on Slot Number : " + specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber + " which is due to betting on " + specialSlotDatas[i].betType
                                + " with a bet amount of : " + specialSlotDatas[i].betAmount);
                                PayoutController.Instance.BetAmountWonForSpecialSlot(specialSlotType, wagerAmount);
                                //Debug.Log("Low Called");
                            }
                        }
                        break;
                    case SpecialSlotType.HIGH:
                        for (int j = 0; j < specialSlotDatas[i].selectedSlots.Count; j++) {
                            if (specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber == rngNumberGenerated) {
                                wagerAmount = specialSlotDatas[i].betAmount;
                                specialSlotType = SpecialSlotType.HIGH;
                                Debug.Log("Won on Slot Number : " + specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber + " which is due to betting on " + specialSlotDatas[i].betType
                                + " with a bet amount of : " + specialSlotDatas[i].betAmount);
                                PayoutController.Instance.BetAmountWonForSpecialSlot(specialSlotType, wagerAmount);
                               // Debug.Log("High Called");
                            }
                        }
                        break;
                    case SpecialSlotType.FIRST_ROW:
                        for (int j = 0; j < specialSlotDatas[i].selectedSlots.Count; j++) {
                            if (specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber == rngNumberGenerated) {
                                wagerAmount = specialSlotDatas[i].betAmount;
                                specialSlotType = SpecialSlotType.FIRST_ROW;
                                Debug.Log("Won on Slot Number : " + specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber + " which is due to betting on " + specialSlotDatas[i].betType
                                + " with a bet amount of : " + specialSlotDatas[i].betAmount);
                                PayoutController.Instance.BetAmountWonForSpecialSlot(specialSlotType, wagerAmount);
                                //Debug.Log("1st row Called");
                            }
                        }
                        break;
                    case SpecialSlotType.SECOND_ROW:
                        for (int j = 0; j < specialSlotDatas[i].selectedSlots.Count; j++) {
                            if (specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber == rngNumberGenerated) {
                                wagerAmount = specialSlotDatas[i].betAmount;
                                specialSlotType = SpecialSlotType.SECOND_ROW;
                                Debug.Log("Won on Slot Number : " + specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber + " which is due to betting on " + specialSlotDatas[i].betType
                                + " with a bet amount of : " + specialSlotDatas[i].betAmount);
                                PayoutController.Instance.BetAmountWonForSpecialSlot(specialSlotType, wagerAmount);
                                //Debug.Log("2nd row Called");
                            }
                        }
                        break;
                    case SpecialSlotType.THIRD_ROW:
                        for (int j = 0; j < specialSlotDatas[i].selectedSlots.Count; j++) {
                            if (specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber == rngNumberGenerated) {
                                wagerAmount = specialSlotDatas[i].betAmount;
                                specialSlotType = SpecialSlotType.THIRD_ROW;
                                Debug.Log("Won on Slot Number : " + specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber + " which is due to betting on " + specialSlotDatas[i].betType
                                + " with a bet amount of : " + specialSlotDatas[i].betAmount);
                                PayoutController.Instance.BetAmountWonForSpecialSlot(specialSlotType, wagerAmount);
                                //Debug.Log("3rd row Called");
                            }
                        }
                        break;
                    case SpecialSlotType.FIRST_COLOUMB:
                        for (int j = 0; j < specialSlotDatas[i].selectedSlots.Count; j++) {
                            if (specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber == rngNumberGenerated) {
                                wagerAmount = specialSlotDatas[i].betAmount;
                                specialSlotType = SpecialSlotType.FIRST_COLOUMB;
                                Debug.Log("Won on Slot Number : " + specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber + " which is due to betting on " + specialSlotDatas[i].betType
                                + " with a bet amount of : " + specialSlotDatas[i].betAmount);
                                PayoutController.Instance.BetAmountWonForSpecialSlot(specialSlotType, wagerAmount);
                                //Debug.Log("3rd row Called");
                            }
                        }
                        break;
                    case SpecialSlotType.SECOND_COLOUMB:
                        for (int j = 0; j < specialSlotDatas[i].selectedSlots.Count; j++) {
                            if (specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber == rngNumberGenerated) {
                                wagerAmount = specialSlotDatas[i].betAmount;
                                specialSlotType = SpecialSlotType.SECOND_COLOUMB;
                                Debug.Log("Won on Slot Number : " + specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber + " which is due to betting on " + specialSlotDatas[i].betType
                                + " with a bet amount of : " + specialSlotDatas[i].betAmount);
                                PayoutController.Instance.BetAmountWonForSpecialSlot(specialSlotType, wagerAmount);
                                //Debug.Log("3rd row Called");
                            }
                        }
                        break;
                    case SpecialSlotType.THIRD_COLOUMB:
                        for (int j = 0; j < specialSlotDatas[i].selectedSlots.Count; j++) {
                            if (specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber == rngNumberGenerated) {
                                wagerAmount = specialSlotDatas[i].betAmount;
                                specialSlotType = SpecialSlotType.THIRD_COLOUMB;
                                Debug.Log("Won on Slot Number : " + specialSlotDatas[i].selectedSlots[j].GetComponent
                                <TableSlotNumberController>().boardData.IndexSlotNumber + " which is due to betting on " + specialSlotDatas[i].betType
                                + " with a bet amount of : " + specialSlotDatas[i].betAmount);
                                PayoutController.Instance.BetAmountWonForSpecialSlot(specialSlotType, wagerAmount);
                                //Debug.Log("3rd row Called");
                            }
                        }
                        break;
                }

            }

        }
    }

    private void ChangeWonAmount(object obj, OnCompleteWagerAmountArgs args) {
        wonAmount = args.totalAmount;
        Debug.Log("Won Amount " + wonAmount + " Total Wallet " + totalWalletBalance);
        if (wonAmount > 0) {
            gameEndState = GameEndState.WON;
        } else {
            gameEndState = GameEndState.LOST;
        }
        //winningRoundAmount = wonAmount;
    }

    private void LostBetAmount() {

        foreach (var data in changeLog) {
            lostAmount += data.amount;
        }
    }
    //private CoinType coinType;


    public void UndoButton() {
        if (changeLog.Count > 0 && gameState==GameState.BET_STATE) {
            SlotState type = changeLog[changeLog.Count - 1].slotState;
            switch (type) {
                case SlotState.NORMAL:
                    int temp1 = changeLog[changeLog.Count - 1].index;
                    for (int i = 0; i < tableSlotNumbers.Count; i++) {
                        if (tableSlotNumbers[i].GetComponent<TableSlotNumberController>().Index == temp1) {
                            tableSlotNumbers[i].GetComponent<TableSlotNumberController>().tablePlacedCoin.PlaceBet(-changeLog[changeLog.Count - 1].amount, 100);
                            tableSlotNumbers[i].GetComponent<TableSlotNumberController>().boardData.BetAmount -= changeLog[changeLog.Count - 1].amount;
                            SetBetValue(-changeLog[changeLog.Count - 1].amount);
                            changeLog.Remove(changeLog[changeLog.Count - 1]);
                        }
                    }
                    break;
                case SlotState.SPECIAL:
                    SpecialSlotType temp2 = changeLog[changeLog.Count - 1].slotType;
                    for (int i = 0; i < specialSlotNumbers.Count; i++) {
                        if (specialSlotNumbers[i].GetComponent<SpecialSlotController>().slotType == temp2) {
                            specialSlotNumbers[i].GetComponent<SpecialSlotController>().tablePlacedCoin.PlaceBet(-changeLog[changeLog.Count - 1].amount, 100);
                            specialSlotNumbers[i].GetComponent<SpecialSlotController>().betAmount -= changeLog[changeLog.Count - 1].amount;
                            SetBetValue(-changeLog[changeLog.Count - 1].amount);
                            changeLog.Remove(changeLog[changeLog.Count - 1]);
                        }
                    }
                    break;
            }
        }
    }

    public void ResetGameAfterRound() {
        playUIAnimationEvent.RaiseEvent();
        gameState = GameState.BET_STATE;
        RouletteWheelManager.Instance.ResetWheel();
        SetWalletValue(wonAmount);
        Debug.Log("Won Amount " + wonAmount + " Total Wallet " + totalWalletBalance);
        lostAmount = 0;
        wonAmount = 0;
        SetBetValue(-betPerRound);
        betButtonType.RaiseEvent(BetButtonType.RESET);
        boardDatas.Clear();
        specialSlotDatas.Clear();
        changeLog.Clear();

        

    }
    public void onResetButtonClicked() {
        if (gameState == GameState.BET_STATE) {
            betButtonType.RaiseEvent(BetButtonType.RESET);
            foreach (BoardData data in boardDatas) {
                if (data.BetAmount > 0) {
                    SetWalletValue(data.BetAmount);
                }
            }
            foreach (SpecialSlotData data in specialSlotDatas) {
                if (data.betAmount > 0) {
                    SetWalletValue(data.betAmount);
                }
            }
            boardDatas.Clear();
            specialSlotDatas.Clear();
            foreach(var log in changeLog) {
                betPerRound -= log.amount;
                SetBetValue(-betPerRound);
            }
            changeLog.Clear();
        }
    }
    public void onSpinButtonClicked() {
        if (changeLog.Count > 0 && gameState == GameState.BET_STATE) {
            gameState = GameState.PLAY;
            betButtonType.RaiseEvent(BetButtonType.SPIN);
            playCameraAnimationEvent.RaiseEvent();
            playUIAnimationEvent.RaiseEvent();
            CheckGameOutCome();
            PayoutController.Instance.CompleteWagerCalculation();
            LostBetAmount();
            gameEndStateEvent.RaiseEvent(gameEndState, gameEndState == GameEndState.WON ? wonAmount : lostAmount);
            //changeLog.Clear();

        }
    }


    public void UpdateLog(ChangeLogOfData log) {
        changeLog.Add(log);
        SetBetValue(log.amount);

    }
    public void UpdateDataFromTable(BoardData data) {
        boardDatas.Add(data);
    }
    public void UpdateSpecialDataFromTable(SpecialSlotData data) {
        specialSlotDatas.Add(data);
    }

    public GameObject GetCoin() {
        return betButton;
    }

    public void SetBetCoinData(int coinData) {
        betCoinData = coinData;
    }
    public bool HasChip() {
        return betCoinData > 0;
    }
    public bool HasSufficientBalance() {
        if (totalWalletBalance <= 0 && betPerRound <= 0)
        {
            hasnobalance = true;
        }
        else
        {
            hasnobalance = false;
        }
        return totalWalletBalance > 0;
    }

    public int GetBetCoinData() {
        return betCoinData;
    }
}
[System.Serializable]
public class SpecialSlotData {
    public int betAmount;
    public SpecialSlotType betType;
    public List<GameObject> selectedSlots = new List<GameObject>();

    public SpecialSlotData(int betAmount, SpecialSlotType betType, List<GameObject> selectedSlots) {
        this.betAmount = betAmount;
        this.betType = betType;
        this.selectedSlots = selectedSlots;
    }
}
public enum GameEndState {
    WON, LOST
}
public enum SlotState {
    NORMAL,
    SPECIAL
}

[System.Serializable]
public class ChangeLogOfData {
    public int index;
    public int amount;
    public SlotState slotState;
    public SpecialSlotType slotType;

    public ChangeLogOfData(int index, int amount, SlotState slotState, SpecialSlotType slotType) {
        this.index = index;
        this.amount = amount;
        this.slotState = slotState;
        this.slotType = slotType;
    }

}
public enum BetButtonType {
    SPIN,
    RESET,
}
public enum GameState {
    BET_STATE, // bet state
    PLAY,
    PAUSED,
    GAME_START,
    DISPLAY
}
