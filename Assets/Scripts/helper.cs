using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.SceneManagement;

public class helper : NetworkBehaviour
{

    public int tempPic;
    public string avatarName;

    public InputField usernameField;

    public GameObject[] Ellipse;

    public void submitButton()
    {

        PlayerPrefs.SetString("Name", usernameField.text);
        avatarName = usernameField.text;
        SceneManager.LoadScene("Game");
    }

    public void hideAll(int avatar)
    {
        foreach(GameObject child in Ellipse)
        {
            child.SetActive(false);
            tempPic = avatar;
            PlayerPrefs.SetInt("pic", tempPic);
        }
        Ellipse[avatar].SetActive(true);
    }

}
