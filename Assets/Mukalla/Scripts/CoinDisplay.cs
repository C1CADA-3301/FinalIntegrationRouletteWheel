using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    // Start is called before the first frame update
    [SerializeField] bool isAllCoinIn;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Image glowImage;
    [SerializeField] BetCoinDataSO coinDataSO;

    private void Start() {

    }
    public void OnClick() {
        if (isAllCoinIn == true) {
            BetManager.Instance.SetBetCoinData(BetManager.Instance.GetWalletAmount());
        } else {
            BetManager.Instance.SetBetCoinData(coinDataSO.coinValue);
        }
    }


    public void OnPointerEnter(PointerEventData eventData) {
        transform.localScale += new Vector3(0.3f,0.3f,1f);
        audioSource.Play();
        glowImage.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        transform.localScale -= new Vector3(0.3f,0.3f,1f);
        audioSource.Stop();
        glowImage.gameObject.SetActive(false);
    }
}
