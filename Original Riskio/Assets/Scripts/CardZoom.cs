using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardZoom : MonoBehaviour
{

    public GameObject Canvas;
    private GameObject zoomCard;

    public void Awake()
    {
        Canvas = GameObject.Find("Main Canvas");
    }

    public void OnHoverEnter()
    {
        zoomCard = Instantiate(gameObject, new Vector2(-8.22f, -1.6f), Quaternion.identity);
        zoomCard.transform.SetParent(Canvas.transform, false);
        zoomCard.layer = LayerMask.NameToLayer("Zoom");

        print(zoomCard.layer);

        UnityEngine.EventSystems.EventTrigger zoom_script = zoomCard.GetComponent<UnityEngine.EventSystems.EventTrigger>();
        Destroy(zoom_script);

        BoxCollider2D colider = zoomCard.GetComponent<BoxCollider2D>();
        Destroy(colider);

        Rigidbody2D rigidbody = zoomCard.GetComponent<Rigidbody2D>();
        Destroy(rigidbody);


        RectTransform rect = zoomCard.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(3.5667f, 5.9096f);

    }

    public void OnHoverExit()
    {
        Destroy(zoomCard);
    }

}
