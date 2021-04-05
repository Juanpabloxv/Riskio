using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ChangeTurnState : MonoBehaviour
{
    public PlayerManager PlayerManager;

    public void onClicAttack()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        if (PlayerManager.isGM)
        {
            PlayerManager.ChangeTurnState("attack");
        }
    }

    public void onClicDefense()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        if (PlayerManager.isGM)
        {
            PlayerManager.ChangeTurnState("defense");
        }
    }

    public void onClicInformation()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        if (PlayerManager.isGM)
        {
            PlayerManager.ChangeTurnState("information");
        }
    }



}
