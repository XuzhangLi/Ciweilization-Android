using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

public class UserInput : Photon.MonoBehaviour
{
    private Ciweilization ciweilization;

    [HideInInspector] public Player player;

    private AudioManager audioManager;

    [HideInInspector] public SpriteRenderer chanceDisplaySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        ciweilization = FindObjectOfType<Ciweilization>();

        audioManager = FindObjectOfType<AudioManager>();

        player = GetComponent<Player>();

        chanceDisplaySpriteRenderer = GameObject.Find("Card Zoom").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            if (ciweilization.disconnectPanelOn == false)
            {
                if (ciweilization.activePlayerNumber == player.playerNumber && player.heroPowerModeOn == false)
                {
                    GetMouseClick();
                }
            }
        }
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) // if left click
        {
            // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                //Debug.Log("you just clicked on something.");

                if (hit.collider.CompareTag("Card"))
                {
                    if (player.clickChanceOnly)
                    {
                        return;
                    }

                    if (player.moves >= 1)
                    {
                        int objID = hit.collider.gameObject.GetPhotonView().viewID;
                        photonView.RPC("BuyCard", PhotonTargets.AllBuffered, objID);
                    }
                    else
                    {
                        audioManager.Play("Warning");
                    }
                }

                else if (hit.collider.CompareTag("TestCard"))
                {
                    int objID = hit.collider.gameObject.GetPhotonView().viewID;
                    photonView.RPC("BuyTestCard", PhotonTargets.AllBuffered, objID);
                }

                else if (hit.collider.CompareTag("Emote"))
                {
                    Debug.Log("You just clicked on an emote.");
                    string emoteName = hit.collider.gameObject.GetComponent<Emote>().name;
                    photonView.RPC("PlayAudioForAll", PhotonTargets.AllBuffered, emoteName);
                }

                else if (hit.collider.CompareTag("HeroTrigger"))
                {
                    //write these into a function later
                    StartCoroutine(ciweilization.CiweilizationDealHeroes(player.playerNumber));
                    ciweilization.CiweilizationSortHeroes();
                    photonView.RPC("PlayAudioForAll", PhotonTargets.AllBuffered, "Coin");
                }

                else if (hit.collider.CompareTag("HeroCard"))
                {
                    SelectHero(hit.collider.gameObject);
                    photonView.RPC("PlayAudioForAll", PhotonTargets.AllBuffered, "Coin");
                }

                else if (hit.collider.CompareTag("ChanceTrigger"))
                {
                    Debug.Log("You just clicked on a chance trigger.");
                    //write these into a function later
                    if (player.chances >= 1)
                    {
                        player.clickChanceOnly = true;
                        player.chances -= 1;
                        StartCoroutine(ciweilization.CiweilizationDealChances());
                        photonView.RPC("PlayAudioForAll", PhotonTargets.AllBuffered, "Coin");
                    }
                    else
                    {
                        audioManager.Play("Warning");
                    }
                }

                else if (hit.collider.CompareTag("ChanceCard"))
                {
                    player.clickChanceOnly = false;
                    //photonView.RPC("ChangeChanceCount", PhotonTargets.AllBuffered, 1);
                    int objID = hit.collider.gameObject.GetPhotonView().viewID;
                    photonView.RPC("UseChance", PhotonTargets.AllBuffered, objID);
                    //DestroyAll("ChanceCard");
                    photonView.RPC("PlayAudioForAll", PhotonTargets.AllBuffered, "Coin");
                    ciweilization.photonView.RPC("HideToggleDiscoveryButtonForAll", PhotonTargets.AllBuffered);
                }

                else if (hit.collider.CompareTag("Hero"))
                {
                    photonView.RPC("ChangePlayerMoves", PhotonTargets.AllBuffered, 0.5f);
                    photonView.RPC("PlayAudioForAll", PhotonTargets.AllBuffered, "Discard");
                }
            }
        }

        else if (Input.GetMouseButtonDown(1)) //if right click
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                if (hit.collider.CompareTag("Card"))
                {
                    if (player.clickChanceOnly)
                    {
                        return;
                    }
                    int objID = hit.collider.gameObject.GetPhotonView().viewID;
                    photonView.RPC("DiscardCard", PhotonTargets.AllBuffered, objID);
                }
                else if (hit.collider.CompareTag("BuildingPlayer1"))
                {
                    int objID = hit.collider.gameObject.GetPhotonView().viewID;
                    photonView.RPC("DestroyBuilding", PhotonTargets.AllBuffered, objID, 1);
                }
                else if (hit.collider.CompareTag("BuildingPlayer2"))
                {
                    int objID = hit.collider.gameObject.GetPhotonView().viewID;
                    photonView.RPC("DestroyBuilding", PhotonTargets.AllBuffered, objID, 2);
                }
                else if (hit.collider.CompareTag("BuildingPlayer3"))
                {
                    int objID = hit.collider.gameObject.GetPhotonView().viewID;
                    photonView.RPC("DestroyBuilding", PhotonTargets.AllBuffered, objID, 3);
                }
                else if (hit.collider.CompareTag("Hero"))
                {
                    photonView.RPC("ChangePlayerMoves", PhotonTargets.AllBuffered, -0.5f);
                    photonView.RPC("PlayAudioForAll", PhotonTargets.AllBuffered, "Discard");
                }
            }
        }
    }

    [PunRPC]
    public void BuyCard(int objID)
    {
        GameObject obj = PhotonView.Find(objID).gameObject;

        char chr = (obj.name[1]);
        int level = (int)(chr - '0');

        Vector3 position = obj.transform.position;
        Destroy(obj);
        Fill(level, position);

        if (obj.name == "B1")
        {
            /* The player can't build Blue-1 from the board if Blue-2-Wonder 
                has been built by one of their opponent. */
            if (ciweilization.wonderB2 == true && player.PlayerGetWonder_B2() == 0)
            {
                Debug.Log("You can't build Blue-1 on the board as Blue-2-Wonder " +
                                    "has been built by one of your opponent.");
                audioManager.Play("Wonder Ability");
                audioManager.Play("Warning");
                return;
            }
        }
        player.PlayerBuild(obj.name);
        player.moves -= 1;
    }

    [PunRPC]
    public void BuyTestCard(int objID)
    {
        GameObject obj = PhotonView.Find(objID).gameObject;

        string testCardName = obj.GetComponent<BuildingDisplay>().testName;

        char suit = testCardName[0];
        char chr = testCardName[1];
        int level = (int)(chr - '0');

        // If the given test card is the random building test card.
        if (suit == 'M')
        {
            player.PlayerBuildRandom(level);
        }
        else
        {
            player.PlayerBuild(testCardName);
        }
    }

    [PunRPC]
    public void DiscardCard(int objID)
    {
        Debug.Log("Discard Card Called");
        GameObject obj = PhotonView.Find(objID).gameObject;

        char chr = (obj.name[1]);
        int level = (int)(chr - '0');

        Vector3 position = obj.transform.position;
        Destroy(obj);
        Fill(level, position);

        audioManager.Play("Discard");
    }

    [PunRPC]
    public void DestroyBuilding(int objID, int playerNum)
    {
        GameObject obj = PhotonView.Find(objID).gameObject;
        Debug.Log("DestoryBuildingCalled");

        string name = obj.GetComponent<BuildingDisplay>().testName;
        Destroy(obj);
        
        if (playerNum == 1)
        {
            ciweilization.player1.PlayerDelete(name);
        }
        else if (playerNum == 2)
        {
            ciweilization.player2.PlayerDelete(name);
        }
        else if (playerNum == 3)
        {
            ciweilization.player3.PlayerDelete(name);
        }
        else
        {
            Debug.Log("Error! Invalid player number.");
        }

        audioManager.Play("Discard");
    }

    void Fill(int level, Vector3 position)
    {
        if (level == 1)
        {
            ciweilization.CiweilizationFill1(position);
        }
        else if (level == 2)
        {
            ciweilization.CiweilizationFill2(position);
        }
        else if (level == 3)
        {
            ciweilization.CiweilizationFill3(position);
        }
        else if (level == 4)
        {
            ciweilization.CiweilizationFill4(position);
        }
        else
        {
            Debug.Log("Error! The input need to be a level between 1 to 4");
        }


        Debug.Log("User Input Fill Method Called.");
    }

    void SelectHero(GameObject obj)
    {
        if (player.heroObj)
        {
            PhotonNetwork.Destroy(player.heroObj);
        }
        GameObject hero = PhotonNetwork.Instantiate("Hero", player.heroPos.transform.position, 
                                        Quaternion.identity, 0);
        player.heroObj = hero;

        int id = player.heroObj.GetPhotonView().viewID;

        photonView.RPC("SetCardName", PhotonTargets.AllBuffered, id, obj.name);

        DestroyAll("HeroCard");

        int heroNumber = int.Parse(player.heroObj.name.Substring(1));

        ciweilization.CiweilizationSetUpHeroPlayer(player.playerNumber, 
                                                   ciweilization.currentHeroNames[heroNumber]);
        Destroy(this.gameObject);
    }

    [PunRPC]
    public void UseChance(int objID)
    {
        player.chanceCount += 1;

        GameObject obj = PhotonView.Find(objID).gameObject;
        int chanceNumber = int.Parse(obj.name.Substring(1));

        chanceDisplaySpriteRenderer.sprite = obj.GetComponent<SpriteRenderer>().sprite;

        if (player.photonView.isMine)
        {
            DestroyAll("ChanceCard");
        }

        player.PlayerUseChance(ciweilization.currentChanceNames[chanceNumber]);
    }

    private void DestroyAll(string tag)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < targets.Length; i++)
        {
            PhotonNetwork.Destroy(targets[i]);
        }
    }

    [PunRPC]
    public void SetCardName(int id, string cardName)
    {
        PhotonView.Find(id).gameObject.name = cardName;
    }

    [PunRPC]
    public void PlayAudioForAll(string name)
    {
        audioManager.Play(name);
    }
}
