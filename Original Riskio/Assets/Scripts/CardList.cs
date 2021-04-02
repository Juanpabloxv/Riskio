using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hand", menuName = "Hand")]
public class CardList : ScriptableObject
{

    public List<Card> card_hand = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
