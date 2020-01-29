using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuObjects : MonoBehaviour
{
    //public Button createroom, joinroom, quitgame, startgame;
    //public TMPro.TMP_InputField roomname;
    public Animator microphone_animator, mainmenu_animator, roommenu_animator;
    public GameObject roommenu, fadecanvas;
    public TMPro.TMP_InputField roomname, playernickname;
    public GameObject MainMenuErrorPopUp, RoomMenuErrorPopUp;
    public TMPro.TMP_Text MainMenuErrorText, RoomMenuErrorText;
    public string MenuStatus;
    public GameObject lobby, playernamepanel;
    // Start is called before the first frame update
    void Start()
    {
        MenuStatus = "MainMenu";
        Debug.Log(MenuStatus);
        lobby = GameObject.FindGameObjectWithTag("OldLobby");
        roomname.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickedCreateButton()
    {
        Debug.Log("Clicked Create");
        MainMenuErrorPopUp.SetActive(true);
        MainMenuErrorPopUp.GetComponent<Image>().color= new Color32(0, 255, 241, 100);
        MainMenuErrorText.color = new Color32(0, 255, 241, 255);
        MainMenuErrorText.text = "Connecting to Room";

        mainmenu_animator.SetBool("Create/JoinRoomClicked", true);
        StartCoroutine(OnClickedCreateButtonReset());
    }

    IEnumerator OnClickedCreateButtonReset()
    {
        yield return 0;
        mainmenu_animator.SetBool("Create/JoinRoomClicked", false);

        lobby.GetComponent<LobbyScript>().CreateRoom(roomname.text);
    }

    public void OnClickedJoinButton()
    {
        Debug.Log("Clicked Join");
        MainMenuErrorPopUp.SetActive(true);
        MainMenuErrorPopUp.GetComponent<Image>().color = new Color32(0, 255, 241, 100);
        MainMenuErrorText.color = new Color32(0, 255, 241, 255);
        MainMenuErrorText.text = "Connecting to Room";

        mainmenu_animator.SetBool("Create/JoinRoomClicked", true);
        StartCoroutine(OnClickedJoinButtonReset());
    }

    IEnumerator OnClickedJoinButtonReset()
    {
        yield return 0;
        mainmenu_animator.SetBool("Create/JoinRoomClicked", false);

        lobby.GetComponent<LobbyScript>().JoinRoom(roomname.text);
    }

    public void FailedJoinRoom()
    {
        Debug.Log("Join Fail");
        MainMenuErrorPopUp.SetActive(true);
        MainMenuErrorPopUp.GetComponent<Image>().color = new Color32(255, 0, 0, 166);
        MainMenuErrorText.color = new Color32(255, 0, 0, 255);
        MainMenuErrorText.text = "Room Does Not Exist";
        mainmenu_animator.SetBool("JoinRoomFailed", true);
        StartCoroutine(FailedJoinRoomReset());
    }

    IEnumerator FailedJoinRoomReset()
    {
        yield return 0;
        mainmenu_animator.SetBool("JoinRoomFailed", false);
    }

    public void FailedCreateRoom()
    {
        Debug.Log("Create Fail");
        MainMenuErrorPopUp.SetActive(true);
        MainMenuErrorPopUp.GetComponent<Image>().color = new Color32(255, 0, 0, 166);
        MainMenuErrorText.color = new Color32(255, 0, 0, 255);
        MainMenuErrorText.text = "Room Already Exists";
        mainmenu_animator.SetBool("CreateRoomFailed", true);
        StartCoroutine(FailedCreateRoomReset());
    }

    IEnumerator FailedCreateRoomReset()
    {
        yield return 0;
        mainmenu_animator.SetBool("CreateRoomFailed", false);
    }

    public void FailedRoomConnect()
    {
        Debug.Log("Connect Fail");
        MainMenuErrorPopUp.SetActive(true);
        MainMenuErrorPopUp.GetComponent<Image>().color = new Color32(255, 0, 0, 166);
        MainMenuErrorText.color = new Color32(255, 0, 0, 255);
        MainMenuErrorText.text = "You Are Not Connected";
        mainmenu_animator.SetBool("NotConnected", true);
        StartCoroutine(FailedRoomConnectReset());
    }

    IEnumerator FailedRoomConnectReset()
    {
        yield return 0;
        mainmenu_animator.SetBool("NotConnected", false);
    }

    public void GoBack()
    {
        Debug.Log("Back");
        PhotonNetwork.LeaveRoom();

        roomname.text = "";
        roomname.ActivateInputField();

        microphone_animator.SetBool("BackButtonClicked", true);
        mainmenu_animator.SetBool("BackButtonClicked", true);
        roommenu_animator.SetBool("BackButtonClicked", true);
        StartCoroutine(GoBackReset());
    }

    IEnumerator GoBackReset()
    {
        yield return 0;
        microphone_animator.SetBool("BackButtonClicked", false);
        mainmenu_animator.SetBool("BackButtonClicked", false);
        roommenu_animator.SetBool("BackButtonClicked", false);

        MenuStatus = "MainMenu";
        Debug.Log(MenuStatus);
    }

    //Need PUN RPC so all players start game...
    public void BeginGame()
    {
        this.GetComponent<PhotonView>().RPC("StartGame", RpcTarget.All);
    }

    [PunRPC]
    public void StartGame()
    {
        Debug.Log("Start");
        microphone_animator.SetBool("GameStartClicked", true);
        mainmenu_animator.SetBool("GameStartClicked", true);
        roommenu_animator.SetBool("GameStartClicked", true);
        StartCoroutine(StartGameReset());
    }

    IEnumerator StartGameReset()
    {
        yield return 0;
        microphone_animator.SetBool("GameStartClicked", false);
        mainmenu_animator.SetBool("GameStartClicked", false);
        roommenu_animator.SetBool("GameStartClicked", false);
        fadecanvas.SetActive(true);

        MenuStatus = "GameStart";
        Debug.Log(MenuStatus);
    }

    public void InRoom()
    {
        Debug.Log("In Room");
        roommenu.SetActive(true);
        //playernamepanel.SetActive(true);
        playernickname.text = "";
        playernickname.ActivateInputField();
        microphone_animator.SetBool("InRoom", true);
        mainmenu_animator.SetBool("InRoom", true);
        roommenu_animator.SetBool("InRoom", true);
        StartCoroutine(InRoomReset());
    }

    IEnumerator InRoomReset()
    {
        yield return 0;
        microphone_animator.SetBool("InRoom", false);
        mainmenu_animator.SetBool("InRoom", false);
        roommenu_animator.SetBool("InRoom", false);

        MenuStatus = "RoomMenu";
        Debug.Log(MenuStatus);
    }

    public void SetNicknameDone()
    {
        Debug.Log("Set Nickname");
        roommenu_animator.SetBool("NicknameSet", true);
        StartCoroutine(SetNicknameDoneReset());
    }

    IEnumerator SetNicknameDoneReset()
    {
        yield return 0;
        roommenu_animator.SetBool("NicknameSet", false);
    }

    public void SetNicknameFailed()
    {
        Debug.Log("Nickname Taken");

        RoomMenuErrorText.text = "Name Already Taken";

        roommenu_animator.SetBool("InvalidNickname", true);
        StartCoroutine(SetNicknameFailedReset());
    }

    IEnumerator SetNicknameFailedReset()
    {
        yield return 0;
        roommenu_animator.SetBool("InvalidNickname", false);
        playernickname.ActivateInputField();
    }

    public void CannotStart()
    {
        Debug.Log("Cannot Start");
//        playernamepanel.SetActive(false);

        RoomMenuErrorText.text = "Only Host Can Start";

        roommenu_animator.SetBool("CannotStart", true);
        StartCoroutine(CannotStartReset());
    }

    IEnumerator CannotStartReset()
    {
        yield return 0;
        roommenu_animator.SetBool("CannotStart", false);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}