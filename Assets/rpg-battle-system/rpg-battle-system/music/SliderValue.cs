using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderValue : MonoBehaviour
{
    public TMPro.TextMeshProUGUi TextValue;
    public Slider Slider;
    
    public void OnValueChanged(float newValue)
    {
        int valueInt = (int)Mathf.Round(newValue * 100.0f);
        TextValue.text = valueInt.ToString();
    }
}
