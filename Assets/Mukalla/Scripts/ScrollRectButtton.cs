using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectButtton : MonoBehaviour
{
    // Start is called before the first frame update
    public ScrollRect myScrollRect;
    public Button myScrollRectButton;
    
    public void OnButtonClick()
    {
        myScrollRect.gameObject.SetActive(true);
        myScrollRectButton.gameObject.SetActive(false);
    }
}
