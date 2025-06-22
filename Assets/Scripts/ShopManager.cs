using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Base baseunit;
    [SerializeField] private List<AntUnit> units;
    [SerializeField] private int playerResource = 500;
    [SerializeField] private int upgradeUnitCost = 20;
    [SerializeField] private int upgradeBaseCost = 100;

    [SerializeField] private TextMeshProUGUI unit1Text;
    [SerializeField] private TextMeshProUGUI unit2Text;
    [SerializeField] private TextMeshProUGUI unit3Text;
    [SerializeField] private TextMeshProUGUI baseText;

    [SerializeField] private TextMeshProUGUI resourceText;
    [SerializeField] private TextMeshProUGUI costHPUnitText;
    [SerializeField] private TextMeshProUGUI costATKUnitText;
    [SerializeField] private TextMeshProUGUI costBaseText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateUnitStatus();
        UpdateShopUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void upgradeUnitHP(int index)
    {
        if (playerResource >= upgradeUnitCost)
        {
            playerResource -= upgradeUnitCost;
            units[index].UpgradeHP(20);
            UpdateUnitStatus();
            UpdateShopUI();
        }
    }

    public void upgradeUnitATK(int index)
    {
        if (playerResource >= upgradeUnitCost)
        {
            playerResource -= upgradeUnitCost;
            units[index].UpgradeATK(5);
            UpdateUnitStatus();
            UpdateShopUI();
        }
    }

    public void upgradeBase()
    {
        if (playerResource >= upgradeBaseCost)
        {
            playerResource -= upgradeBaseCost;
            baseunit.upgradeBase(20);
            UpdateUnitStatus();
            UpdateShopUI();
        }
    }

    void UpdateUnitStatus()
    {
        unit1Text.text = $"{units[0].GetUnitType()}\nHP: {units[0].GetMaxHP()}\nATK: {units[0].GetCurrentATK()}";
        unit2Text.text = $"{units[1].GetUnitType()}\nHP: {units[1].GetMaxHP()}\nATK: {units[1].GetCurrentATK()}";
        unit3Text.text = $"{units[2].GetUnitType()}\nHP: {units[2].GetMaxHP()}\nATK: {units[2].GetCurrentATK()}";

        baseText.text = $"Base HP: {baseunit.getBaseHP()}";
    }

    void UpdateShopUI()
    {
        resourceText.text = $"Resource: {playerResource}";
        costHPUnitText.text = $"HP Upgrade Cost: {upgradeUnitCost}";
        costATKUnitText.text = $"ATK Upgrade Cost: {upgradeUnitCost}";
        costBaseText.text = $"Base Upgrade Cost: {upgradeBaseCost}";
    }
}
