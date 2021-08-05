using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{

    public Slider slider;
    public Text betText;
    public Sprite[] chipsImages;
    public Image chips;
    private int betTotal;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void sliderValue()
    {
        int a = (int)slider.value;
        a *= 1000;
        betText.text = a.ToString();
    }

    public void Raise()
    {
        int a = (int)slider.value * 1000;
        a += betTotal;

        if (a <= 20000)
        {
            betTotal = a;
            chips.sprite = chipsImages[(betTotal / 1000) - 1];
        }
        else
        {
            betTotal = 20000;
            chips.sprite = chipsImages[(betTotal / 1000) - 1];
        }
    }

}
