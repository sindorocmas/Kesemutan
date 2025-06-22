// SugarManager.cs

using UnityEngine;
using TMPro;

public class SugarManager : MonoBehaviour
{
    public static SugarManager instance; 

    [Header("UI References")]
    public TMP_Text sugarCountText;

    [Header("Game State")]
    public int totalSugar;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        totalSugar = 150;
        UpdateUI();
    }

    public void AddSugar(int amount)
    {
        totalSugar += amount;
        UpdateUI();
    }

    public bool SpendSugar(int amount)
    {
        if (totalSugar >= amount)
        {
            totalSugar -= amount;
            UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("Not enough sugar!");
            return false;
        }
    }

    private void UpdateUI()
    {
        if (sugarCountText != null)
        {
            sugarCountText.text = "Sugar: " + totalSugar;
        }
        else
        {
            Debug.LogError("Sugar Count Text is not assigned in the SugarManager!");
        }
    }
}