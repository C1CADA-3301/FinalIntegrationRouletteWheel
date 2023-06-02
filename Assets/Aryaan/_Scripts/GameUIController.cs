using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] VoidEventSO displayUIEvent; // Event 
    [SerializeField] GameEndStatePayoutEventSo gameEndStateEvent; // event
    //Refernce of Gameobjects
    [SerializeField] GameObject backGroundPanel;
    [SerializeField] GameObject rewardPanel;
    [SerializeField] GameObject InstructionsPanel;
    //[SerializeField] GameObject BetDescriptionPanel;
    [Header("GAME RESULT PANEL")]
    [SerializeField] GameObject winResultPanel;
    [SerializeField] TextMeshProUGUI wonAmountText;

    [SerializeField] GameObject lostResultPanel;
    [SerializeField] TextMeshProUGUI lostAmountText;
   // [SerializeField] GameObject AudioControls;
    [SerializeField] GameObject PausePanel;
    [SerializeField] Sprite AudioOff;
    [SerializeField] Sprite AudioOn;
    [SerializeField] Image AudioBTN;

    GameState currentGameState;

    [Header("UI ANIM")]
    [SerializeField] GameObject muteButtonImageHolder;
    [SerializeField] Sprite muteIcon;
    [SerializeField] Sprite SoundIcon;


    [SerializeField] GameObject sidePanelOBJ;
    [SerializeField] GameObject OptionsPanelOBJ;
    [SerializeField] VoidEventSO playUIAnimationEvent;
    [SerializeField] VoidEventSO playCameraAnimationEvent;



    private GameEndState state;

    private int displayAmount = 0;
    private string displayText = string.Empty;
    private void OnEnable() {
        gameEndStateEvent.onEventRaised += GetData;
        displayUIEvent.onEventRaised += DisplayData;

       playUIAnimationEvent.onEventRaised += PlayUIAnimation;
        playCameraAnimationEvent.onEventRaised += PlayCameraAnimation;
    }
    private void OnDisable() {
        gameEndStateEvent.onEventRaised -= GetData;
        displayUIEvent.onEventRaised -= DisplayData;

        playUIAnimationEvent.onEventRaised -= PlayUIAnimation;
        playCameraAnimationEvent.onEventRaised -= PlayCameraAnimation;
    }

    private void Start()
    {
        BetManager.gameState = GameState.BET_STATE;
       // backGroundPanel.SetActive(true);

    }

    public void ChangeGameStateAfterGettingPlayerID()
    {
        BetManager.gameState = GameState.BET_STATE;
    }

    public void GetData(GameEndState state, int payout) {
        this.state = state;
        displayAmount = payout;
        displayText = state == GameEndState.WON ? "You Won" : "You Lost";
        DatabaseManager.Instance.UpdateDataToTable(displayText,payout);
    }
    public void DisplayData() {
        backGroundPanel.SetActive(true);
        if(state==GameEndState.WON)
        {
            winResultPanel.SetActive(true);
            wonAmountText.text = "+" + displayAmount; 
            AudioManager.Instance.Play(AudioType.Win);
        }
        else
        {
            lostResultPanel.SetActive(true);
            lostAmountText.text = "-" + displayAmount;
            AudioManager.Instance.Play(AudioType.Lose);
        }
        //rewardPanel.SetActive(true);
        //gameResultText.text = state == GameEndState.WON ? " + " + displayAmount : " - "  + displayAmount;
        //gameStateText.text = displayText;
    }


    public void DisableData() 
    {
        if(state==GameEndState.WON) 
        {
            winResultPanel.SetActive(false);
        
        }
        else
        {
            lostResultPanel.SetActive(false);
        }
        backGroundPanel.SetActive(false);
        //rewardPanel.SetActive(false);
    }

    public void Help()
    {
        if(BetManager.gameState == GameState.BET_STATE) 
        {
            backGroundPanel.SetActive(true);
            InstructionsPanel.SetActive(true);

        }
       

    }
    public void Return()
    {
        backGroundPanel.SetActive(false);
        InstructionsPanel.SetActive(false);
    }
    public void pauseGame()
    {
        if(BetManager.gameState== GameState.BET_STATE)
        {
            backGroundPanel.SetActive(true);
            currentGameState = BetManager.gameState;
            BetManager.gameState = GameState.PAUSED;

            PausePanel.SetActive(true);
            backGroundPanel.SetActive(true);
            AudioListener.volume = 0f;
            /*
            backGroundPanel.SetActive(true);
            PausePanel.SetActive(true);

           // AudioControls.SetActive(false);
            
            */
            StartCoroutine(Wait());

        }


    }

    bool isMute;
    public void MuteButton()
    {
        if(BetManager.gameState!= GameState.BET_STATE) 
            return;
        
        isMute = !isMute;
        if(isMute) 
        {
            muteButtonImageHolder.GetComponent<Image>().sprite = muteIcon;
            AudioListener.volume = 0f;

        }
        if(!isMute) 
        {
            muteButtonImageHolder.GetComponent<Image>().sprite = SoundIcon;
            AudioListener.volume = 1f;
         

        }
        
        
    }
    public void ResumeGame()
    {
        
        BetManager.gameState = currentGameState;
        backGroundPanel.SetActive(false);
        PausePanel.SetActive(false);
        backGroundPanel.SetActive(false); 
        Time.timeScale = 1f;
        AudioListener.volume = 1f;
        /*
         //AudioControls.SetActive(true);
         PausePanel.SetActive(false);
         backGroundPanel.SetActive(false);*/
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        EditorApplication.isPlaying = false;

    #else
        Application.Quit();

    #endif

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0f;

    }

    //UI ANIM


    public void PlayUIAnimation()
    {
        if(BetManager.gameState==GameState.PLAY || BetManager.gameState == GameState.DISPLAY)
        {
           
            sidePanelOBJ.GetComponent<Animator>().SetTrigger("anim_Trigger");
            OptionsPanelOBJ.GetComponent<Animator>().SetTrigger("anim_Trigger");

        }

    }

    public void PlayCameraAnimation()
    {
        if (BetManager.gameState == GameState.PLAY || BetManager.gameState == GameState.DISPLAY)
        {
            Camera.main.GetComponent<Animator>().SetTrigger("anim_Trigger");
        }
            
    }
}
