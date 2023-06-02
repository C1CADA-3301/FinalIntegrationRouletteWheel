using System.Collections;
using System.Collections.Generic;
//using UnityEditor.MPE;
using UnityEngine;
using static BetManager;

public class BaseSlotNumber : MonoBehaviour
{
    protected GameObject placedCoin;
    [SerializeField] GameObject NoBalanceDisplay;
    [SerializeField] Animator NoBalanceDisplayAnim;
    [HideInInspector] public TableCoinVisualCountroller tablePlacedCoin;
    [SerializeField] protected Transform HoverPanel;
    [SerializeField] protected SlotState slotState;
    [HideInInspector] protected int index;
    float timer;
    public SpecialSlotType slotType;
    [HideInInspector] public int betAmount = 0;
    public int Index { get { return index; } }
    bool stopanim;
    protected virtual void OnMouseEnter() {
        if (BetManager.gameState != GameState.BET_STATE) return;
        HoverPanel.gameObject.SetActive(true);
    }
    void disabler()
    {
            AudioManager.Instance.Play(AudioType.Nofunds);
            NoBalanceDisplay.SetActive(true);
            NoBalanceDisplayAnim.SetBool("AnimPlay", true);
            stopanim = true;
    }
   

    protected void OnMouseDown() {
        if (!BetManager.Instance.HasChip()) {
            return;
        }
        if (!BetManager.Instance.HasSufficientBalance()) {
            disabler();
            return;
        }
        if (BetManager.gameState != GameState.BET_STATE) return;
        if (Input.GetMouseButtonDown(0)) {
            if (!(BetManager.Instance.GetWalletAmount() - BetManager.Instance.GetBetCoinData() >= 0)) {
                Debug.Log("Sorry you dont have the balance to do it");
                AudioManager.Instance.Play(AudioType.Nofunds);
                disabler();
                return;
            }
            if (placedCoin == null) {
                placedCoin = Instantiate(GameAssets.i.placedCoinChip, transform);
                tablePlacedCoin = placedCoin.GetComponent<TableCoinVisualCountroller>();
                tablePlacedCoin.PlaceBet(BetManager.Instance.GetBetCoinData(), 100);
            } else {
                tablePlacedCoin.PlaceBet(BetManager.Instance.GetBetCoinData(), 100);
            }
            AudioManager.Instance.Play(AudioType.PlaceBet);
            BetManager.Instance.UpdateLog(new ChangeLogOfData(index, BetManager.Instance.GetBetCoinData(), slotState, slotType));
        }
    }
    private void Update() {
        if (tablePlacedCoin == null) {
            return;
        }
        if (tablePlacedCoin.ChipCurrentValue <= 0) {
            betAmount = 0;
            Destroy(placedCoin);
            placedCoin = null;
            tablePlacedCoin = null;
        }
        if (stopanim)
        {
            timer += Time.deltaTime;
            if (timer >= 1.7f)
            {
                NoBalanceDisplayAnim.SetBool("AnimPlay", false);
                NoBalanceDisplay.SetActive(false);
                timer = 0;
                stopanim = false;
            }
        }
    }

    protected virtual void OnMouseExit() {
        if (BetManager.gameState != GameState.BET_STATE) return;
        HoverPanel.gameObject.SetActive(false);
    }
}
