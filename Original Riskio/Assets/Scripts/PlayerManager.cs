using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{


    public GameObject Card1;
    public GameObject Card2;
    public GameObject PlayerArea;
    public GameObject MainCanvas;
    public GameObject EnemyArea;
    public CardList hand;

    public List<GameObject> hand_cards = new List<GameObject>();


    // Start is called before the first frame update
    
    public override void OnStartClient()
    {
        base.OnStartClient();
        PlayerArea = GameObject.Find("PlayerArea");
        MainCanvas = GameObject.Find("Main Canvas");
        /*
        for (var i = 0; i < 6; i++)
        {
            GameObject playerCard = Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.GetComponent<CardDisplay>().card = hand.card_hand[i];
            PlayerArea.GetComponent<Hand>().showable_list.Add(playerCard);
        }
        */

    }
    

    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();
    }


    [Command]
    public void CmdDeaLCards()
    {
        for (var i = 0; i < 6; i++)
        {
            GameObject playerCard = Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity);
            NetworkServer.Spawn(playerCard, connectionToClient);
            print(hand.card_hand[i]);
            playerCard.GetComponent<CardDisplay>().card = hand.card_hand[i];
            PlayerArea.GetComponent<Hand>().showable_list.Add(playerCard);
        }
    }


    [ClientRpc]
    public void RpcShowCard(GameObject card, string type)
    {
        if (type == "Hand")
        {
            if (hasAuthority)
            {
                card.transform.SetParent(PlayerArea.transform, false);
            }
        }

    }



    // Update is called once per frame
    void Update()
    {
        
    }



}
