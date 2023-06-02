using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouletteWheelManager : MonoBehaviour
{
   public static RouletteWheelManager Instance;

    [SerializeField] TMP_Text outputTextUI;
    [SerializeField] GameObject outputDisplayPanelImage;

    [Header("SCRIPTABLE OBJECT REFERENCE")]
    [SerializeField] BetTypeEventSO bettypeEvent;
   // [SerializeField] VoidEventSO dataResetEvent;
    [SerializeField] VoidEventSO displayEvent;

    public int randomGeneratedNumber;
   // public bool isOutputDisplayed;


    public delegate void SpinBtn_del();
    public static event SpinBtn_del SpinBtn_evt;

    public delegate void ResetWheel_del();
    public static event ResetWheel_del ResetWheel_evt;

    public delegate void RecieveNumberDel(int rng);


    private void Awake()
    {
        Instance = this;
    }


    private void OnEnable()
    {
        bettypeEvent.onEventRaised += SpinButton;
      //  dataResetEvent.onEventRaised += ResetWheel;
     

    }

    private void OnDisable()
    {
        bettypeEvent.onEventRaised -= SpinButton;
       // dataResetEvent.onEventRaised -= ResetWheel;

    }

    private void Start()
    {
        AudioManager.Instance.Play(AudioType.BgMusic);
        ResetOutput();
        RouletteWheelManager.ResetWheel_evt += ResetOutput;
    }
    public void SpinButton(BetButtonType type)
    {
        if(type==BetButtonType.SPIN)
        {

            RandomNumberGenerator.Instance.RandomNumberPosition();
            randomGeneratedNumber = RandomNumberGenerator.Instance.randomNumber;
            BetManager.Instance.SetRandomNumber(randomGeneratedNumber);
            SpinBtn_evt?.Invoke();

            AudioManager.Instance.changeVolume(AudioType.BgMusic, 0.1f);
            AudioManager.Instance.Play(AudioType.Spin);
        }
        
        //randomGeneratedNumber = RandomNumberGenerator.randomNumber;

    }


    public void DisplayOutPutText()
    {
        outputDisplayPanelImage.SetActive(true);
        SlotColor slotcolor;
        slotcolor = RandomNumberGenerator.Instance.randomObject.GetComponent<WheelSlotNumberData>().slotColor;
        outputTextUI.gameObject.SetActive(true);
        outputTextUI.text = RandomNumberGenerator.Instance.randomNumber.ToString();
        outputTextUI.gameObject.GetComponent<Animator>().SetBool("sizeanim", true);

    }
    public void ResetOutput()
    {
        
        outputDisplayPanelImage.SetActive(false);
        outputTextUI.gameObject.SetActive(false);
        outputTextUI.gameObject.GetComponent<Animator>().SetBool("sizeanim", false);
        //isOutputDisplayed= false;
        AudioManager.Instance.changeVolume(AudioType.BgMusic, 0.3f);
    }


    public void ResetWheel()
    {
        
        ResetWheel_evt?.Invoke();
        
    }

    public void OutPutDisplayed()
    {
        displayEvent.RaiseEvent();
       

        // isOutputDisplayed = true;
    }

    

 

    

  
}
