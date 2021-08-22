using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class player : NetworkBehaviour
{

    public Slider slider;
    public Text betText;
    public Image chips;
    public Image pic;

    public Image chips2;
    public Image pic2;

    public Sprite[] chipsImages;
    public Sprite[] avatars;
    private int betTotal;

    void Start()
    {

        chips2 = GameObject.FindGameObjectWithTag("chips2").GetComponent<Image>();
        pic2 = GameObject.FindGameObjectWithTag("pic2").GetComponent<Image>();

        if (isLocalPlayer)
        {

            gameObject.transform.parent = GameObject.FindGameObjectWithTag("players").transform;
            gameObject.transform.localPosition = new Vector3(0, -210, 0);
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

            chips.sprite = chipsImages[0];
            pic.sprite = avatars[0];
        }
        else
        {
            chips2.sprite = chipsImages[0];
            pic2.sprite = avatars[1];
            // control player auto creation
        }
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
            chips.sprite = chipsImages[(betTotal / 1000)];
        }
        else
        {
            betTotal = 20000;
            chips.sprite = chipsImages[(betTotal / 1000)];
        }
    }

}
