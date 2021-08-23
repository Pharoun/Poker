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
    public Image pic;
    public Text name1;

    //public Image pic2;
    //public Text name2;

    //public Image pic3;
    //public Text name3;
    //public Image pic4;
    //public Text name4;

    public Sprite[] chipsImages;
    public Sprite[] avatars;

    private int betTotal;
    //private int count;

    string randomString = string.Empty;

    GameObject[] players;

    void Start()
    {

        //pic2 = GameObject.FindGameObjectWithTag("pic2").GetComponent<Image>();
        //name2 = GameObject.FindGameObjectWithTag("name2").GetComponent<Text>();
        pic.sprite = avatars[Random.Range(0, 6)];
        CreateRandomString(7);
        name1.text = randomString;

        //count = 0;
        players = GameObject.FindGameObjectsWithTag("Player");

        //foreach (GameObject go in players)
        //{
        //    count++;
        //}

        CmdShowPlayer();
    }

    [Command(requiresAuthority = false)]
    void CmdShowPlayer()
    {
        RpcShowPlayer();
    }

    [ClientRpc]
    void RpcShowPlayer()
    {

        if (hasAuthority)
        {

            gameObject.transform.parent = GameObject.FindGameObjectWithTag("player1").transform;
            gameObject.transform.localPosition = new Vector3(-70, 0, 0);
            gameObject.transform.localScale = new Vector3(1, 1, 1);

            //foreach (GameObject go in players)
            //{
            //    if (go != gameObject)
            //    {
            //        foreach (Transform child in go.transform)
            //        {
            //            if (child.tag == "pic")
            //                pic2.sprite = child.GetComponent<Image>().sprite;

            //            if (child.tag == "name")
            //                name2.text = child.GetComponent<Text>().text;
            //        }
            //    }
            //}

        }
        else
        {
            if(players.Length == 2)
            {
                gameObject.transform.parent = GameObject.FindGameObjectWithTag("player2").transform;
                gameObject.transform.localPosition = new Vector3(0, 0, 0);
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }

            if (players.Length == 3)
            {
                gameObject.transform.parent = GameObject.FindGameObjectWithTag("player3").transform;
                gameObject.transform.localPosition = new Vector3(0, 0, 0);
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }

            if (players.Length == 4)
            {
                gameObject.transform.parent = GameObject.FindGameObjectWithTag("player4").transform;
                gameObject.transform.localPosition = new Vector3(0, 0, 0);
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }

            slider.gameObject.SetActive(false);
            buttons.SetActive(false);
        }
    }

    private void CreateRandomString(int stringLength = 7)
    {
        int _stringLength = stringLength - 1;
        string[] characters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        for (int i = 0; i <= _stringLength; i++)
        {
            randomString = randomString + characters[Random.Range(0, characters.Length)];
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
