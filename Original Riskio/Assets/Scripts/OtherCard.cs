using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCard : MonoBehaviour
{

    public GameObject PlayerArea;
    public CardList hand;
    public GameObject Card1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void onClickNext()
    {
        var card_list = PlayerArea.GetComponent<Hand>().showable_list;
        var last_number = card_list.Count - 1;
        var last_card = card_list[last_number];
        last_number = last_card.gameObject.GetComponent<CardDisplay>().card.number;
        if ( last_number != 12)
        {
            foreach (var card in hand.card_hand)
            {
                if (card.number == last_number + 1 )
                {
                    GameObject playerCard = Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity);
                    playerCard.GetComponent<CardDisplay>().card = card;
                    PlayerArea.GetComponent<Hand>().showable_list.Add(playerCard);
                    Destroy(PlayerArea.GetComponent<Hand>().showable_list[0]);
                    PlayerArea.GetComponent<Hand>().showable_list.RemoveAt(0);
                    break;
                }
            }
        } else 
        {
            Card selected_card = null;
            foreach (var card in hand.card_hand)
            {
                if(card.number == 0)
                {
                    selected_card = card;
                }
            }
            if (selected_card) {
                GameObject playerCard = Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity);
                playerCard.GetComponent<CardDisplay>().card = selected_card;
                PlayerArea.GetComponent<Hand>().showable_list.Add(playerCard);
                Destroy(PlayerArea.GetComponent<Hand>().showable_list[0]);
                PlayerArea.GetComponent<Hand>().showable_list.RemoveAt(0);
            }
        }

    }

    public void onClickPrevious()
    {
        var card_list = PlayerArea.GetComponent<Hand>().showable_list;
        var first_card = card_list[0];
        var first_number = first_card.gameObject.GetComponent<CardDisplay>().card.number;
        if (first_number != 0)
        {
            foreach (var card in hand.card_hand)
            {
                if (card.number == first_number - 1)
                {
                    GameObject playerCard = Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity);
                    playerCard.GetComponent<CardDisplay>().card = card;
                    Destroy(PlayerArea.GetComponent<Hand>().showable_list[5]);
                    PlayerArea.GetComponent<Hand>().showable_list.RemoveAt(5);
                    PlayerArea.GetComponent<Hand>().showable_list.Insert(0, playerCard);
                    playerCard.transform.SetParent(PlayerArea.transform, false);
                    playerCard.transform.SetSiblingIndex(0);
                    PlayerArea.GetComponent<Hand>().showable_list[0].transform.SetSiblingIndex(0);
                    break;

                }
            }
        }
        else
        {
            Card selected_card = null;
            foreach (var card in hand.card_hand)
            {
                if (card.number == 12)
                {
                    selected_card = card;
                }
            }
            if (selected_card)
            {
                GameObject playerCard = Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity);
                playerCard.GetComponent<CardDisplay>().card = selected_card;
                Destroy(PlayerArea.GetComponent<Hand>().showable_list[5]);
                PlayerArea.GetComponent<Hand>().showable_list.RemoveAt(5);
                playerCard.transform.SetParent(PlayerArea.transform, false);
                playerCard.transform.SetSiblingIndex(0);
                PlayerArea.GetComponent<Hand>().showable_list.Insert(0, playerCard);
            }
        }

        /*
        var aux_list = PlayerArea.GetComponent<Hand>().showable_list;
        foreach (Transform child in PlayerArea.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        PlayerArea.GetComponent<Hand>().showable_list = aux_list;
    */
        }



}
