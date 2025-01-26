using LevelData;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ClientManager : MonoBehaviour
{
    private const float X_DIFF = 10.0f;

    private List<ClientData> clients;
    private GameObject currentClient;
    public Transform clientSpawnPoint;
    public static ClientManager Instance { get; private set; }

    [SerializeField] private GameObject _clientGenericPrefab;
    [SerializeField] private GameObject _clientSlimePrefab;

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
            // GameObject.FindGameObjectsWithTag("Sound").ToList().ForEach(x => Destroy(x));
            SoundAssets.instance.PlayMenuMusic();
            SceneManager.LoadScene("MainMenu");
            return;
        }

        SoundManager.PlaySound(SoundManager.Sound.Doorbell, 0.5f);
        ClientData nextClient = clients[0];
        clients.RemoveAt(0);
        if (nextClient._type == ClientType.HumanOrInfected)
        {
            currentClient = Instantiate(_clientGenericPrefab);
            currentClient.GetComponent<Client>().LoadData(nextClient);
        }
        else
        {
            currentClient = Instantiate(_clientSlimePrefab);
            currentClient.GetComponent<ClientSlime>()._ingredientInStomac = nextClient.ingredientInStomac;
            currentClient.GetComponent<ClientSlime>().LoadData(nextClient);
        }

        currentClient.transform.position = clientSpawnPoint.position + Vector3.right * X_DIFF;
        float x = currentClient.transform.position.x;
        currentClient.transform.DOMoveX(x - X_DIFF, 1.0f);
    }

    public void ClientTimerEnded()
    {
        ClientLeave(currentClient);
    }

    public void ClientHappy()
    {
        Debug.Log("Happy Client got his BUBBLE TEA !");
        ClientLeave(currentClient);
    }

    public void ClientUnHappy()
    {
        Debug.Log("Un Happy Client didn't get his desired BUBBLE TEA !");
        ClientLeave(currentClient);
    }

    private void ClientLeave(GameObject client)
    {
        float x = client.transform.position.x;
        client.transform.DOMoveX(x + X_DIFF, 1.0f).OnComplete(() =>
        {
            Destroy(client);
            SpawnNextClient();
        });
    }
}