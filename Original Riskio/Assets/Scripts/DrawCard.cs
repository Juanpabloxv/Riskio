using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCard : MonoBehaviour
{

    public GameObject Card1;
    public GameObject Card2;
    public GameObject PlayerArea;
    public GameObject EnemyArea;
    public CardList hand;

    public List<GameObject> hand_cards = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
    }

    public void onClick()
    {

        for (var i=0; i<6; i++)
        {
            GameObject playerCard = Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.GetComponent<CardDisplay>().card = hand.card_hand[i];
            PlayerArea.GetComponent<Hand>().showable_list.Add(playerCard);
        }


    }

    
    public void Update()
    {
       
    }
    

}

