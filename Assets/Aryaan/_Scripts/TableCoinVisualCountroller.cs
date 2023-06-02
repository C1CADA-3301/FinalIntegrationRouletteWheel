using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TableCoinVisualCountroller : MonoBehaviour {
    [Header("Variables")]
    [SerializeField] TextMeshPro displayText;
    [Space]
    [SerializeField] List<Sprite> displaySprite;
    [Space]
    [HideInInspector]public int ChipCurrentValue = 0;
    SpriteRenderer spriteRenderer;
    private void Start() {
        displayText.text = ChipCurrentValue.ToString();
    }

    public void PlaceBet(int coinData, int maxBetAmount) {
        if(!spriteRenderer) {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        ChipCurrentValue += coinData;
        BetManager.Instance.SetWalletValue(-coinData);
        UpdateBetChipGraphics();
        displayText.text = ChipCurrentValue.ToString();
    }
    public void UpdateBetChipGraphics() {
        if(ChipCurrentValue >= 1000) {
            spriteRenderer.sprite = displaySprite[0];
        }
        else if (ChipCurrentValue >= 500)
        {
            spriteRenderer.sprite = displaySprite[1];
        }
        else if (ChipCurrentValue >= 200)
        {
            spriteRenderer.sprite = displaySprite[2];
        }
        else if(ChipCurrentValue >= 100) {
            spriteRenderer.sprite = displaySprite[3];
        }
        else if(ChipCurrentValue >= 50) {
            spriteRenderer.sprite = displaySprite[4];
        }
        else if(ChipCurrentValue >= 25) {
            spriteRenderer.sprite = displaySprite[5];
        }
        else if(ChipCurrentValue >= 10) {
            spriteRenderer.sprite = displaySprite[6];
        } else {
            spriteRenderer.sprite = displaySprite[6];
        }
    }
}