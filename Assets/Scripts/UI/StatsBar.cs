using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    [SerializeField] private TextMeshProUGUI textMesh;


    public void SetMaxValue(float value)
    {
        slider.maxValue = value;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetInitialValue(float value)
    {
        slider.maxValue = value;
        slider.value = value;

        fill.color = gradient.Evaluate(1f);

        textMesh.text = value.ToString("0.##");
    }

    public void SetValue(float value)
    {
        slider.value = value;

        fill.color = gradient.Evaluate(slider.normalizedValue);

        textMesh.text = value.ToString("0.##");
    }
}