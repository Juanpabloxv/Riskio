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

    private void Update()
    {
        foreach (var card in showable_list)
        {
            card.transform.SetParent(gameObject.transform, false);
        }
    }
}
