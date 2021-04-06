using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SelectAttackType : NetworkBehaviour
{
    public PlayerManager PlayerManager;
    public GameObject Card;
    public GameObject attack_canvas;
    // Start is called before the first frame update
    void Start()
    {
        attack_canvas = GameObject.Find("");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        if (PlayerManager.isGM)
        {
            GameObject inputField = GameObject.Find("AttackerTypeInput");
            var attack_number = inputField.GetComponent<UnityEngine.UI.InputField>().text;
            print(attack_number);
            PlayerManager.CmdSelectAttack(int.Parse(attack_number));

            gameObject.transform.parent.GetComponent<Canvas>().enabled = false;
        }
    }


    public void setAttackCard(int attack_number, int position)
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        Dictionary<int, CardList> attack_dict = new Dictionary<int, CardList>();
        attack_dict.Add(1, PlayerManager.hand);
        attack_dict.Add(2, PlayerManager.hand);
        attack_dict.Add(3, PlayerManager.hand);
        attack_dict.Add(4, PlayerManager.hand);
        attack_dict.Add(5, PlayerManager.hand);
        attack_dict.Add(6, PlayerManager.hand);

        var attack_hand = attack_dict[attack_number];

        foreach(var card in attack_hand.card_hand)
        {
            if (card.number == position)
            {
                GameObject played_card = Instantiate(Card, new Vector2(0, 0), Quaternion.identity);
                played_card.GetComponent<CardDisplay>().card = card;
                played_card.GetComponent<CardDisplay>().pasteSprite();
                played_card.transform.SetParent(attack_canvas.transform, false);
            }

        }

    }

}
