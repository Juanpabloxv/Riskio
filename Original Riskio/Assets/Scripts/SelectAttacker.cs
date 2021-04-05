using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SelectAttacker : NetworkBehaviour
{
    public PlayerManager PlayerManager;
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
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        if (PlayerManager.isGM)
        {
            GameObject inputField = GameObject.Find("AttackerIdInput");
            var player_id = inputField.GetComponent<UnityEngine.UI.InputField>().text;
            print(player_id);
            PlayerManager.SelectAttacker(int.Parse(player_id));

            gameObject.transform.parent.GetComponent<Canvas>().enabled = false;
        }
    }
}
