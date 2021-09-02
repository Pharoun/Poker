using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class gameHelper : NetworkBehaviour
{

    public int pic;
    public string avatarName;

    void Awake()
    {
        //avatarName = PlayerPrefs.GetString("Name");
        avatarName = System.Guid.NewGuid().ToString().Substring(0, 5);
        pic = PlayerPrefs.GetInt("pic");
    }

    public struct PlayerVals
    {
        public int pic;
        public string name;
    }

    public SyncList<PlayerVals> ActivePlayers = new SyncList<PlayerVals>();

    public void clientActivePlayer()
    {

        PlayerVals plv = new PlayerVals
        {
            pic = pic,
            name = avatarName
        };

        CmdActivePlayer(plv);
    }



    [Command(requiresAuthority = false)]
    void CmdActivePlayer(PlayerVals plv)
    {
        ActivePlayers.Add(plv);
    }
}
