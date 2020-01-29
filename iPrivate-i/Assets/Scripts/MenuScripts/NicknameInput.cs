using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicknameInput : MonoBehaviour
{
    private bool allowEnter;
    public GameObject lobby;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //(allowEnter && (line1.text.Length > lenprompt) && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)))
        if (allowEnter && (this.gameObject.GetComponent<TMPro.TMP_InputField>().text.Length > 0) && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)))
        {
            OnSubmit();
            allowEnter = false;
        }
        else
            allowEnter = this.gameObject.GetComponent<TMPro.TMP_InputField>().isFocused;
    }

    private void OnSubmit()
    {
        lobby.GetComponent<LobbyScript>().SetNickname(this.gameObject.GetComponent<TMPro.TMP_InputField>().text);
    }
}
