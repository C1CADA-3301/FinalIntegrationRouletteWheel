using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardData
{
    public int IndexSlotNumber;
    [Space]
    [HideInInspector] public int BetAmount = 0;
    [Space]
    public OddOrEven OddOrEvenData;
    public RedOrBlack RedOrBlackData;
    public MultiRow multiRowData;
    public HighOrLow highOrLowData;
    public MultiColoumb multiColoumbData;

    public BoardData(int indexSlotNumber, int betAmount, OddOrEven oddOrEvenData, RedOrBlack redOrBlackData, MultiRow multiRow, HighOrLow highOrLow, MultiColoumb multiColoumbData) {
        IndexSlotNumber = indexSlotNumber;
        BetAmount = betAmount;
        OddOrEvenData = oddOrEvenData;
        RedOrBlackData = redOrBlackData;
        multiRowData = multiRow;
        highOrLowData = highOrLow;
        this.multiColoumbData = multiColoumbData;
    }
}
public enum OddOrEven {
    DEFAULT,
    ODD,
    EVEN
}

public enum RedOrBlack {
    RED,
    BLACK,
    DEFAULT
}

public enum MultiRow {
    FRIST_12,
    SEC_12,
    THIRD_12,
    ZERO
}
public enum MultiColoumb {
    FIRST_12,
    SECOUND_12,
    THIRD_12,
    ZERO
}
public enum HighOrLow {
    HIGH,
    LOW,
    ZERO
}


