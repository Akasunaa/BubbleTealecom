using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


public class ClientManager : MonoBehaviour
{
    public List<GameObject> clients;
    private GameObject currentClient;
    public Transform clientSpawnPoint;
    public static ClientManager Instance { get; private set; }

    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    void Start()
    {
        SpawnNextClient();
    }

    private void SpawnNextClient()
    {
        if (clients.Count == 0)
        {
            return;
        }
        GameObject nextClient = clients[0];
        clients.RemoveAt(0);
        currentClient = Instantiate(nextClient);
        currentClient.transform.position = clientSpawnPoint.position;
    }

    public void ClientTimerEnded()
    {
        Destroy(currentClient);
        SpawnNextClient();
    }
}
