using TMPro;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using System.Text;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI betAmount;
    [SerializeField] TextMeshProUGUI walletAmount;
    [SerializeField] TextMeshProUGUI playerName;
    public List<ChangeLogOfData> dataToBeInserted;
    [HideInInspector] public static DatabaseManager Instance;
    string apiUrl = "http://15.207.110.133:8098";
    int attemptId;
    int userId = 1;
    int gameID = 2;

    int totalBet = 0;

    bool gotData;

    // Start is called before the first frame update
    public void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        StartCoroutine(GetUserCredit());
        // StartCoroutine(GetUserBalance());
        
    }
    public void Insert()
    {
     dataToBeInserted = BetManager.Instance.changeLog;
    
     foreach(var data in dataToBeInserted)
     {
        int bet = data.amount;

       
        if(data.slotState == SlotState.SPECIAL)
        {
            if(data.slotType == SpecialSlotType.FIRST_ROW)
            {
                StartCoroutine(InsertDataGameHistoryCoroutine(gameID, bet, "1-12", attemptId));
            }

            else if(data.slotType == SpecialSlotType.SECOND_ROW)
            {
                StartCoroutine(InsertDataGameHistoryCoroutine(gameID, bet, "13-24", attemptId));
            }

            else if(data.slotType == SpecialSlotType.THIRD_ROW)
            {
                StartCoroutine(InsertDataGameHistoryCoroutine(gameID, bet, "25-36", attemptId));
            }

            else if(data.slotType == SpecialSlotType.RED)
            {
                StartCoroutine(InsertDataGameHistoryCoroutine(gameID, bet, "RED", attemptId));
            }

            else if(data.slotType == SpecialSlotType.BLACK)
            {
                StartCoroutine(InsertDataGameHistoryCoroutine(gameID, bet, "BLACK", attemptId));
            }

            else if(data.slotType == SpecialSlotType.LOW)
            {
                StartCoroutine(InsertDataGameHistoryCoroutine(gameID, bet, "1-18", attemptId));
            }

            else if(data.slotType == SpecialSlotType.HIGH)
            {
                StartCoroutine(InsertDataGameHistoryCoroutine(gameID, bet, "19-36", attemptId));
            }

            else if(data.slotType == SpecialSlotType.ODD)
            {
                StartCoroutine(InsertDataGameHistoryCoroutine(gameID, bet, "ODD", attemptId));
            }

            else if(data.slotType == SpecialSlotType.EVEN)
            {
                StartCoroutine(InsertDataGameHistoryCoroutine(gameID, bet, "EVEN", attemptId));
            }

            else if(data.slotType == SpecialSlotType.FIRST_COLOUMB)
            {
                StartCoroutine(InsertDataGameHistoryCoroutine(gameID, bet, "1st Column", attemptId));
            }

            else if(data.slotType == SpecialSlotType.SECOND_COLOUMB)
            {
                StartCoroutine(InsertDataGameHistoryCoroutine(gameID, bet, "2nd Column", attemptId));
            }

            else if(data.slotType == SpecialSlotType.THIRD_COLOUMB)
            {
                StartCoroutine(InsertDataGameHistoryCoroutine(gameID, bet, "3rd Column", attemptId));
            }
        }

        else
        {
            int betNumber = data.index;
            StartCoroutine(InsertDataGameHistoryCoroutine(gameID,betNumber, bet, "Single", attemptId));
        }

    }

    BetManager.Instance.changeLog.Clear();
  }


  public void UpdateDataToTable(string result, int payout)
  {
    totalBet = int.Parse(betAmount.text);
    if(result == "You Lost")
    {
        StartCoroutine(PayoutCredit(gameID, totalBet , "Loss", payout));
        StartCoroutine(UpdateUserCredit(0));
    }

    else
    {
        StartCoroutine(PayoutCredit(gameID, totalBet , "Win", payout));
        StartCoroutine(UpdateUserCredit(payout));
    }
  }

    // Coroutine for Games


    IEnumerator GetUserCredit()
    {
        try
        {
           // string url = Application.absoluteURL;
           string url = "http://15.207.110.133:8098/roulette.html?Id=34";
            Uri uri = new Uri(url);
            string user = uri.Query.Substring(1).Split('=')[1];
            userId = int.Parse(user);
            Debug.Log("Received User  ID: " + userId);
            gotData = true;
        }

        catch(Exception exception)
        {
            Debug.Log(exception);
        }

        if(gotData)
            {
                UnityWebRequest request = UnityWebRequest.Get(apiUrl+"/getUserCredit/" + userId);

                yield return request.SendWebRequest();

                if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError(request.error);
                }
                else
                {
                    int walletbalance = 0;
                    
                    try
                    {
                        string json = request.downloadHandler.text;
                        string[] s =  json.Split(":");
                        s[1] = Regex.Replace(s[1], @"[}]]", "");
                        walletbalance = int.Parse(s[1]);
                    }

                    catch(Exception exception)
                    {
                        Debug.Log(exception);
                        walletbalance = 1000;
                    }

                    finally
                    {
                        Debug.Log("Wallet "+ walletbalance);
                        BetManager.Instance.SetWalletValue(walletbalance);
                        StartCoroutine(GetUserName());
                    }
                    
                }
            }
        
      
    }

     IEnumerator GetUserName()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl+"/getUserDetail/" + userId);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            string[] s =  json.Split(":");
            s[1] = Regex.Replace(s[1], @"[}]]", "");
            s[1] = s[1].Replace('"', ' ').Trim();
            playerName.text = "PLAYER NAME : " + s[1];
        }
    }

    IEnumerator GetUserBalance()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://15.207.110.133:/api/"+"GetBalance"+"/34");

        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // string json = request.downloadHandler.text;
            // string[] s =  json.Split(":");
            // s[1] = Regex.Replace(s[1], @"[}]]", "");
            Debug.Log(request.downloadHandler.text);
        }
    }

    public class DatabaseAccess 
    {
        public int Balance;
    }

    IEnumerator UpdateUserCredit(int payout)
    {
        Debug.Log("Update");
        WWWForm form = new WWWForm();

        DatabaseAccess databaseAccess = new DatabaseAccess();
        int amount = int.Parse(walletAmount.text);
        databaseAccess.Balance = amount + payout;
        Debug.Log(databaseAccess.Balance);

        string jsonData = JsonUtility.ToJson(databaseAccess);

        using (UnityWebRequest www = UnityWebRequest.Put(apiUrl + "/updateUserCredit/" + userId, jsonData))
        {
            www.SetRequestHeader("Content-Type","application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }

            else
            {
                Debug.Log("User Credits Inserted Successfully");
            }
        }
    }

    private IEnumerator InsertDataGameHistoryCoroutine(int gameID,  int betAmount, string betType, int attemptId)
    {

        Debug.Log("Insert");
    //Multiple
        WWWForm form = new WWWForm();

        form.AddField("UserId", userId);
        form.AddField("GameId", gameID);
        form.AddField("BetAmount", betAmount);
        form.AddField("BetType", betType);
        form.AddField("AttemptId", attemptId);

        

        using (UnityWebRequest www = UnityWebRequest.Post(apiUrl + "/addGameHistory", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Game History Data Inserted Successfully");
            }
        }
    }
   private IEnumerator InsertDataGameHistoryCoroutine(int gameID, int betNumber, int betAmount, string betType, int attemptId)

   {        
    Debug.Log("Insert");
    // Single
        WWWForm form = new WWWForm();

        form.AddField("UserId", userId);
        form.AddField("GameId", gameID);
        form.AddField("BetNumber",betNumber);
        form.AddField("BetAmount", betAmount);
        form.AddField("BetType", betType);
        form.AddField("AttemptId", attemptId);

        

        using (UnityWebRequest www = UnityWebRequest.Post(apiUrl + "/addGameHistory", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Game History Data Inserted Successfully");
            }
        }
    }


    private IEnumerator PayoutCredit(int gameID,int betAmount, string result, double payout)
    {
        WWWForm form = new WWWForm();
        attemptId = UnityEngine.Random.Range(1,200) + UnityEngine.Random.Range(201,500) + UnityEngine.Random.Range(501,800);
        form.AddField("PayoutId", attemptId);
        form.AddField("UserId", userId);
        form.AddField("GameId", gameID);
        form.AddField("BetAmount", betAmount);
        form.AddField("Result", result);
        form.AddField("WinLossAmount", payout.ToString());

        UnityWebRequest www = UnityWebRequest.Post(apiUrl + "/insertPayoutCredits",form);

        yield return www.SendWebRequest();
        
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
            
        }
        else
        {
            Debug.Log(attemptId);
            Debug.Log("Payout credits inserted successfully.");
        }

        www.Dispose();
    }
    
}