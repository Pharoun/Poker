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
    public Sprite noImage;

    public Sprite[] chipsImages;
    public Sprite[] avatars;

    private int betTotal;

    public string playerName;
    public int playerPic;

    GameObject pic1;
    GameObject pic2;
    GameObject pic3;
    GameObject pic4;

    GameObject name1;
    GameObject name2;
    GameObject name3;
    GameObject name4;

    gameHelper gameHelp;

    InputField log;


    public void Start()
    {
        pic1 = GameObject.FindGameObjectWithTag("player1").transform.GetChild(0).gameObject;
        gameHelp = GameObject.FindGameObjectWithTag("gameHelper").GetComponent<gameHelper>();
        name1 = GameObject.FindGameObjectWithTag("player1").transform.GetChild(1).gameObject;
        name1.GetComponent<Text>().text = gameHelp.avatarName;
        pic1.GetComponent<Image>().sprite = avatars[gameHelp.avatarPic];

        gameObject.transform.parent = GameObject.FindGameObjectWithTag("canvas").transform;
        gameObject.transform.localPosition = new Vector3(0, -180, 0);
        gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    public void listAdded(gameHelper.PlayerVals plv)
    {
        log = GameObject.FindGameObjectWithTag("log").GetComponent<InputField>();
        gameHelp = GameObject.FindGameObjectWithTag("gameHelper").GetComponent<gameHelper>();
        pic1 = GameObject.FindGameObjectWithTag("player1").transform.GetChild(0).gameObject;
        name1 = GameObject.FindGameObjectWithTag("player1").transform.GetChild(1).gameObject;
        pic2 = GameObject.FindGameObjectWithTag("player2").transform.GetChild(0).gameObject;
        name2 = GameObject.FindGameObjectWithTag("player2").transform.GetChild(1).gameObject;
        pic3 = GameObject.FindGameObjectWithTag("player3").transform.GetChild(0).gameObject;
        name3 = GameObject.FindGameObjectWithTag("player3").transform.GetChild(1).gameObject;
        pic4 = GameObject.FindGameObjectWithTag("player4").transform.GetChild(0).gameObject;
        name4 = GameObject.FindGameObjectWithTag("player4").transform.GetChild(1).gameObject;

        if (playerName != plv.name && playerName != string.Empty && playerName != null)
        {

            if (name2.GetComponent<Text>().text == string.Empty && name1.GetComponent<Text>().text != plv.name)
            {
                name2.GetComponent<Text>().text = plv.name;
                pic2.GetComponent<Image>().sprite = avatars[plv.pic];
            }
            else if (name3.GetComponent<Text>().text == string.Empty && name1.GetComponent<Text>().text != plv.name && name2.GetComponent<Text>().text != plv.name)
            {
                name3.GetComponent<Text>().text = plv.name;
                pic3.GetComponent<Image>().sprite = avatars[plv.pic];
            }
            else if (name4.GetComponent<Text>().text == string.Empty && name1.GetComponent<Text>().text != plv.name && name2.GetComponent<Text>().text != plv.name && name3.GetComponent<Text>().text != plv.name)
            {
                name4.GetComponent<Text>().text = plv.name;
                pic4.GetComponent<Image>().sprite = avatars[plv.pic];
            }
        }

    }

    public void listremove(gameHelper.PlayerVals plv)
    {
        log = GameObject.FindGameObjectWithTag("log").GetComponent<InputField>();
        gameHelp = GameObject.FindGameObjectWithTag("gameHelper").GetComponent<gameHelper>();
        pic1 = GameObject.FindGameObjectWithTag("player1").transform.GetChild(0).gameObject;
        name1 = GameObject.FindGameObjectWithTag("player1").transform.GetChild(1).gameObject;
        pic2 = GameObject.FindGameObjectWithTag("player2").transform.GetChild(0).gameObject;
        name2 = GameObject.FindGameObjectWithTag("player2").transform.GetChild(1).gameObject;
        pic3 = GameObject.FindGameObjectWithTag("player3").transform.GetChild(0).gameObject;
        name3 = GameObject.FindGameObjectWithTag("player3").transform.GetChild(1).gameObject;
        pic4 = GameObject.FindGameObjectWithTag("player4").transform.GetChild(0).gameObject;
        name4 = GameObject.FindGameObjectWithTag("player4").transform.GetChild(1).gameObject;

        if (name2.GetComponent<Text>().text == plv.name)
        {
            name2.GetComponent<Text>().text = string.Empty;
            pic2.GetComponent<Image>().sprite = noImage;
        }
        else if (name3.GetComponent<Text>().text == plv.name)
        {
            name3.GetComponent<Text>().text = string.Empty;
            pic3.GetComponent<Image>().sprite = noImage;
        }
        else if (name4.GetComponent<Text>().text == plv.name)
        {
            name4.GetComponent<Text>().text = string.Empty;
            pic4.GetComponent<Image>().sprite = noImage;
        }

    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        log = GameObject.FindGameObjectWithTag("log").GetComponent<InputField>();
        gameHelp = GameObject.FindGameObjectWithTag("gameHelper").GetComponent<gameHelper>();

        playerName = gameHelp.avatarName;
        playerPic = gameHelp.avatarPic;
        gameHelp.clientActivePlayer();

        log.text += "client joined \n";

        pic1 = GameObject.FindGameObjectWithTag("player1").transform.GetChild(0).gameObject;
        name1 = GameObject.FindGameObjectWithTag("player1").transform.GetChild(1).gameObject;
        pic2 = GameObject.FindGameObjectWithTag("player2").transform.GetChild(0).gameObject;
        name2 = GameObject.FindGameObjectWithTag("player2").transform.GetChild(1).gameObject;
        pic3 = GameObject.FindGameObjectWithTag("player3").transform.GetChild(0).gameObject;
        name3 = GameObject.FindGameObjectWithTag("player3").transform.GetChild(1).gameObject;
        pic4 = GameObject.FindGameObjectWithTag("player4").transform.GetChild(0).gameObject;
        name4 = GameObject.FindGameObjectWithTag("player4").transform.GetChild(1).gameObject;

        foreach (gameHelper.PlayerVals pl in gameHelp.ActivePlayers)
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

    public virtual void OnClientDisconnect(NetworkConnection conn)
    {
        gameHelp.discActivePlayer();
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
