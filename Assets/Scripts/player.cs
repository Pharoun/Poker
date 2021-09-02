using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class player : NetworkBehaviour
{

    public Slider slider;
    public GameObject buttons;
    public Text betText;
    public Image chips;

    public Sprite[] chipsImages;
    public Sprite[] avatars;

    private int betTotal;

    string playerName;
    //int playerPic;

    GameObject pic1;
    GameObject pic2;
    GameObject pic3;
    GameObject pic4;

    GameObject name1;
    GameObject name2;
    GameObject name3;
    GameObject name4;

    gameHelper help;

    InputField log;


    public void Start()
    {
        log = GameObject.FindGameObjectWithTag("log").GetComponent<InputField>();
        help = GameObject.FindGameObjectWithTag("gameHelper").GetComponent<gameHelper>();

        pic1 = GameObject.FindGameObjectWithTag("player1").transform.GetChild(0).gameObject;
        name1 = GameObject.FindGameObjectWithTag("player1").transform.GetChild(1).gameObject;

        name1.GetComponent<Text>().text = help.avatarName;
        pic1.GetComponent<Image>().sprite = avatars[help.pic];

        //playerName = help.avatarName; 
        //playerPic = help.pic;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        log = GameObject.FindGameObjectWithTag("log").GetComponent<InputField>();
        help = GameObject.FindGameObjectWithTag("gameHelper").GetComponent<gameHelper>();
        name1 = GameObject.FindGameObjectWithTag("player1").transform.GetChild(1).gameObject;

        playerName = help.avatarName;
        //playerName = name1.GetComponent<Text>().text;
        //playerPic = help.pic;

        help.clientActivePlayer();

        log.text += "client joined \n";

        pic1 = GameObject.FindGameObjectWithTag("player1").transform.GetChild(0).gameObject;
        name1 = GameObject.FindGameObjectWithTag("player1").transform.GetChild(1).gameObject;
        pic2 = GameObject.FindGameObjectWithTag("player2").transform.GetChild(0).gameObject;
        name2 = GameObject.FindGameObjectWithTag("player2").transform.GetChild(1).gameObject;
        pic3 = GameObject.FindGameObjectWithTag("player3").transform.GetChild(0).gameObject;
        name3 = GameObject.FindGameObjectWithTag("player3").transform.GetChild(1).gameObject;
        pic4 = GameObject.FindGameObjectWithTag("player4").transform.GetChild(0).gameObject;
        name4 = GameObject.FindGameObjectWithTag("player4").transform.GetChild(1).gameObject;

        foreach (gameHelper.PlayerVals pl in help.ActivePlayers)
        {

            log.text += "playername: " + playerName + " \n";
            log.text += "pl.name: " + pl.name + " \n";

            if (playerName != pl.name && playerName != string.Empty && playerName != null)
            {

                if (name2.GetComponent<Text>().text == string.Empty && name1.GetComponent<Text>().text != pl.name)
                {
                    name2.GetComponent<Text>().text = pl.name;
                    pic2.GetComponent<Image>().sprite = avatars[pl.pic];
                    break;
                }
                else if (name3.GetComponent<Text>().text == string.Empty && name1.GetComponent<Text>().text != pl.name && name2.GetComponent<Text>().text != pl.name)
                {
                    name3.GetComponent<Text>().text = pl.name;
                    pic3.GetComponent<Image>().sprite = avatars[pl.pic];
                    break;
                }
                else if (name4.GetComponent<Text>().text == string.Empty && name1.GetComponent<Text>().text != pl.name && name2.GetComponent<Text>().text != pl.name && name3.GetComponent<Text>().text != pl.name)
                {
                    name4.GetComponent<Text>().text = pl.name;
                    pic4.GetComponent<Image>().sprite = avatars[pl.pic];
                    break;
                }
            }
        }

        log.text += "end client joined \n";

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
