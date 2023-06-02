using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class PlayerBalance : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI walletBal;
    [SerializeField] TMP_InputField playerIdText;
    [SerializeField] GameObject playerPanel;
    [SerializeField] GameObject bgPanel;
    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowBalance()
    {
        Debug.Log(playerIdText.text);
        string bal = playerIdText.text;
        int id = int.Parse(bal);
        StartCoroutine(GetUserCredit(id));
        playerPanel.SetActive(false);
        bgPanel.SetActive(false);

    }

    public IEnumerator GetUserCredit(int userId)
    {
        string url = "http://43.205.23.196:3000/api/GetUserCredit/"+userId;



        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();



        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            Debug.Log(json);



            string[] s = json.Split(":");



            s[1] = Regex.Replace(s[1], @"[}]]", "");
            int walletBalance = int.Parse(s[1]);



            //Debug.Log(walletBalance);
            walletBal.text = walletBalance.ToString();
        }
    }
    public void ClearData()
    {
        Debug.Log("Cleared");
        playerIdText.text = string.Empty;
        
    }
}
