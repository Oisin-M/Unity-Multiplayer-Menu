using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject MenuController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if(PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            MenuController.GetComponent<MenuObjects>().BeginGame();
        }
        else
        {
            MenuController.GetComponent<MenuObjects>().CannotStart();
        }
    }

    //IEnumerator LoadGame()
    //{

    //}
}
