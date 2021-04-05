using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Timer : NetworkBehaviour
{
    public PlayerManager PlayerManager;

    public void onClicStart()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        if (PlayerManager.isGM)
        {
            GameTimer[] timers = FindObjectsOfType<GameTimer>();
            for (int i = 0; i < timers.Length; i++)
            {
                if (timers[i].masterTimer)
                {
                    timers[i].StartTimer();
                    break;
                }
            }
        }
    }

    public void onClicStop()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        if (PlayerManager.isGM)
        {
            GameTimer[] timers = FindObjectsOfType<GameTimer>();
            for (int i = 0; i < timers.Length; i++)
            {
                if (timers[i].masterTimer)
                {
                    timers[i].StopTimer();
                    break;
                }

            }
        }

    }
}
