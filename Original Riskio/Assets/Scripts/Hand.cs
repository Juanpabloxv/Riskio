using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Hand : MonoBehaviour
{

    public List<GameObject> showable_list = new List<GameObject>();

    public void update_list()
    {
        //NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        //PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        foreach (var card in showable_list)
        {
            card.transform.SetParent(gameObject.transform, false);
        }
    }
}
