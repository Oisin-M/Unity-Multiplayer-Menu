using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LobbyScript : MonoBehaviourPunCallbacks
{
    public MenuObjects MenuController;

    public bool connectedtomasterbool;
    public bool joinedroombool;


    private List<string> defaultnicknames= new List<string> { "Player1", "Player2", "Player3", "Player4", "Player5",
    "Player6", "Player7", "Player8", "Player9", "Player10"};
    private bool nickname_valid;
    private string nickname;
   // public TMPro.TMP_Text title;
   // public List<TMPro.TMP_Text> p = new List<TMPro.TMP_Text>();
   // public List<string> playerlist = new List<string>();
   // private int i;

    public static LobbyScript lobby;

    public void Awake()
    {
        if (GameObject.FindGameObjectWithTag("OldLobby") == null)
        {
            Debug.Log("No Lobby");
            lobby = this;
        }
        else
        {
            Destroy(GameObject.FindGameObjectWithTag("OldLobby"));
            lobby = this;
        }

        this.gameObject.tag = "OldLobby";
    }

    public void Start()
    {
        Debug.Log("Menu Scene");
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }

        DontDestroyOnLoad(lobby);
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("CONNECTED TO PHOTON MASTER SERVER");
        connectedtomasterbool = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("DISCONNECTED");
        connectedtomasterbool = false;
        joinedroombool = false;
        if (MenuController.MenuStatus=="RoomMenu")
        {
            MenuController.GoBack();
        }
// MIGHT WANT SOMETHING HERE FOR WHEN THEY DISCONNECT IN START PHASE
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log(message);
            Debug.Log("Room DNE");
            MenuController.FailedJoinRoom();
        }
        else
        {
            Debug.Log("We're not connected");
            MenuController.FailedRoomConnect();
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log(message);
            Debug.Log("Room Already Exists");
            MenuController.FailedCreateRoom();
        }
        else
        {
            Debug.Log("We're not connected");
            MenuController.FailedRoomConnect();
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("created room "+PhotonNetwork.CurrentRoom);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("joined room " + PhotonNetwork.CurrentRoom);
        joinedroombool = true;
        MenuController.InRoom();
        GiveDefaultNickname(0);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("left room " + PhotonNetwork.CurrentRoom);
        joinedroombool = false;
    }

    public void CreateRoom(string roomname)
    {
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10, PublishUserId = true, CleanupCacheOnLeave = false };
        PhotonNetwork.CreateRoom(roomname, roomOps, TypedLobby.Default);
    }

    public void JoinRoom(string roomname)
    {
        PhotonNetwork.JoinRoom(roomname);
    }

    public void SetNickname(string nickname)
    {
        nickname_valid = true;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player == PhotonNetwork.LocalPlayer)
            {

            }
            else
            {
                if (player.NickName != null && player.NickName == nickname)
                {
                    nickname_valid = false;
                }
            }
        }
        if (nickname_valid)
        {
            PhotonNetwork.LocalPlayer.NickName = nickname;
            MenuController.GetComponent<MenuObjects>().SetNicknameDone();
        }
        else
        {
            MenuController.GetComponent<MenuObjects>().SetNicknameFailed();
        }
    }

    public void GiveDefaultNickname(int i)
    {
        nickname = defaultnicknames[i];
        nickname_valid = true;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player == PhotonNetwork.LocalPlayer)
            {

            }
            else
            {
                if (player.NickName != null && player.NickName == nickname)
                {
                    nickname_valid = false;
                }
            }
        }
        if (nickname_valid)
        {
            PhotonNetwork.LocalPlayer.NickName = nickname;
        }
        else
        {
            i++;
            GiveDefaultNickname(i);
        }
    }
}


    //private void Update()
    //{
    //    playerlist.Clear();
    //    foreach (TMPro.TMP_Text ps in p)
    //    {
    //        ps.text = "";
    //    }
    //    if (joinedroombool && start != null)
    //    {
    //        title.text = lobbynameinput.text;

    //        foreach (var player in PhotonNetwork.PlayerList)
    //        {
    //            if (playerlist.Contains(player.UserId.ToString()) == false)
    //            {
    //                if (player.IsMasterClient)
    //                {
    //                    //Debug.Log("loop");
    //                    playerlist.Add(player.UserId.ToString() + "-HOST");
    //                }
    //                else
    //                {
    //                    //Debug.Log("loop");
    //                    playerlist.Add(player.UserId.ToString());
    //                }
    //            }
    //            // else { start.active=true}
    //        }
    //        i = 0;
    //        foreach (string player in playerlist)
    //        {
    //            p[i].text = player;
    //            //Debug.Log(player);
    //            //Debug.Log(playerlist.Count);
    //            i++;
    //        }
    //        //for i in len players, change name
    //    }
    //    else
    //    {
    //        if (start != null)
    //        {
    //            start.active = true;
    //        }
    //    }
    //    if (joinedroombool && start != null)
    //    {
    //        if (PhotonNetwork.IsMasterClient)
    //        {
    //            start.active = true;
    //        }
    //        else
    //        {
    //            start.active = false;
    //        }
    //    }
    //}
