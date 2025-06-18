using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text sugarCountText; 

    void Start()
    {
        SugarInteraction.sugarUIText = sugarCountText;

        // Initialize the UI text with the starting value.
        if (sugarCountText != null)
        {
            sugarCountText.text = "Sugar: " + SugarInteraction.totalSugar;
        }
        else
        {
            Debug.LogError("Sugar Count Text is not assigned in the UIManager!");
        }
    }
}
