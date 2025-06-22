// SugarManager.cs

using UnityEngine;
using TMPro;

public class SugarManager : MonoBehaviour
{
    public static SugarManager instance; 

    [Header("Game State")]
    public int totalSugar = 0;

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

    }

    public void AddSugar(int amount)
    {
        totalSugar += amount;
    }

    public bool SpendSugar(int amount)
    {
        if (totalSugar >= amount)
        {
            totalSugar -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough sugar!");
            return false;
        }
    }
}