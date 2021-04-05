using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameTimer : NetworkBehaviour
{
    [SyncVar] public float timer; //How long the game has been running. -1=waiting for players, -2=game is done
    [SyncVar] public bool masterTimer = false; //Is this the master timer?
    [SyncVar] public bool isRunning = false;
    //public ServerTimer timerObj;

    GameTimer serverTimer;

    void Start()
    {
        if (isServer)
        { // For the host to do: use the timer and control the time.
                serverTimer = this;
                masterTimer = true;
        }
        else if (isLocalPlayer)
        { //For all the boring old clients to do: get the host's timer.
            GameTimer[] timers = FindObjectsOfType<GameTimer>();
            for (int i = 0; i < timers.Length; i++)
            {
                if (timers[i].masterTimer)
                {
                    serverTimer = timers[i];
                }
            }
        }
    }
    void Update()
    {
        if (masterTimer && isRunning)
        { //Only the MASTER timer controls the time

                timer += Time.deltaTime;
        }

        if (isLocalPlayer)
        { //EVERYBODY updates their own time accordingly.
            if (serverTimer)
            {
                timer = serverTimer.timer;
            }
            else
            { //Maybe we don't have it yet?
                GameTimer[] timers = FindObjectsOfType<GameTimer>();
                for (int i = 0; i < timers.Length; i++)
                {
                    if (timers[i].masterTimer)
                    {
                        serverTimer = timers[i];
                    }
                }
            }
        }
        gameObject.GetComponent<UnityEngine.UI.Text>().text = timer.ToString();
    }

    public void StartTimer()
    {
        if (masterTimer)
        {
            isRunning = true;
            timer = 0;
        }
    }

    public void StopTimer()
    {
        if (masterTimer)
        {
            isRunning = false;
        }
    }
}
