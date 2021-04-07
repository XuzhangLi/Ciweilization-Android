using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Photon;

public class TestGameManager : Photon.MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject GameCanvas;
    public GameObject SceneCamera;
    public Text pingText;
    public GameObject disconnectUI;
    public TestPlayer player1;
    public GameObject testObj;

    public int playerCount = 0;

    private bool Off = false;

    public Locations locations;

    private void Start()
    {
        locations = GetComponent<Locations>();
    }

    public void SpawnPlayer()
    {

        GameObject player1Obj = PhotonNetwork.Instantiate(playerPrefab.name,
                        new Vector2(this.transform.position.x, this.transform.position.y),
                        Quaternion.identity, 0);

        GameCanvas.SetActive(false);
        //SceneCamera.SetActive(false);

        photonView.RPC("AddPlayerCount", PhotonTargets.AllBuffered);
    }

    [PunRPC]
    public void AddPlayerCount()
    {
        playerCount += 1;
    }

    private void Awake()
    {
        //GameCanvas.SetActive(true);
    }

    private void Update()
    {
        pingText.text = ("Ping: " + PhotonNetwork.GetPing());
        CheckInput(); 
    }

    private void CheckInput()
    {
        if(Off && Input.GetKeyDown(KeyCode.Escape))
        {
            disconnectUI.SetActive(false);
            Off = false;
        } else if (!Off && Input.GetKeyDown(KeyCode.Escape))
        {
            disconnectUI.SetActive(true);
            Off = true;
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Main Menu"); 
    }
}
