using LevelData;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientManager : MonoBehaviour
{
    private List<ClientData> clients;
    private GameObject currentClient;
    public Transform clientSpawnPoint;
    public static ClientManager Instance { get; private set; }

    [SerializeField] private GameObject _clientPrefab;

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

    public void SetupClients(List<ClientData> clients)
    {
        this.clients = clients;
        SpawnNextClient();
    }

    private void SpawnNextClient()
    {
        if (clients.Count == 0)
        {
            LevelDataHolder.CurrentDay = LevelDataHolder.CurrentDay + 1;
            GameObject.FindGameObjectsWithTag("Sound").ToList().ForEach(x => Destroy(x));
            SoundAssets.instance.PlayMenuMusic();
            SceneManager.LoadScene("MainMenu");
            return;
        }
        SoundManager.PlaySound(SoundManager.Sound.Doorbell, 0.5f);
        ClientData nextClient = clients[0];
        clients.RemoveAt(0);
        currentClient = Instantiate(_clientPrefab);
        currentClient.GetComponent<Client>().LoadData(nextClient);
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
