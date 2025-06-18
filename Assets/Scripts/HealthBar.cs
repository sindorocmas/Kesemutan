using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public void UpdateHealth(int current, int max)
    {
        slider.maxValue = max;
        slider.value = current;
    }
}