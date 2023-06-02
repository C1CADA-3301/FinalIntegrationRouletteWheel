using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLParameterHandler : MonoBehaviour
{
    void Start() {
        //string url = Application.absoluteURL;
        /*string url = "http://15.207.110.133:8098/roulette.html/1";
        string[] pathSegments = url.Split('/');

        string lastSegment = pathSegments[pathSegments.Length - 1];

        int playerID;
        if (int.TryParse(lastSegment, out playerID)) {
            Debug.Log("Player ID: " + playerID);
        }*/
        //string url = Application.absoluteURL;
        string url = "https://example.com/unitygame?userID=${12345}";

        if (url.Contains("?")) {
            string query = url.Substring(url.IndexOf('?') + 1);
            string[] parameters = query.Split('&');
            foreach (string parameter in parameters) {
                string[] keyValue = parameter.Split('=');
                if (keyValue[0] == "userID") {
                    string userID = keyValue[1].Replace("${", "").Replace("}", "");
                    Debug.Log("User ID: " + userID);
                }
            }
        }
    }
}
