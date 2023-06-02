using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoAmount : MonoBehaviour
{
    [SerializeField] GameObject NoBalanceDisplay;
    [SerializeField] Animator NoBalanceDisplayAnim;
    [SerializeField] Button[] buttons;

    float timer = 0;
    bool stopanim;

    private void Start()
    {
        NoBalanceDisplay.SetActive(false);

        foreach (var button in buttons)
        {
            button.onClick.AddListener(disabler);
        }
    }

    private void Update()
    {
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
    public void disabler()
    {
        BetManager.Instance.HasSufficientBalance();
        print("Wallet Empty - " + BetManager.hasnobalance);
        if (BetManager.hasnobalance)
        {
            AudioManager.Instance.Play(AudioType.Nofunds);
            NoBalanceDisplay.SetActive(true);
            NoBalanceDisplayAnim.SetBool("AnimPlay", true);
            stopanim = true;
        }
        else
        {
            NoBalanceDisplay.SetActive(false);
        }
    }
}
