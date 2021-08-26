using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class helper : NetworkBehaviour
{
    public int player1Pic;
    public int player2Pic;
    public int player3Pic;
    public int player4Pic;
    int tempPic;

    public string player1Name;
    public string player2Name;
    public string player3Name;
    public string player4Name;

    public InputField usernameField;

    public GameObject[] Ellipse;
    GameObject[] players;

    public Canvas canvas;

    public GameObject playerPrefab;

    public void submitButton()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(players.Length);

        if (players.Length == 1)
        {
            player1Name = usernameField.text;
            player1Pic = tempPic;
        }

        if (players.Length == 2)
        {
            player2Name = usernameField.text;
            player2Pic = tempPic;
        }

        if (players.Length == 3)
        {
            player3Name = usernameField.text;
            player3Pic = tempPic;
        }

        if (players.Length == 4)
        {
            player4Name = usernameField.text;
            player4Pic = tempPic;
        }

        canvas.GetComponent<Animator>().Play("swipeIn");

        NetworkClient.localPlayer.gameObject.GetComponent<player>().beginning();

    }

    public void hideAll(int avatar)
    {
        foreach(GameObject child in Ellipse)
        {
            child.SetActive(false);
            tempPic = avatar;
        }
        Ellipse[avatar].SetActive(true);
    }

}
