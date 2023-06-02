using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsSound : MonoBehaviour
{
    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(Sounddd);
    }

   void Sounddd()
    {
        AudioManager.Instance.Play(AudioType.ButtonClick);
    }
}
