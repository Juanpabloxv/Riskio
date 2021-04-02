using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    /*
    private List<GameObject> card_hand = new List<GameObject>();
    public GameObject PlayerArea;
    public GameObject Card1;
    private ScriptableObject Defense_1;
    */

    public List<GameObject> showable_list = new List<GameObject>();


    // Start is called before the first frame update
    /*
    void Start()
    {

        var defense_deck = ScriptableObject.FindObjectsOfType<Card>();
        foreach(var defense in defense_deck)
        {
            GameObject playerCard = Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity);
            card_hand.Add(playerCard);
        }



    }
    */
    private void Update()
    {
        foreach (var card in showable_list)
        {
            card.transform.SetParent(gameObject.transform, false);
        }
    }
}
