using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private string unitName;

    [Header("BaseStats")]
    [SerializeField] private int baseHP;
    [SerializeField] private int baseATK;

    [Header("CurrentStats")]
    [SerializeField] private int currentHP;
    [SerializeField] private int currentATK;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = baseHP;
        currentATK = baseATK;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeHP(int amount)
    {
        currentHP += amount;
    }

    public void UpgradeATK(int amount)
    {
        currentATK += amount;
    }

    public int GetCurrentHP() => currentHP;
    public int GetCurrentATK() => currentATK;
    public string GetUnitName() => unitName;
}
