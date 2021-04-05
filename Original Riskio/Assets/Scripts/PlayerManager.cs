using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public enum TurnState { START, ATTACK, INFORMATION, DEFENSE, SCORE }

public class PlayerManager : NetworkBehaviour
{


    public GameObject PlayerArea;
    public GameObject MainCanvas;
    public GameObject BoardArea;
    public GameObject playedCardPrefab;
    public GameObject playedCard;
    public CardList hand;
    public CardList information_hand;
    public GameObject Card;
    [SyncVar]  public bool isAttacker = false;
    public bool hasPlayed = false;
    public bool isGM = false;
    [SyncVar]  public TurnState state;
    public GameObject[] Players;



    // Start is called before the first frame update

    public override void OnStartClient()
    {
        base.OnStartClient();
        PlayerArea = GameObject.Find("PlayerArea");
        MainCanvas = GameObject.Find("Main Canvas");
        BoardArea = GameObject.Find("BoardArea");
        state = TurnState.START;
        GameObject selectAttacker = GameObject.Find("AttackerSelection");
        selectAttacker.GetComponent<Canvas>().enabled = false;
        if (isServer)
        {
            isGM = true;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        /*if (isServer)
        {
            getPlayers();
        }*/
    }

    [Server]
    public void  getPlayers(GameObject selectPlayerCanvas)
    {
        var ids = NetworkServer.connections.Values;
        var str = "Estos son los ids de los clientes: \n";
        foreach(var id in ids)
        {
            str += "- " + id.ToString() + "\n";
        }

        GameObject attackerIds = GameObject.Find("PlayerIdsText");
        attackerIds.GetComponent<UnityEngine.UI.Text>().text = str ;

    }


    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();

        //var numPlayers = Network
        
    }



    public void DrawCard()
    {
        if (hasAuthority)
        {
            CmdDrawCard();
        }
    }

    [Command]
    public void CmdDrawCard()
    {
        for (var i = 0; i < 13; i++)
        {
            GameObject playerCard = Instantiate(Card, new Vector3(0, 0, 0), Quaternion.identity);
            NetworkServer.Spawn(playerCard, connectionToClient);
            RpcDrawCard(playerCard, i);
            
        }
    }


    [ClientRpc]
    public void RpcDrawCard(GameObject playerCard, int position)
    {
        //if (!isGM)
        //{
            playerCard.GetComponent<CardDisplay>().card = hand.card_hand[position];
            playerCard.GetComponent<CardDisplay>().pasteSprite();
            playerCard.transform.SetParent(PlayerArea.transform, false);
            PlayerArea.GetComponent<Hand>().showable_list.Add(playerCard);
            PlayerArea.GetComponent<Hand>().update_list();
        /*} else
        {
            playerCard.GetComponent<CardDisplay>().card = information_hand.card_hand[position];
            playerCard.GetComponent<CardDisplay>().pasteSprite();
            playerCard.transform.SetParent(PlayerArea.transform, false);
            PlayerArea.GetComponent<Hand>().showable_list.Add(playerCard);
            PlayerArea.GetComponent<Hand>().update_list();
        }
        */

    }


    public void PlayCard(GameObject card)
    {
        if (!hasPlayed && !isAttacker && !isGM && state == TurnState.DEFENSE)
        {
            CmdPlayCard(card);
        }
    }

    [Command]
    public void CmdPlayCard(GameObject card)
    {
        GameObject played_card = Instantiate(playedCardPrefab, new Vector2(0, 0), Quaternion.identity);
        NetworkServer.Spawn(played_card, connectionToClient);
        var i = 0;
        foreach(var hand_card in hand.card_hand)
        {
            print(hand_card.number);
            if(hand_card.number == card.GetComponent<CardDisplay>().card.number)
            {
                RpcPlayCard(played_card, i);
                break;
            }
            i++;
        }
       
    }


    [ClientRpc]
    public void RpcPlayCard(GameObject played_card, int position)
    {
        //played_card.GetComponent<CardDisplay>().CopyValuesFrom(card.GetComponent<CardDisplay>());
        played_card.GetComponent<CardDisplay>().card = hand.card_hand[position];
        played_card.GetComponent<CardDisplay>().pasteSprite();
        played_card.transform.SetParent(BoardArea.transform, false);  //.SetParent(MainCanvas.transform, false);
        hasPlayed = true;
    }




    public void ChangeTurnState(string new_state)
    {
        if(new_state == "attack")
        {
            CmdChangeTurnState(TurnState.ATTACK, new_state);
            if (isServer)
            {
                GameObject selectAttacker = GameObject.Find("AttackerSelection");
                selectAttacker.GetComponent<Canvas>().enabled = true;
                getPlayers(selectAttacker);
            }

        } else if (new_state == "defense")
        {
            CmdChangeTurnState(TurnState.DEFENSE, new_state);
        } else
        {
            CmdChangeTurnState(TurnState.INFORMATION, new_state);
        }
    }

    [Command]
    public void CmdChangeTurnState(TurnState new_state, string str_state)
    {
        GameObject state_text = GameObject.Find("StateText");
        state_text.GetComponent<UnityEngine.UI.Text>().text = str_state;
        PlayerManager[] Players = FindObjectsOfType<PlayerManager>();
        foreach (var player in Players)
        {
            player.state = new_state;
        }
    }


    public void SelectAttacker(int clientId)
    {
        RpcSelectAttacker(clientId);
    }

    [ClientRpc]
    public void RpcSelectAttacker(int clientId)
    {
        GameObject Player = GameObject.Find("PlayerManager");
        PlayerManager Playermanager = Player.GetComponent<PlayerManager>();
        print(NetworkConnection.LocalConnectionId);
        print(clientId);

        if(NetworkConnection.LocalConnectionId == clientId)
        {
            print("hi");
            Player.GetComponent<PlayerManager>().isAttacker = true;
        } else
        {
            print("hi2");
            Player.GetComponent<PlayerManager>().isAttacker = false;
        }
    }

}
