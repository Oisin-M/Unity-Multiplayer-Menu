using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDisplay : MonoBehaviour
{
    public List<TMPro.TMP_Text> p;

    public List<string> playerlist;
    public string host;

    public GameObject MenuController;
    public TMPro.TMP_Text p1, title;

    private int i;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerlist.Clear();
        title.text = "";
        p1.text = "";
        if ((p1.fontStyle & TMPro.FontStyles.Bold) != 0)
        {
            p1.fontStyle ^= TMPro.FontStyles.Bold;
        }
        foreach (TMPro.TMP_Text ps in p)
        {
            ps.text = "";
            if ((ps.fontStyle & TMPro.FontStyles.Bold) != 0)
            {
                ps.fontStyle ^= TMPro.FontStyles.Bold;
            }
        }
        if (MenuController.GetComponent<MenuObjects>().MenuStatus == "RoomMenu")
        {
            title.text = PhotonNetwork.CurrentRoom.Name;

            foreach (var player in PhotonNetwork.PlayerList)
            {
                if (playerlist.Contains(player.NickName) == false)
                {
                    if (player.IsMasterClient)
                    {
                        //Debug.Log("loop");
                        host = player.NickName;
                    }
                    else
                    {
                        //Debug.Log("loop");
                        playerlist.Add(player.NickName);
                    }
                }
                // else { start.active=true}
            }
            i = 0;
            foreach (string player in playerlist)
            {
                p[i].text = player;
                if (player == PhotonNetwork.LocalPlayer.NickName)
                {
                    p[i].fontStyle = TMPro.FontStyles.Bold;
                }
                //Debug.Log(player);
                //Debug.Log(playerlist.Count);
                i++;
            }
            p1.text = host;
            if (host == PhotonNetwork.LocalPlayer.NickName)
            {
                p1.fontStyle = TMPro.FontStyles.Bold;
            }

            //for i in len players, change name
        }
    }
}
