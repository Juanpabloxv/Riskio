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
        attack_canvas = GameObject.Find("AttackCanvas");
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
        attack_dict.Add(1, PlayerManager.dos_hand);
        attack_dict.Add(2, PlayerManager.eop_hand);
        attack_dict.Add(3, PlayerManager.id_hand);
        attack_dict.Add(4, PlayerManager.rep_hand);
        attack_dict.Add(5, PlayerManager.spoo_hand);
        attack_dict.Add(6, PlayerManager.tamp_hand);
        print(attack_dict);

        var attack_hand = attack_dict[attack_number];
        print(attack_hand);

        foreach(var card in attack_hand.card_hand)
        {
            print(card);
            if (card.number == position)
            {
                print("oelo");
                GameObject played_card = Instantiate(Card, new Vector2(0, 0), Quaternion.identity);
                RectTransform rect = played_card.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(3.29f, 4.7504f);
                played_card.GetComponent<CardDisplay>().card = card;
                played_card.GetComponent<CardDisplay>().pasteSprite();
                played_card.transform.SetParent(attack_canvas.transform, false);
                break;
            }

        }

    }

}
