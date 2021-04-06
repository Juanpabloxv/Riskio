using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public enum TurnState { START, ATTACK, INFORMATION, DEFENSE, SCORE }

public class PlayerManager : NetworkBehaviour
{

    [SyncVar] public int playerNumber;
    public GameObject PlayerArea;
    public GameObject MainCanvas;
    public GameObject BoardArea;
    public GameObject playedCardPrefab;
    public GameObject playedCard;
    public CardList hand;
    public CardList information_hand;
    public CardList dos_hand;
    public CardList eop_hand;
    public CardList id_hand;
    public CardList rep_hand;
    public CardList spoo_hand;
    public CardList tamp_hand;
    public GameObject Card;
    [SyncVar] public bool isAttacker;
    [SyncVar] public bool hasPlayed;
    public bool isGM;
    [SyncVar] public TurnState state;



    // Start is called before the first frame update

    public override void OnStartClient()
    {
        base.OnStartClient();
        PlayerArea = GameObject.Find("PlayerArea");
        MainCanvas = GameObject.Find("Main Canvas");
        BoardArea = GameObject.Find("BoardArea");
        state = TurnState.START;
        isAttacker = false;
        hasPlayed = false;
        GameObject selectAttacker = GameObject.Find("AttackerSelection");
        selectAttacker.GetComponent<Canvas>().enabled = false;
        GameObject selectAttackType = GameObject.Find("AttackTypeSelection");
        selectAttackType.GetComponent<Canvas>().enabled = false;
        CmdSetPlayerNumber();
        if (isServer)
        {
            isGM = true;
        }
        else
        {
            isGM = false;
        }

    }

    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();
        //var numPlayers = Network

    }



    [Command]
    public void CmdSetPlayerNumber()
    {
        var total = NetworkServer.connections.Count;
        print(total);
        RpcSetPlayerNumber(total);
    }


    [ClientRpc]
    public void RpcSetPlayerNumber(int total)
    {
        print("holi 1");
        if (playerNumber == 0)
        {
            playerNumber = total;
            GameObject playerNumberText = GameObject.Find("PlayerNumberText");
            playerNumberText.GetComponent<UnityEngine.UI.Text>().text = "Jugador " + total.ToString();
            //print("holi 2");
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
        var ids = NetworkServer.connections.Count;
        var str = "Enter the number of the player (Between 2 and " + ids.ToString() + ")";
        

        GameObject attackerIds = GameObject.Find("PlayerIdsText");
        attackerIds.GetComponent<UnityEngine.UI.Text>().text = str ;

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
        if (!isGM)
        {
            playerCard.GetComponent<CardDisplay>().card = hand.card_hand[position];
            playerCard.GetComponent<CardDisplay>().pasteSprite();
            playerCard.transform.SetParent(PlayerArea.transform, false);
            PlayerArea.GetComponent<Hand>().showable_list.Add(playerCard);
           // PlayerArea.GetComponent<Hand>().update_list();
        } else
        {
            print(information_hand.card_hand.Count);
            playerCard.GetComponent<CardDisplay>().card = information_hand.card_hand[position];
            playerCard.GetComponent<CardDisplay>().pasteSprite();
            playerCard.transform.SetParent(PlayerArea.transform, false);
            PlayerArea.GetComponent<Hand>().showable_list.Add(playerCard);
           // PlayerArea.GetComponent<Hand>().update_list();
        }

    }


    public void PlayCard(GameObject card)
    {
        if (!hasPlayed && !isAttacker && !isGM && state == TurnState.DEFENSE)
        {
            CmdPlayCard(card, "defense");
        } else if(!hasPlayed && !isAttacker && isGM && state == TurnState.INFORMATION){
            CmdPlayCard(card, "information");
        }
    }

    [Command]
    public void CmdPlayCard(GameObject card, string type)
    {
        if (type == "defense")
        {
            GameObject played_card = Instantiate(playedCardPrefab, new Vector2(0, 0), Quaternion.identity);
            NetworkServer.Spawn(played_card, connectionToClient);
            var i = 0;
            foreach (var hand_card in hand.card_hand)
            {
                print(hand_card.number);
                if (hand_card.number == card.GetComponent<CardDisplay>().card.number)
                {
                    RpcPlayCard(played_card, i, type);
                    break;
                }
                i++;
            }
        } else if(type == "information"){
            GameObject played_card = Instantiate(playedCardPrefab, new Vector2(0, 0), Quaternion.identity);
            NetworkServer.Spawn(played_card, connectionToClient);
            var i = 0;
            foreach (var hand_card in information_hand.card_hand)
            {
                print(hand_card.number);
                if (hand_card.number == card.GetComponent<CardDisplay>().card.number)
                {
                    RpcPlayCard(played_card, i, type);
                    break;
                }
                i++;
            }
        }
       
    }


    [ClientRpc]
    public void RpcPlayCard(GameObject played_card, int position, string type)
    {
        if (type == "defense")
        {
            //played_card.GetComponent<CardDisplay>().CopyValuesFrom(card.GetComponent<CardDisplay>());
            played_card.GetComponent<CardDisplay>().card = hand.card_hand[position];
            played_card.GetComponent<CardDisplay>().pasteSprite();
            played_card.transform.SetParent(BoardArea.transform, false);  //.SetParent(MainCanvas.transform, false);
            hasPlayed = true;
            RectTransform rect = played_card.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(89.88f, 153.67f);
        }
        else if (type == "information")
        {
            //played_card.GetComponent<CardDisplay>().CopyValuesFrom(card.GetComponent<CardDisplay>());
            played_card.GetComponent<CardDisplay>().card = information_hand.card_hand[position];
            played_card.GetComponent<CardDisplay>().pasteSprite();
            played_card.transform.SetParent(BoardArea.transform, false);  //.SetParent(MainCanvas.transform, false);
            hasPlayed = true;
            RectTransform rect = played_card.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(89.88f, 153.67f);
        }
    }


    public void ChangeTurnState(string new_state)
    {
        if(new_state == "attack" && (state == TurnState.START || state == TurnState.DEFENSE))
        {
            if (isServer)
            {
                GameObject selectAttacker = GameObject.Find("AttackerSelection");
                selectAttacker.GetComponent<Canvas>().enabled = true;
                getPlayers(selectAttacker);
            }
            CmdChangeTurnState(TurnState.ATTACK, new_state);

        } else if (new_state == "defense" && (state == TurnState.ATTACK || state == TurnState.INFORMATION))
        {
            CmdChangeTurnState(TurnState.DEFENSE, new_state);
        } else if (new_state == "information" && state == TurnState.ATTACK)
        {
            CmdChangeTurnState(TurnState.INFORMATION, new_state);
        }
    }

    [Command]
    public void CmdChangeTurnState(TurnState new_state, string str_state)
    {
        PlayerManager[] Players = FindObjectsOfType<PlayerManager>();
        foreach (var player in Players)
        {
            player.state = new_state;
            if (str_state == "attack")
            {
                player.hasPlayed = false;
                GameObject board = GameObject.Find("BoardArea");
                foreach (Transform child in board.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
        RpcChangeTurnState(str_state);
    }

    [ClientRpc]
    public void RpcChangeTurnState(string str_state)
    {
        GameObject state_text = GameObject.Find("StateText");
        state_text.GetComponent<UnityEngine.UI.Text>().text = str_state;
    }


        [ClientRpc]
    public void RpcSelectAttacker(int clientId)
    {
        print(clientId);
        print(playerNumber);
        GameObject okbutton = GameObject.Find("OkButton");
        okbutton.GetComponent<SelectAttacker>().setAttacker(clientId);
        //changeAttacker(clientId);
       
    }


    [Command]
    public void CmdSelectAttacker(int clientId)
    {
        RpcSelectAttacker(clientId);
    }

    [Command]
    public void CmdSelectAttack(int attack_number)
    {
        int position = Random.Range(0, 12);
        RpcSelectAttack(attack_number, position);
    }

    [ClientRpc]
    public void RpcSelectAttack(int attack_number, int position)
    {
        print(attack_number);
        print(playerNumber);
        GameObject okbutton = GameObject.Find("OkTypeButton");
        okbutton.GetComponent<SelectAttackType>().setAttackCard(attack_number, position);
        //changeAttacker(clientId);

    }


}
