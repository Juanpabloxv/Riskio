using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class CardZoom : MonoBehaviour
{

    public GameObject Canvas;
    public GameObject CardZoomed;
    private GameObject zoomCard;
    public PlayerManager PlayerManager;

    public void Awake()
    {
        Canvas = GameObject.Find("Main Canvas");
    }

    public void OnHoverEnter()
    {
        //NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        //PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        zoomCard = Instantiate(CardZoomed, new Vector2(-419.01f, 53), Quaternion.identity);
        zoomCard.GetComponent<CardDisplay>().CopyValuesFrom(gameObject.GetComponent<CardDisplay>());
        zoomCard.transform.SetParent(Canvas.transform, false);
        //zoomCard.layer = LayerMask.NameToLayer("Zoom");

 
        RectTransform rect = zoomCard.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(200, 300);
    }

    public void OnHoverExit()
    {
        Destroy(zoomCard);
    }

}
