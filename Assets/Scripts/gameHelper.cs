using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class gameHelper : NetworkBehaviour
{

    public int avatarPic;
    public string avatarName;
    public readonly SyncList<PlayerVals> ActivePlayers = new SyncList<PlayerVals>();
    public event SyncList<PlayerVals>.SyncListChanged Callback;

    void Awake()
    {
        avatarName = System.Guid.NewGuid().ToString().Substring(0, 5);
        avatarPic = PlayerPrefs.GetInt("pic");
    }

    void Start()
    {
        ActivePlayers.Callback += OnActivePlayersUpdated;
    }

    void OnActivePlayersUpdated(SyncList<PlayerVals>.Operation op, int itemIndex, PlayerVals oldItem, PlayerVals newItem)
    {

        GameObject[] go = GameObject.FindGameObjectsWithTag("Player");

        switch (op)
        {
            case SyncList<PlayerVals>.Operation.OP_ADD:

                foreach (GameObject Gobject in go)
                {
                    Gobject.GetComponent<player>().listAdded(newItem);
                }

                break;
            case SyncList<PlayerVals>.Operation.OP_REMOVEAT:

                foreach (GameObject Gobject in go)
                {
                    Gobject.GetComponent<player>().listremove(oldItem);
                }

                break;
        }
    }

    public struct PlayerVals
    {
        public int pic;
        public string name;
    }


    public void clientActivePlayer()
    {

        PlayerVals plv = new PlayerVals
        {
            pic = avatarPic,
            name = avatarName
        };

        CmdActivePlayer(plv);
    }

    [Command(requiresAuthority = false)]
    void CmdActivePlayer(PlayerVals plv)
    {
        ActivePlayers.Add(plv);
    }



    public void discActivePlayer()
    {

        PlayerVals plv = new PlayerVals
        {
            pic = avatarPic,
            name = avatarName
        };

        CmdDiscActivePlayer(plv);
    }

    [Command(requiresAuthority = false)]
    void CmdDiscActivePlayer(PlayerVals plv)
    {
        ActivePlayers.Remove(plv);
    }
}
