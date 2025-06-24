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
    public TMP_Text dropRateLevel;

    public Slider enemyProgressBar;
    public TextMeshProUGUI enemyProgressText;

    public GameObject upgradePopup;
    public GameObject summaryPopup;
    public GameObject losePopup;
    public List<GameObject> stars;
    public static UIManager instance;

    void Start()
    {
        instance = this;

        losePopup.SetActive(false);  

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
        foreach (GameObject star in stars)
        {
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
        if (dropRateLevel != null)
        {
            dropRateLevel.text = "Lvl" + DropManager.instance.getDropUpgradeLevel();
        }
        if (healBaseText != null)
        {
            healBaseText.text = "50";
        }

        if (dropRateUpgradeText != null)
        {
            dropRateUpgradeText.text = "" + DropManager.instance.dropMultiplierPrice;
        }
        upgradePopup.SetActive(true);
    }


    public void hideUpgrade()
    {
        upgradePopup.SetActive(false);
    }

    public void healBase()
    {
        if (SugarManager.instance.totalSugar >= 50)
        {
            PlayerBase.instance.heal(50);
        }
    }

    public void UpdateEnemyProgress(int enemiesDefeated, int totalEnemies)
    {
        if (enemyProgressBar == null) return;

        enemyProgressBar.maxValue = totalEnemies;
        enemyProgressBar.value = enemiesDefeated;

        if (enemyProgressText != null)
        {
            enemyProgressText.text = (int)(((float)enemiesDefeated / (float)totalEnemies) * 100) + "%";
        }
    }

    public void showLosePopup()
    {
        losePopup.SetActive(true);  
    }
}
