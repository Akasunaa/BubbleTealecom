using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


public class ClientManager : MonoBehaviour
{
    private List<GameObject> clients = new List<GameObject>();
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

    public void SetupClients(List<GameObject> clients)
    {
        this.clients = clients;
        SpawnNextClient();
    }

    private void SpawnNextClient()
    {
        if (clients.Count == 0)
        {
            return;
        }
        SoundManager.PlaySound(SoundManager.Sound.Doorbell, 0.5f);
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

    public void ClientHappy()
    {
        Debug.Log("Happy Client got his BUBBLE TEA !");
        Destroy(currentClient);
        SpawnNextClient();
    }
    
    public void ClientUnHappy()
    {
        Debug.Log("Un Happy Client didn't get his desired BUBBLE TEA !");
        Destroy(currentClient);
        SpawnNextClient();
    }
}
