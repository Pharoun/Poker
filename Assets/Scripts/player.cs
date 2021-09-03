using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.SceneManagement;

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

    GameObject mainMenuButton;
    GameObject leaveButton;


    public void listAdded(gameHelper.PlayerVals plv)
    {
        defineVars();

        log.text += "list add \n";
        log.text += playerName + " \n";
        log.text += plv.name + " \n";

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

    public void listremove(gameHelper.PlayerVals plv)
    {
        defineVars(); 

        Debug.Log("removed");
        log.text += "removed \n";

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

        defineVars();

        if(isLocalPlayer)
        {

            mainMenuButton.GetComponent<Button>().interactable = false;
            leaveButton.GetComponent<Button>().interactable = true;

            name1.GetComponent<Text>().text = gameHelp.avatarName;
            pic1.GetComponent<Image>().sprite = avatars[gameHelp.avatarPic];

            gameObject.transform.parent = GameObject.FindGameObjectWithTag("canvas").transform;
            gameObject.transform.localPosition = new Vector3(0, -180, 0);
            gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        }

        gameHelp.clientActivePlayer();

        log.text += "client joined \n";

    }

    public void clientExit()
    {
        defineVars();

        mainMenuButton.GetComponent<Button>().interactable = true;
        leaveButton.GetComponent<Button>().interactable = false;

        Debug.Log("sending remove");
        log.text += "sending remove \n";

        gameHelp.discActivePlayer();

        Debug.Log("sending remove1");
        log.text += "sending remove1 \n";

        name1.GetComponent<Text>().text = string.Empty;
        pic1.GetComponent<Image>().sprite = noImage;
        name2.GetComponent<Text>().text = string.Empty;
        pic2.GetComponent<Image>().sprite = noImage;
        name3.GetComponent<Text>().text = string.Empty;
        pic3.GetComponent<Image>().sprite = noImage;
        name4.GetComponent<Text>().text = string.Empty;
        pic4.GetComponent<Image>().sprite = noImage;

        Debug.Log("sending remove2");
        log.text += "sending remove2 \n";

    }

    public void mainMenu()
    {
        NetworkManager nw = FindObjectOfType<NetworkManager>();

        if (NetworkServer.active && NetworkClient.isConnected)
        {
            nw.StopHost();
        }
        else if (NetworkClient.isConnected)
        {
            nw.StopClient();
        }
        else if (NetworkServer.active)
        {
            nw.StopServer();
        }

        SceneManager.LoadScene("Profile");
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

    void defineVars()
    {
        mainMenuButton = GameObject.FindGameObjectWithTag("mainButton");
        leaveButton = GameObject.FindGameObjectWithTag("leaveButton");
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
    }

}
