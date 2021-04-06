using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBoard : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject board_canvas;
    void Start()
    {
        board_canvas = GameObject.Find("Office");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        if (board_canvas.GetComponent<Canvas>().enabled)
        {
            board_canvas.GetComponent<Canvas>().enabled = false;
        } else
        {
            board_canvas.GetComponent<Canvas>().enabled = true;
        }
    }
}
