using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DrawCard : NetworkBehaviour
{


    public GameObject Card1;
    public GameObject Card2;
    public GameObject PlayerArea;
    public GameObject MainCanvas;
    public GameObject EnemyArea;
    public CardList hand;

    public List<GameObject> hand_cards = new List<GameObject>();

    public PlayerManager PlayerManager;
    // Start is called before the first frame update
    void Start()
    {
        MainCanvas = GameObject.Find("Main Canvas");

    }

    public void onClick()
    {

        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        PlayerArea = PlayerManager.GetComponent<PlayerManager>().PlayerArea;
        for (var i = 0; i < 6; i++)
        {
            GameObject playerCard = Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.GetComponent<CardDisplay>().card = hand.card_hand[i];
            playerCard.transform.SetParent(PlayerArea.transform, false);
            PlayerArea.GetComponent<Hand>().showable_list.Add(playerCard);
        }

    }

    
    public void Update()
    {
       
    }
    

}

