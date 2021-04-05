using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using Mirror;

public class PlayingCard : NetworkBehaviour
{

    //public GameObject card;
    public GameObject cardplayed;
    public GameObject PlayerArea;
    public PlayerManager PlayerManager;
    public GameObject BoardArea;
    public GameObject playedCardPrefab;


    // Start is called before the first frame update

    private void Start()
    {

        PlayerArea = GameObject.Find("PlayerArea");
        BoardArea = GameObject.Find("BoardArea");

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        
        foreach (var card in PlayerArea.GetComponent<Hand>().showable_list)
        {
            if (card.GetComponent<PlayCard>().selected_card)
            {

                NetworkIdentity networkIdentity = NetworkClient.connection.identity;
                PlayerManager = networkIdentity.GetComponent<PlayerManager>();
                PlayerManager.PlayCard(card);
                break;

            }
        }

    }

    public void playTheCard(GameObject card)
    {
        cardplayed = card;
    }
}
