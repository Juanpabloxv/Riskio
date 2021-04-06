using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class changeText : NetworkBehaviour
{
    // Start is called before the first frame update
    public PlayerManager PlayerManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void changePlayerNumber()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        gameObject.GetComponent<UnityEngine.UI.Text>().text = "Jugador " + PlayerManager.playerNumber.ToString();
    }
}
