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

    GameObject[] players;

    int player1pic;
    int player2pic;
    int player3pic;
    int player4pic;

    string name1string;
    string name2string;
    string name3string;
    string name4string;

    GameObject pic1;
    GameObject pic2;
    GameObject pic3;
    GameObject pic4;

    GameObject name1;
    GameObject name2;
    GameObject name3;
    GameObject name4;

    GameObject helperObject;

    void Start()
    {
        
    }

    public void beginning()
    {
        helperObject = GameObject.FindGameObjectWithTag("helper");
        helper helperClass = helperObject.GetComponent<helper>();

        players = GameObject.FindGameObjectsWithTag("Player");

        Debug.Log("player script players: " + players.Length);

        if (players.Length == 1)
        {
            player1pic = helperClass.player1Pic;
            name1string = helperClass.player1Name;
        }

        if (players.Length == 2)
        {
            player2pic = helperClass.player2Pic;
            name2string = helperClass.player2Name;
        }

        if (players.Length == 3)
        {
            player3pic = helperClass.player3Pic;
            name3string = helperClass.player3Name;
        }

        if (players.Length == 4)
        {
            player4pic = helperClass.player4Pic;
            name4string = helperClass.player4Name;
        }

        CmdShowPlayer(player1pic, name1string, player2pic, name2string, player3pic, name3string, player4pic, name4string);
    }

    [Command(requiresAuthority = false)]
    void CmdShowPlayer(int player1pic, string name1string, int player2pic, string name2string, int player3pic, string name3string, int player4pic, string name4string)
    {
        RpcShowPlayer(player1pic, name1string, player2pic, name2string, player3pic, name3string, player4pic, name4string);
    }

    [ClientRpc]
    void RpcShowPlayer(int player1pic, string name1string, int player2pic, string name2string, int player3pic, string name3string, int player4pic, string name4string)
    {

        pic1 = GameObject.FindGameObjectWithTag("player1").transform.GetChild(0).gameObject;
        name1 = GameObject.FindGameObjectWithTag("player1").transform.GetChild(1).gameObject;
        pic2 = GameObject.FindGameObjectWithTag("player2").transform.GetChild(0).gameObject;
        name2 = GameObject.FindGameObjectWithTag("player2").transform.GetChild(1).gameObject;
        pic3 = GameObject.FindGameObjectWithTag("player3").transform.GetChild(0).gameObject;
        name3 = GameObject.FindGameObjectWithTag("player3").transform.GetChild(1).gameObject;
        pic4 = GameObject.FindGameObjectWithTag("player4").transform.GetChild(0).gameObject;
        name4 = GameObject.FindGameObjectWithTag("player4").transform.GetChild(1).gameObject;
        players = GameObject.FindGameObjectsWithTag("Player");

        if (hasAuthority)
        {
            pic1.GetComponent<Image>().sprite = avatars[player1pic];
            name1.GetComponent<Text>().text = name1string;
        }
        else
        {
            if (players.Length == 2)
            {
                pic1.GetComponent<Image>().sprite = avatars[player1pic];
                name1.GetComponent<Text>().text = name1string;
                pic2.GetComponent<Image>().sprite = avatars[player2pic];
                name2.GetComponent<Text>().text = name2string;
            }

            if (players.Length == 3)
            {
                pic1.GetComponent<Image>().sprite = avatars[player1pic];
                name1.GetComponent<Text>().text = name1string;
                pic2.GetComponent<Image>().sprite = avatars[player2pic];
                name2.GetComponent<Text>().text = name2string;
                pic3.GetComponent<Image>().sprite = avatars[player3pic];
                name3.GetComponent<Text>().text = name3string;
            }

            if (players.Length == 4)
            {
                pic1.GetComponent<Image>().sprite = avatars[player1pic];
                name1.GetComponent<Text>().text = name1string;
                pic2.GetComponent<Image>().sprite = avatars[player2pic];
                name2.GetComponent<Text>().text = name2string;
                pic3.GetComponent<Image>().sprite = avatars[player3pic];
                name3.GetComponent<Text>().text = name3string;
                pic4.GetComponent<Image>().sprite = avatars[player4pic];
                name4.GetComponent<Text>().text = name4string;
            }

            slider.gameObject.SetActive(false);
            buttons.SetActive(false);
        }
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
