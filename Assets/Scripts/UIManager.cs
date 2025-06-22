using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public TMP_Text sugarCountText;
    public TMP_Text healBaseText;
    public TMP_Text dropRateUpgradeText;
    public TMP_Text coinsText;
    public GameObject upgradePopup;
    public GameObject summaryPopup;
    public List<GameObject> stars;
    public static UIManager instance;

    void Start()
    {
        instance = this;
        
        foreach (GameObject star in stars)
        {
            star.SetActive(false);
        }
    }

    void Update()
    {
        if (sugarCountText != null)
        {
            sugarCountText.text = "Sugar: " + SugarManager.instance.totalSugar;
        }
    }

    public void showSummary(int coins, int starAmount)
    {
        foreach (GameObject star in stars) {
            if (starAmount > 0)
            {
                star.SetActive(true);
                starAmount--;
            }
            else
            {
                return;
            }
        }
        coinsText.text = coins.ToString();
        summaryPopup.SetActive(true);
    }

    public void hideSummary()
    {
        summaryPopup.SetActive(false);
    }

    public void showUpgrade()
    {
        if (healBaseText != null)
        {
            healBaseText.text = "Heal Base 50";
        }

        if (dropRateUpgradeText != null)
        {
            dropRateUpgradeText.text = "Drop Rate " + DropManager.instance.dropMultiplierPrice;
        }
        upgradePopup.SetActive(true);
    }


    public void hideUpgrade()
    {
        upgradePopup.SetActive(false);
    }
}
