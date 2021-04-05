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
    public PlayerManager PlayerManager;
    public CardList hand;

    //public List<GameObject> hand_cards = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        MainCanvas = GameObject.Find("Main Canvas");

    }

    public void onClick()
    {

        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        PlayerManager.DrawCard();
        Destroy(gameObject);
    }

    
    public void Update()
    {
       
    }
    

}

