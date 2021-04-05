using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    // Start is called before the first frame update
    public void pasteSprite ()
    {
        gameObject.GetComponent<Image>().sprite  = card.artwork;
    }

    public void CopyValuesFrom(CardDisplay input)
    {
        card = input.card;
        gameObject.GetComponent<Image>().sprite = card.artwork;
    }


}
