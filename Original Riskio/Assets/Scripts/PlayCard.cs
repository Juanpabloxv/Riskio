using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCard : MonoBehaviour
{

    public bool selected_card = false;
    public GameObject PlayerArea;

    private void Start()
    {
        PlayerArea = GameObject.Find("PlayerArea");
    }


    public void onClick()
    {
        selected_card = true;
        gameObject.GetComponent<Image>().color = Color.red;
        foreach (var card in PlayerArea.GetComponent<Hand>().showable_list)
        {
            if (card != gameObject)
            {
                card.GetComponent<PlayCard>().selected_card = false;
                card.GetComponent<Image>().color = Color.white;
            }
        }
    }
}
