using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureSlider : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;

    public Image fill;

    public void SetMaxTemperature(int temperature){
        slider.maxValue = temperature;
        slider.value = temperature;

        fill.color = gradient.Evaluate(0f);
    }

    public void SetTemperature(int temperature){
        slider.value = temperature;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
