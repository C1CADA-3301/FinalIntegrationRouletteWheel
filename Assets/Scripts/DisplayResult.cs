using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayResult : MonoBehaviour
{
    [SerializeField] GameObject backgroundPanel;
    [SerializeField] GameObject rewardPanel;
    [SerializeField] GameObject resultPanel;
    [SerializeField] Sprite resultWinBG; 
    [SerializeField] Sprite resultLoseBG; 
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] TextMeshProUGUI payoutText;
    [SerializeField] string result;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowResults()
    {
        backgroundPanel.SetActive(true);
        Invoke("ResultPanelPopup",0.3f);    
       
    }

    public void ResultPanelPopup()
    {
        rewardPanel.SetActive(true);
        if(result == "Win")
        {
            resultPanel.GetComponent<Image>().sprite = resultWinBG;
            resultText.text = "You Win!!!";
            payoutText.text = "+1000";
        }
        else if(result == "Lose")
        {
            resultPanel.GetComponent<Image>().sprite = resultLoseBG;
            resultText.text = "You Lose!!!";
            payoutText.text = "-1000";
        }
    }
}
