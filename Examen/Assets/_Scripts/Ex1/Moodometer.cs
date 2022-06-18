using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moodometer : MonoBehaviour
{
    private Slider _slider;
    public Gradient ColorGradient;
    public Image FillImage;
  
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    public void SetValue(float f)
    {
        
        _slider.value = f;
        FillImage.color = ColorGradient.Evaluate(f);

        _slider.gameObject.SetActive(true);
    }
}
