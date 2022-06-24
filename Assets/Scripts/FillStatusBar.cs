using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStatusBar : MonoBehaviour
{

    public Image fillImage;
    public Slider slider;
    private float maxVal;

    void Start()
    {
        maxVal = slider.maxValue;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaxVal(int val)
    {
        slider.maxValue = val;
        slider.value = val;
    }

    public void UpdateStatusBar(int curVal)
    {
        float fillValue = curVal / maxVal;
        slider.value = fillValue;
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }
        
        if (fillValue > slider.maxValue / 2)
        {
            fillImage.color = Color.green;
        }
        if (fillValue < slider.maxValue / 3)
        {
            fillImage.color = Color.red;
        }
        else if (fillValue < slider.maxValue / 2)
        {
            fillImage.color = Color.yellow;
        }
    }
}
