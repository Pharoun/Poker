using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.SceneManagement;

public class player : NetworkBehaviour
{
    GameObject container;
    GameObject chips1;
    Slider slider;
    Text betText;

    public Sprite noImage;
    public Sprite noChips;
    public Sprite[] chipsImages;
    public Sprite[] avatars;

    int betTotal;

    GameObject pic1;
    GameObject pic2;
    GameObject pic3;
    GameObject pic4;

    GameObject name1;
    GameObject name2;
    GameObject name3;
    GameObject name4;

    GameObject chips2;
    GameObject chips3;
    GameObject chips4;

    GameObject player2Name;
    GameObject player3Name;
    GameObject player4Name;

    gameHelper gameHelp;

    InputField log;

    Transform quitPanel;

    //test//

    public GameObject card;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (isLocalPlayer)
        {
            gameObject.name = "local";

            defineVars();

            name1.GetComponent<Text>().text = gameHelp.avatarName;
            pic1.GetComponent<Image>().sprite = avatars[gameHelp.avatarPic];


            log.text += "is local end \n";

        }

        gameObject.transform.parent = GameObject.FindGameObjectWithTag("netPlayers").transform;
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        gameHelp = GameObject.FindGameObjectWithTag("gameHelper").GetComponent<gameHelper>();

        gameHelp.clientActivePlayer();

        log = GameObject.FindGameObjectWithTag("log").GetComponent<InputField>();
        log.text += "start end \n";
    }

    public void listAdded(gameHelper.PlayerVals plv)
    {
        defineVars();

        log.text += "list added start \n";

        if (name2.GetComponent<Text>().text == string.Empty && name1.GetComponent<Text>().text != plv.name)
        {
            name2.GetComponent<Text>().text = plv.name;
            player2Name.name = plv.name;
            pic2.GetComponent<Image>().sprite = avatars[plv.pic];
        }
        else if (name3.GetComponent<Text>().text == string.Empty && name1.GetComponent<Text>().text != plv.name && name2.GetComponent<Text>().text != plv.name)
        {
            name3.GetComponent<Text>().text = plv.name;
            player3Name.name = plv.name;
            pic3.GetComponent<Image>().sprite = avatars[plv.pic];
        }
        else if (name4.GetComponent<Text>().text == string.Empty && name1.GetComponent<Text>().text != plv.name && name2.GetComponent<Text>().text != plv.name && name3.GetComponent<Text>().text != plv.name)
        {
            name4.GetComponent<Text>().text = plv.name;
            player4Name.name = plv.name;
            pic4.GetComponent<Image>().sprite = avatars[plv.pic];
        }

        log.text += "list added end \n";
    }

    public void listremove(gameHelper.PlayerVals plv)
    {
        defineVars(); 
        Debug.Log("removed");
        log.text += plv.name + " removed \n";

        if (name2.GetComponent<Text>().text == plv.name)
        {
            name2.GetComponent<Text>().text = string.Empty;
            player2Name.name = "playerName";
            pic2.GetComponent<Image>().sprite = noImage;
            chips2.GetComponent<Image>().sprite = noChips;
        }
        else if (name3.GetComponent<Text>().text == plv.name)
        {
            name3.GetComponent<Text>().text = string.Empty;
            player3Name.name = "playerName";
            pic3.GetComponent<Image>().sprite = noImage;
            chips3.GetComponent<Image>().sprite = noChips;
        }
        else if (name4.GetComponent<Text>().text == plv.name)
        {
            name4.GetComponent<Text>().text = string.Empty;
            player4Name.name = "playerName";
            pic4.GetComponent<Image>().sprite = noImage;
            chips4.GetComponent<Image>().sprite = noChips;
        }

        log.text += "list remove end \n";

    }

    public void clientExit()
    {
        quitPanel = GameObject.FindGameObjectWithTag("quitPanel").transform.GetChild(0).transform;
        quitPanel.gameObject.SetActive(true);
    }

    public void sitStand()
    {
        GameObject standOut = GameObject.FindGameObjectWithTag("standOut");

        if(standOut.transform.GetChild(0).GetComponent<Text>().text == "Sit Back")
        {
            //sitting down
            defineVars();

            name1.GetComponent<Text>().text = gameHelp.avatarName;
            pic1.GetComponent<Image>().sprite = avatars[gameHelp.avatarPic];

            gameHelp.clientActivePlayer();
            container.SetActive(true);
            standOut.transform.GetChild(0).GetComponent<Text>().text = "Stand Out";
            GameObject leaveButton = GameObject.FindGameObjectWithTag("leave");
            leaveButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            //standing up
            defineVars();
            gameHelp.discActivePlayer();
            
            name1.GetComponent<Text>().text = string.Empty;
            pic1.GetComponent<Image>().sprite = noImage;
            slider.value = 0;
            betText.text = slider.value.ToString();
            chips1.GetComponent<Image>().sprite = noChips;
            container.SetActive(false);
            standOut.transform.GetChild(0).GetComponent<Text>().text = "Sit Back";
            GameObject leaveButton = GameObject.FindGameObjectWithTag("leave");
            leaveButton.GetComponent<Button>().interactable = true;
        }
    }

    public void yesButton()
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

    public void noButton()
    {
        quitPanel = GameObject.FindGameObjectWithTag("quitPanel").transform.GetChild(0).transform;
        quitPanel.gameObject.SetActive(false);
    }

    public void check()
    {
        //check
    }

    public void call()
    {
        //call
    }

    public void fold()
    {
        //fold
    }

    public void sliderValue()
    {

        defineVars();

        int a = (int)slider.value;
        a *= 1000;
        betText.text = a.ToString();
    }

    public void Raise()
    {

        if (isLocalPlayer)
        {
            defineVars();

            int a = (int)slider.value * 1000;
            a += betTotal;

            if (a <= 20000)
            {
                betTotal = a;
                chips1.GetComponent<Image>().sprite = chipsImages[(betTotal / 1000)];
            }
            else
            {
                betTotal = 20000;
                chips1.GetComponent<Image>().sprite = chipsImages[(betTotal / 1000)];
            }

            CmdRaise(name1.GetComponent<Text>().text, betTotal / 1000);
        }
    }

    [Command]
    void CmdRaise(string name, int number)
    {
        RpcRaise(name, number);
    }

    [ClientRpc]
    void RpcRaise(string name, int number)
    {
        defineVars();

        if (name2.GetComponent<Text>().text == name)
        {
            chips2.GetComponent<Image>().sprite = chipsImages[number];
        }
        else if (name3.GetComponent<Text>().text == name)
        {
            chips3.GetComponent<Image>().sprite = chipsImages[number];
        }
        else if (name4.GetComponent<Text>().text == name)
        {
            chips4.GetComponent<Image>().sprite = chipsImages[number];
        }
    }


    //[Command]
    //void CmdSpawnCard()
    //{
    //    GameObject game = GameObject.FindGameObjectWithTag("game");
    //    GameObject cd = Instantiate(card, game.transform);
    //    NetworkServer.Spawn(cd);
    //    RpcSpawnCard();
    //}

    //[ClientRpc]
    //void RpcSpawnCard()
    //{
    //    GameObject game = GameObject.FindGameObjectWithTag("game");
    //    GameObject cd = Instantiate(card, game.transform);
    //    NetworkServer.Spawn(cd);
    //}

    public override void OnStopClient()
    {
        if (!isLocalPlayer)
        {
            //get notified
        }
    }

    void defineVars()
    {
        name1 = GameObject.FindGameObjectWithTag("player1").transform.GetChild(1).gameObject;
        pic1 = GameObject.FindGameObjectWithTag("player1").transform.GetChild(0).gameObject;
        name2 = GameObject.FindGameObjectWithTag("player2").transform.GetChild(1).gameObject;
        pic2 = GameObject.FindGameObjectWithTag("player2").transform.GetChild(0).gameObject;
        name3 = GameObject.FindGameObjectWithTag("player3").transform.GetChild(1).gameObject;
        pic3 = GameObject.FindGameObjectWithTag("player3").transform.GetChild(0).gameObject;
        name4 = GameObject.FindGameObjectWithTag("player4").transform.GetChild(1).gameObject;
        pic4 = GameObject.FindGameObjectWithTag("player4").transform.GetChild(0).gameObject;

        container = GameObject.Find("local").transform.GetChild(1).gameObject;
        slider = container.transform.GetChild(0).gameObject.GetComponent<Slider>();
        chips1 = container.transform.GetChild(2).gameObject;
        betText = slider.transform.GetChild(3).gameObject.GetComponent<Text>();
        log = GameObject.FindGameObjectWithTag("log").GetComponent<InputField>();
        gameHelp = GameObject.FindGameObjectWithTag("gameHelper").GetComponent<gameHelper>();
        chips2 = GameObject.FindGameObjectWithTag("player2").transform.GetChild(2).gameObject;
        chips3 = GameObject.FindGameObjectWithTag("player3").transform.GetChild(2).gameObject;
        chips4 = GameObject.FindGameObjectWithTag("player4").transform.GetChild(2).gameObject;
        player2Name = GameObject.FindGameObjectWithTag("player2").transform.GetChild(3).gameObject;
        player3Name = GameObject.FindGameObjectWithTag("player3").transform.GetChild(3).gameObject;
        player4Name = GameObject.FindGameObjectWithTag("player4").transform.GetChild(3).gameObject;
    }

}
