using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Hand : NetworkBehaviour
{
    /*
    private List<GameObject> card_hand = new List<GameObject>();
    public GameObject PlayerArea;
    public GameObject Card1;
    private ScriptableObject Defense_1;
    */

    public PlayerManager PlayerManager;

    public List<GameObject> showable_list = new List<GameObject>();

    private void Update()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        foreach (var card in showable_list)
        {
            card.transform.SetParent(PlayerManager.GetComponent<PlayerManager>().PlayerArea.transform, false);
        }
    }
}
