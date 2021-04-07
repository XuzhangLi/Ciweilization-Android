using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Linq;
using TMPro;

public class Ciweilization : Photon.MonoBehaviour
{
    #region Variables
    public static string[] suits = new string[] { "G", "R", "Y", "B" };
    public static string[] values = new string[] { "1", "2", "3", "4" };

    private AudioManager audioManager;

    public Player player1;
    public Player player2;
    public Player player3;

    public List<string>[] level1s;
    public List<string>[] level2s;
    public List<string>[] level3s;
    public List<string>[] level4s;
    public List<string>[] player1Heroes;
    public List<string>[] player2Heroes;
    public List<string>[] player3Heroes;
    public List<string>[] chances;

    public Sprite[] cardFaces1;
    public Sprite[] cardFaces2;
    public Sprite[] cardFaces3;
    public Sprite[] cardFaces4;
    public Sprite[] cardFacesHeroes;
    public Sprite[] cardFacesChances;

    public GameObject cardPrefab;
    public GameObject heroCardPrefab;
    public GameObject chanceCardPrefab;

    [HideInInspector] public GameObject[] level1Pos;
    [HideInInspector] public GameObject[] level2Pos;
    [HideInInspector] public GameObject[] level3Pos;
    [HideInInspector] public GameObject[] level4Pos;
    public GameObject[] discoverPos;
    public GameObject[] heroPos;

    public List<string> heroNames;
    public List<string> currentHeroNames;
    public List<string> currentChanceNames;

    [HideInInspector] public List<string> deck1;
    [HideInInspector] public List<string> deck2;
    [HideInInspector] public List<string> deck3;
    [HideInInspector] public List<string> deck4;
    [HideInInspector] public List<string> deckHeroes;
    [HideInInspector] public List<string> deckChances;

    private List<string> level1_0 = new List<string>();
    private List<string> level1_1 = new List<string>();
    private List<string> level1_2 = new List<string>();
    private List<string> level1_3 = new List<string>();
    private List<string> level2_0 = new List<string>();
    private List<string> level2_1 = new List<string>();
    private List<string> level2_2 = new List<string>();
    private List<string> level3_0 = new List<string>();
    private List<string> level3_1 = new List<string>();
    private List<string> level4_0 = new List<string>();

    private List<string> player1_0 = new List<string>();
    private List<string> player1_1 = new List<string>();
    private List<string> player1_2 = new List<string>();
    private List<string> player2_0 = new List<string>();
    private List<string> player2_1 = new List<string>();
    private List<string> player2_2 = new List<string>();
    private List<string> player3_0 = new List<string>();
    private List<string> player3_1 = new List<string>();
    private List<string> player3_2 = new List<string>();

    private List<string> chance0 = new List<string>();
    private List<string> chance1 = new List<string>();
    private List<string> chance2 = new List<string>();
    public GameObject chanceDiscovery1;
    public GameObject chanceDiscovery2;
    public GameObject chanceDiscovery3;

    [HideInInspector] public int turn;
    [HideInInspector] public bool isSpring;
    [HideInInspector] public bool isSummer;
    [HideInInspector] public bool isFall;
    [HideInInspector] public bool isWinter;

    private TextMeshProUGUI turnText;
    private TextMeshProUGUI ruleText;
    private TextMeshProUGUI playerConnectionText;
    private TextMeshProUGUI pingText;
    private TextMeshProUGUI player1NameText;
    private TextMeshProUGUI player2NameText;
    private TextMeshProUGUI player3NameText;
    private TextMeshProUGUI hideDiscoveryButtonText;
    private TextMeshProUGUI endTurnText;

    private Locations locations;

    public int activePlayerNumber = 0;

    [HideInInspector] public bool wonderG2 = false;
    [HideInInspector] public bool wonderR2 = false;
    [HideInInspector] public bool wonderY2 = false;
    [HideInInspector] public bool wonderB2 = false;
    [HideInInspector] public bool wonderG3 = false;
    [HideInInspector] public bool wonderR3 = false;
    [HideInInspector] public bool wonderY3 = false;
    [HideInInspector] public bool wonderB3 = false;
    [HideInInspector] public bool wonderG4 = false;
    [HideInInspector] public bool wonderR4 = false;
    [HideInInspector] public bool wonderY4 = false;
    [HideInInspector] public bool wonderB4 = false;

    public Image[] actives;
    public Sprite isActive;

    [HideInInspector] public int playerCount = 0;

    public GameObject disconnectUI;
    [HideInInspector] public bool disconnectPanelOn = false;
    public GameObject sitButton;
    public GameObject startButton;
    public GameObject endTurnButton;
    public GameObject hideDiscoveryButton;
    public GameObject chanceTrigger;
    [HideInInspector] public bool showingDiscoveryCards = false;
    [HideInInspector] public int screenWidth = 800;
    [HideInInspector] public bool gameStarted = false;
    [HideInInspector] public bool isLastTurn = false;
    [HideInInspector] public bool win = false;

    #endregion

    #region Unity Functions
    /* Start is called before the first frame update. */
    void Start()
    {
        locations = GetComponent<Locations>();

        // Doesn't actually change the screen size, only changes 
        //      what this game manager thinks the screen size is.
        screenWidth = 800;

        turnText = GameObject.Find("Canvas/Turn Text").GetComponent<TextMeshProUGUI>();
        turnText.text = "Hero Selection Time";

        ruleText = GameObject.Find("Canvas/Rule Text").GetComponent<TextMeshProUGUI>();
        ruleText.text = "";

        pingText = GameObject.Find("Canvas/Ping Text").GetComponent<TextMeshProUGUI>();
        pingText.text = "";

        playerConnectionText = GameObject.Find("Canvas/Player Connection Text").GetComponent<TextMeshProUGUI>();
        playerConnectionText.text = "Players Connected: " + playerCount;

        player1NameText = GameObject.Find("Canvas/Player 1 Name Text").GetComponent<TextMeshProUGUI>();
        player2NameText = GameObject.Find("Canvas/Player 2 Name Text").GetComponent<TextMeshProUGUI>();
        player3NameText = GameObject.Find("Canvas/Player 3 Name Text").GetComponent<TextMeshProUGUI>();

        audioManager = FindObjectOfType<AudioManager>();

        turn = 0;
        activePlayerNumber = 1;
        wonderG2 = wonderR2 = wonderY2 = wonderB2 = wonderG3 = wonderR3 = wonderY3 = wonderB3 =
            wonderG4 = wonderR4 = wonderY4 = wonderB4 = false;

        isSpring = false;
        isSummer = false;
        isFall = false;
        isWinter = false;

        level1s = new List<string>[] { level1_0, level1_1, level1_2, level1_3 };
        level2s = new List<string>[] { level2_0, level2_1, level2_2 };
        level3s = new List<string>[] { level3_0, level3_1 };
        level4s = new List<string>[] { level4_0 };

        player1Heroes = new List<string>[] { player1_0, player1_1, player1_2 };
        player2Heroes = new List<string>[] { player2_0, player2_1, player2_2 };
        player3Heroes = new List<string>[] { player3_0, player3_1, player3_2 };
        chances = new List<string>[] { chance0, chance1, chance2 };


        Debug.Log("Hero Selection Time.");
        PrepareCards();
        //StartCoroutine(CiweilizationDealHeroes(1));

        audioManager.Play("Season Start");

        actives[0].sprite = isActive;
        actives[1].sprite = isActive;
        actives[2].sprite = isActive;
        actives[0].enabled = false;
        actives[1].enabled = false;
        actives[2].enabled = false;
    }

    /* Update is called once per frame */
    void Update()
    {
        playerConnectionText.text = "Players Connected: " + playerCount;

        pingText.text = ("Ping: " + PhotonNetwork.GetPing() + " ");

        ShowActivePlayerBar();

        CheckDisconnectInput();

        ChangeEndTurnText();
    }
    #endregion

    #region Unity Functions Sub-functions
    /* Show active player bar according to current active player. */
    private void ShowActivePlayerBar()
    {
        if (activePlayerNumber == 1)
        {
            actives[0].enabled = true;
            actives[1].enabled = false;
            actives[2].enabled = false;
        }
        else if (activePlayerNumber == 2)
        {
            actives[0].enabled = false;
            actives[1].enabled = true;
            actives[2].enabled = false;
        }
        else if (activePlayerNumber == 3)
        {
            actives[0].enabled = false;
            actives[1].enabled = false;
            actives[2].enabled = true;
        }
    }

    /* Checks if the player is trying to opne/close the disconnect panel and react accordingly.*/
    private void CheckDisconnectInput()
    {
        if (disconnectPanelOn == true && Input.GetKeyDown(KeyCode.Escape))
        {
            disconnectUI.SetActive(false);
            disconnectPanelOn = false;
        }
        else if (disconnectPanelOn == false && Input.GetKeyDown(KeyCode.Escape))
        {
            disconnectUI.SetActive(true);
            disconnectPanelOn = true;
        }
    }

    /* ChangeS end turn button text automatically based on whether it's the client's player's turn. */
    private void ChangeEndTurnText()
    {
        if (gameStarted == false)
        {
            return;
        }

        if (player1 && player1.photonView.isMine)
        {
            if (activePlayerNumber == 1)
            {
                endTurnText.text = "End Turn"; 
            }
            else
            {
                endTurnText.text = "Opponent's Turn";
            }
        }
        else if (player2 && player2.photonView.isMine)
        {
            if (activePlayerNumber == 2)
            {
                endTurnText.text = "End Turn";
            }
            else
            {
                endTurnText.text = "Opponent's Turn";
            }
        }
        else if (player3 && player3.photonView.isMine)
        {
            if (activePlayerNumber == 3)
            {
                endTurnText.text = "End Turn";
            }
            else
            {
                endTurnText.text = "Opponent's Turn";
            }
        }
        else
        {
            Debug.Log("You don't control any player now.");
        }
    }
    #endregion

    #region ESC Panel Functions
    /* Disconnect the client from the room, reduce player count by 1, 
     * and take the player back to the main menu.*/
    public void Disconnect()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Main Menu");
    }

    /* Switch from windowed mode to full screen mode or the other way around.*/
    public void ToggleScreenSize()
    {
        if (screenWidth == 800)
        {
            Screen.SetResolution(1280, 720, false);
            screenWidth = 1280;
        }
        else if (screenWidth == 1280)
        {
            Screen.SetResolution(2560, 1440, true);
            screenWidth = 2560;
        }
        else if (screenWidth == 2560)
        {
            Screen.SetResolution(800, 450, false);
            screenWidth = 800;
        }
    }
    #endregion

    #region Generate Deck Functions
    /* Takes in a level and how many copies are there for each card; 
    Gives out a deck of buildings. */
    public static List<string> GenerateDeck(string v, int x) 
    {
        List<string> newDeck = new List<string>();
        for (int i = 0; i < x; i++)
        {
            foreach (string s in suits)
            {
                newDeck.Add(s + v);
            }
        }
        return newDeck;
    }

    /* Generates the chance deck as a list of strings. */
    public List<string> GenerateChanceDeck()
    {
        List<string> newDeck = new List<string>();
        for (int k = 0; k < cardFacesChances.Length; k++)
        {
            newDeck.Add("c" + k);
        }
        return newDeck;
    }

    /* Generates the hero deck as a list of strings. */
    public List<string> GenerateHeroDeck()
    {
        List<string> newDeck = new List<string>();
        for (int k = 0; k < cardFacesHeroes.Length; k++)
        {
            newDeck.Add("h" + k);
        }
        return newDeck;
    }

    /* Generates and shuffles the building decks, the hero deck, and the chance deck. */
    public void PrepareCards()
    {
        deck1 = GenerateDeck("1", 12);
        deck2 = GenerateDeck("2", 9);
        deck3 = GenerateDeck("3", 6);
        deck4 = GenerateDeck("4", 3);
        deckHeroes = GenerateHeroDeck();
        deckChances = GenerateChanceDeck();

        Shuffle(deck1);
        Shuffle(deck2);
        Shuffle(deck3);
        Shuffle(deck4);
        Shuffle(deckHeroes);
        Shuffle(deckChances);

        CiweilizationSort();
        CiweilizationSortHeroes();
        CiweilizationSortChances();
    }

    /* Takes in a deck, shuffles the deck in a rather naive way. */
    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    /* Put cards in building decks into their corresponding boxs, so they are ready to be dealt out.*/
    public void CiweilizationSort()
    {
        for (int i = 0; i < 4; i++)
        {
            level1s[i].Add(deck1.Last<string>());
            deck1.RemoveAt(deck1.Count - 1);
        }
        for (int i = 0; i < 3; i++)
        {
            level2s[i].Add(deck2.Last<string>());
            deck2.RemoveAt(deck2.Count - 1);
        }
        for (int i = 0; i < 2; i++)
        {
            level3s[i].Add(deck3.Last<string>());
            deck3.RemoveAt(deck3.Count - 1);
        }
        for (int i = 0; i < 1; i++)
        {
            level4s[i].Add(deck4.Last<string>());
            deck4.RemoveAt(deck4.Count - 1);
        }
    }

    /* Generates hero cards (strings) and put them into their corresponding boxs, 
     * so they are ready to be dealt out.*/
    public void CiweilizationSortHeroes()
    {
        deckHeroes = GenerateHeroDeck();
        Shuffle(deckHeroes);

        for (int i = 0; i < 3; i++)
        {
            player1Heroes[i].Add(deckHeroes.Last<string>());
            deckHeroes.RemoveAt(deckHeroes.Count - 1);

            player2Heroes[i].Add(deckHeroes.Last<string>());
            deckHeroes.RemoveAt(deckHeroes.Count - 1);

            player3Heroes[i].Add(deckHeroes.Last<string>());
            deckHeroes.RemoveAt(deckHeroes.Count - 1);
        }
    }

    /* Generates chance cards (strings) and put them into their corresponding boxs, 
     * so they are ready to be dealt out.*/
    public void CiweilizationSortChances()
    {
        deckChances = GenerateChanceDeck();
        Shuffle(deckChances);

        for (int i = 0; i < 3; i++)
        {
            chances[i].Add(deckChances.Last<string>());
            deckChances.RemoveAt(deckChances.Count - 1);
        }
    }
    #endregion

    #region Set Up Player Functions
    /* Takes in a player number;
     * Set up the corresponding player, including its building, hero, and energy locations
     * and its player number. */
    public void CiweilizationSetUpPlayer()
    {
        photonView.RPC("ChangePlayerCount", PhotonTargets.AllBuffered, 1);

        if (playerCount == 1)
        {
            GameObject player1Obj = PhotonNetwork.Instantiate("Player Prefabs/MainPlayer",
                                    new Vector2(this.transform.position.x, this.transform.position.y),
                                    Quaternion.identity, 0);
            int playerID = player1Obj.GetPhotonView().viewID;
            photonView.RPC("AssignPlayerParameters", PhotonTargets.AllBuffered, 1, playerID);
            photonView.RPC("DisplayPlayerName", PhotonTargets.AllBuffered, 1, PhotonNetwork.playerName);
            photonView.RPC("PlayAudioForAll", PhotonTargets.AllBuffered, "Season Start");
            sitButton.SetActive(false);
            startButton.SetActive(true);
        }
        else if (playerCount == 2)
        {
            GameObject player2Obj = PhotonNetwork.Instantiate("Player Prefabs/MainPlayer",
                                    new Vector2(this.transform.position.x, this.transform.position.y),
                                    Quaternion.identity, 0);
            int playerID = player2Obj.GetPhotonView().viewID;
            photonView.RPC("AssignPlayerParameters", PhotonTargets.AllBuffered, 2, playerID);
            photonView.RPC("DisplayPlayerName", PhotonTargets.AllBuffered, 2, PhotonNetwork.playerName);
            photonView.RPC("PlayAudioForAll", PhotonTargets.AllBuffered, "Season Start");
            sitButton.SetActive(false);
        }
        else if (playerCount == 3)
        {
            GameObject player3Obj = PhotonNetwork.Instantiate("Player Prefabs/MainPlayer",
                                    new Vector2(this.transform.position.x, this.transform.position.y),
                                    Quaternion.identity, 0);
            int playerID = player3Obj.GetPhotonView().viewID;
            photonView.RPC("AssignPlayerParameters", PhotonTargets.AllBuffered, 3, playerID);
            photonView.RPC("DisplayPlayerName", PhotonTargets.AllBuffered, 3, PhotonNetwork.playerName);
            photonView.RPC("PlayAudioForAll", PhotonTargets.AllBuffered, "Season Start");
            sitButton.SetActive(false);
        }
        else
        {
            Debug.Log("Error! A player has an invalid number.");
        }
    }

    /* Takes in a player number and a hero name;
    * Set up the corresponding player with the given hero and its player number. */
    public void CiweilizationSetUpHeroPlayer(int playerNum, string heroName)
    {
        if (heroName == "na")
        {
            heroName = "MainPlayer";
        }

        if (playerNum == 1)
        {
            GameObject player1Obj = PhotonNetwork.Instantiate("Player Prefabs/" + heroName,
                                    new Vector2(this.transform.position.x, this.transform.position.y),
                                    Quaternion.identity, 0);
            int playerID = player1Obj.GetPhotonView().viewID;
            photonView.RPC("AssignPlayerParameters", PhotonTargets.AllBuffered, 1, playerID);
            photonView.RPC("PlayAudioForAll", PhotonTargets.AllBuffered, "Season Start");
        }
        else if (playerNum == 2)
        {
            GameObject player2Obj = PhotonNetwork.Instantiate("Player Prefabs/" + heroName,
                                    new Vector2(this.transform.position.x, this.transform.position.y),
                                    Quaternion.identity, 0);
            int playerID = player2Obj.GetPhotonView().viewID;
            photonView.RPC("AssignPlayerParameters", PhotonTargets.AllBuffered, 2, playerID);
            photonView.RPC("PlayAudioForAll", PhotonTargets.AllBuffered, "Season Start");
        }
        else if (playerNum == 3)
        {
            GameObject player3Obj = PhotonNetwork.Instantiate("Player Prefabs/" + heroName,
                                    new Vector2(this.transform.position.x, this.transform.position.y),
                                    Quaternion.identity, 0);
            int playerID = player3Obj.GetPhotonView().viewID;
            photonView.RPC("AssignPlayerParameters", PhotonTargets.AllBuffered, 3, playerID);
            photonView.RPC("PlayAudioForAll", PhotonTargets.AllBuffered, "Season Start");
        }
        else
        {
            Debug.Log("Error! A player has an invalid number.");
        }
    }

    /* Start the game!
       Namely, start the theme music, and deal heroes to the first player.
       Only the client owning player1 can call this function.*/

    [PunRPC]
    public void DisplayPlayerName(int playerNum, string name)
    {
        if (playerNum == 1)
        {
            player1NameText.text = name;
        }
        else if (playerNum == 2)
        {
            player2NameText.text = name;
        }
        else if (playerNum == 3)
        {
            player3NameText.text = name;
        }
        else
        {
            Debug.Log("Error! Invalid player number when displaying player name.");
        }
    }

    [PunRPC]
    public void AssignPlayerParameters(int num, int playerID)
    {
        if (num == 1)
        {
            GameObject obj = PhotonView.Find(playerID).gameObject;
            obj.name = "Test Player 1";
            player1 = obj.GetComponent<Player>();
            //testObj = obj.GetComponent<Photon.MonoBehaviour>().gameObject;
            player1.level1Pos = locations.player1Level1Pos;
            player1.level2Pos = locations.player1Level2Pos;
            player1.level3Pos = locations.player1Level3Pos;
            player1.level4Pos = locations.player1Level4Pos;
            player1.GetComponent<Energy>().energies = locations.player1Energies;
            player1.startEnergy = true;

            player1.playerNumber = 1;
            player1.heroPos = heroPos[0];
        }
        else if (num == 2)
        {
            GameObject obj = PhotonView.Find(playerID).gameObject;
            obj.name = "Test Player 2";
            player2 = obj.GetComponent<Player>();
            player2.level1Pos = locations.player2Level1Pos;
            player2.level2Pos = locations.player2Level2Pos;
            player2.level3Pos = locations.player2Level3Pos;
            player2.level4Pos = locations.player2Level4Pos;
            player2.GetComponent<Energy>().energies = locations.player2Energies;
            player2.startEnergy = true;

            player2.playerNumber = 2;
            player2.heroPos = heroPos[1];
        }
        else if (num == 3)
        {
            GameObject obj = PhotonView.Find(playerID).gameObject;
            obj.name = "Test Player 3";
            player3 = obj.GetComponent<Player>();
            player3.level1Pos = locations.player3Level1Pos;
            player3.level2Pos = locations.player3Level2Pos;
            player3.level3Pos = locations.player3Level3Pos;
            player3.level4Pos = locations.player3Level4Pos;
            player3.GetComponent<Energy>().energies = locations.player3Energies;
            player3.startEnergy = true;

            player3.playerNumber = 3;
            player3.heroPos = heroPos[2];
        }
    }

    [PunRPC]
    public void ChangePlayerCount(int x)
    {
        if (playerCount < 3)
        {
            playerCount += x;
        }
    }
    #endregion

    #region Start Game Functions
    public void StartGame()
    {
        photonView.RPC("PlayAudioForAll", PhotonTargets.AllBuffered, "Theme");
        photonView.RPC("PlayAudioForAll", PhotonTargets.AllBuffered, "Season Start");
        photonView.RPC("ShowEndTurnButton", PhotonTargets.AllBuffered);
        StartCoroutine(CiweilizationDealHeroes(1));
    }

    [PunRPC]
    public void ShowEndTurnButton()
    {
        endTurnButton.SetActive(true);
        endTurnText = GameObject.Find("Canvas/End Turn Button/End Turn Text").GetComponent<TextMeshProUGUI>();
        chanceTrigger.SetActive(true);
        startButton.SetActive(false);

        gameStarted = true;
    }

    [PunRPC]
    public void PlayAudioForAll(string name)
    {
        audioManager.Play(name);
    }
    #endregion

    #region Deal Cards Functions
    /* Deals 4 level-1 buildings from the box */
    IEnumerator CiweilizationDeal1()
    {
        for (int i = 0; i < 4; i++)
        {
            foreach (string card in level1s[i])
            {
                yield return new WaitForSeconds(0.01f);
                GameObject newCard = PhotonNetwork.Instantiate(cardPrefab.name, 
                                                    level1Pos[i].transform.position, 
                                                    Quaternion.identity, 0);
                int id = newCard.GetPhotonView().viewID;

                photonView.RPC("SetCardName", PhotonTargets.AllBuffered, id, card);
            }
        }
    }

    /* Deals 3 level-2 buildings from the box */
    IEnumerator CiweilizationDeal2()
    {
        for (int i = 0; i < 3; i++)
        {
            foreach (string card in level2s[i])
            {
                yield return new WaitForSeconds(0.01f);
                GameObject newCard = PhotonNetwork.Instantiate(cardPrefab.name,
                                    level2Pos[i].transform.position,
                                    Quaternion.identity, 0);
                int id = newCard.GetPhotonView().viewID;

                photonView.RPC("SetCardName", PhotonTargets.AllBuffered, id, card);
            }
        }
    }

    /* Deals 2 level-3 buildings from the box */
    IEnumerator CiweilizationDeal3()
    {
        for (int i = 0; i < 2; i++)
        {
            foreach (string card in level3s[i])
            {
                yield return new WaitForSeconds(0.01f);
                GameObject newCard = PhotonNetwork.Instantiate(cardPrefab.name,
                                     level3Pos[i].transform.position,
                                     Quaternion.identity, 0);
                int id = newCard.GetPhotonView().viewID;

                photonView.RPC("SetCardName", PhotonTargets.AllBuffered, id, card);
            }
        }
    }

    /* Deals 1 level-4 buildings from the box */
    IEnumerator CiweilizationDeal4()
    {
        for (int i = 0; i < 1; i++)
        {
            foreach (string card in level4s[i])
            {
                yield return new WaitForSeconds(0.01f);
                GameObject newCard = PhotonNetwork.Instantiate(cardPrefab.name,
                                    level4Pos[i].transform.position,
                                    Quaternion.identity, 0);
                int id = newCard.GetPhotonView().viewID;

                photonView.RPC("SetCardName", PhotonTargets.AllBuffered, id, card);
            }
        }
    }

    /* Takes in a player number, and deals 3 heroes for that player from the box.*/
    public IEnumerator CiweilizationDealHeroes(int playerNum)
    {
        for (int i = 0; i < 3; i++)
        {
            if (playerNum == 1 && player1.photonView.isMine)
            {
                string card = player1Heroes[i].Last<string>();
                yield return new WaitForSeconds(0.01f);
                
                GameObject newCard = PhotonNetwork.Instantiate(heroCardPrefab.name, discoverPos[i].transform.position,
                                                            Quaternion.identity, 0);
                int id = newCard.GetPhotonView().viewID;
                
                photonView.RPC("SetCardName", PhotonTargets.AllBuffered, id, card);       
            }
            else if (playerNum == 2 && player2.photonView.isMine)
            {
                string card = player2Heroes[i].Last<string>();
                yield return new WaitForSeconds(0.1f);
                GameObject newCard = PhotonNetwork.Instantiate(heroCardPrefab.name, discoverPos[i].transform.position,
                                                            Quaternion.identity, 0);
                int id = newCard.GetPhotonView().viewID;

                photonView.RPC("SetCardName", PhotonTargets.AllBuffered, id, card);
            }
            else if (playerNum == 3 && player3.photonView.isMine)
            {
                string card = player3Heroes[i].Last<string>();
                yield return new WaitForSeconds(0.1f);
                GameObject newCard = PhotonNetwork.Instantiate(heroCardPrefab.name, discoverPos[i].transform.position,
                                                            Quaternion.identity, 0);
                int id = newCard.GetPhotonView().viewID;

                photonView.RPC("SetCardName", PhotonTargets.AllBuffered, id, card);
            }
        }
    }

    /* Deal out three chances from the box, and prepares the box for next time.*/
    public IEnumerator CiweilizationDealChances()
    {
        for (int i = 0; i < 3; i++)
        {
            string card = chances[i].Last<string>();
            yield return new WaitForSeconds(0.01f);
            GameObject newCard = PhotonNetwork.Instantiate(chanceCardPrefab.name,
                                                            discoverPos[i].transform.position,
                                                            Quaternion.identity, 0);
            int id = newCard.GetPhotonView().viewID;

            photonView.RPC("SetCardName", PhotonTargets.AllBuffered, id, card);
            photonView.RPC("TrackChanceCard", PhotonTargets.AllBuffered, id, (i + 1));
            photonView.RPC("ShowToggleDiscoveryButtonForAll", PhotonTargets.AllBuffered);
        }

        CiweilizationSortChances();
    }

    /* Set the name of object with the given photonview id to be the given cardname. */
    [PunRPC]
    public void SetCardName(int id, string cardName)
    {
        PhotonView.Find(id).gameObject.name = cardName;
    }
    
    /* Track the chance discovery card by assigning it to a local variable according
     * to the given number. */
    [PunRPC]
    public void TrackChanceCard(int id, int num)
    {
        if (num == 1)
        {
            chanceDiscovery1 = PhotonView.Find(id).gameObject;
        }
        else if (num == 2)
        {
            chanceDiscovery2 = PhotonView.Find(id).gameObject;
        }
        else if (num == 3)
        {
            chanceDiscovery3 = PhotonView.Find(id).gameObject;
        }
        else
        {
            Debug.Log("Error! Invalid chance discovery number!");
        }
    }

    /* Show the toggle discovery button to all clients.*/
    [PunRPC]
    public void ShowToggleDiscoveryButtonForAll()
    {
        showingDiscoveryCards = true;
        hideDiscoveryButton.SetActive(true);
        hideDiscoveryButtonText = GameObject.Find("Canvas/Hide Discovery Button/Hide Discovery Text")
                                            .GetComponent<TextMeshProUGUI>();
        hideDiscoveryButtonText.text = "Show Board";
    }

    /* Hide the toggle discovery button to all clients.*/
    [PunRPC]
    public void HideToggleDiscoveryButtonForAll()
    {
        showingDiscoveryCards = false;
        hideDiscoveryButton.SetActive(false);
    }

    /* Toggle showing/hiding the discovery cards. */
    public void ToggleDiscovery()
    {
        if (showingDiscoveryCards)
        {
            chanceDiscovery1.SetActive(false);
            chanceDiscovery2.SetActive(false);
            chanceDiscovery3.SetActive(false);
            showingDiscoveryCards = false;
            hideDiscoveryButtonText.text = "Show Chances";
        }
        else if (showingDiscoveryCards == false)
        {
            chanceDiscovery1.SetActive(true);
            chanceDiscovery2.SetActive(true);
            chanceDiscovery3.SetActive(true);
            showingDiscoveryCards = true;
            hideDiscoveryButtonText.text = "Show Board";
        }
    }

    #endregion

    #region Fill Cards Functions
    /* Takes in the vector3 position of the missing level-1 card on the board,
     * and fill in the top card from the level-1 building deck.*/
    public void CiweilizationFill1(Vector3 position)
    {
        if (player1.photonView.isMine)
        {
            string card = deck1.Last<string>();
            deck1.RemoveAt(deck1.Count - 1);

            GameObject newCard = PhotonNetwork.Instantiate(cardPrefab.name, position,
                                                           Quaternion.identity, 0);

            int id = newCard.GetPhotonView().viewID;

            photonView.RPC("SetCardName", PhotonTargets.AllBuffered, id, card);
        }
    }

    /* Takes in the vector3 position of the missing level-2 card on the board,
     * and fill in the top card from the level-2 building deck.*/
    public void CiweilizationFill2(Vector3 position)
    {
        if (player1.photonView.isMine)
        {
            string card = deck2.Last<string>();
            deck2.RemoveAt(deck2.Count - 1);

            GameObject newCard = PhotonNetwork.Instantiate(cardPrefab.name, position,
                                                           Quaternion.identity, 0);
            int id = newCard.GetPhotonView().viewID;

            photonView.RPC("SetCardName", PhotonTargets.AllBuffered, id, card);
        }
    }

    /* Takes in the vector3 position of the missing level-3 card on the board,
     * and fill in the top card from the level-3 building deck.*/
    public void CiweilizationFill3(Vector3 position)
    {
        if (player1.photonView.isMine)
        {
            string card = deck3.Last<string>();
            deck3.RemoveAt(deck3.Count - 1);

            GameObject newCard = PhotonNetwork.Instantiate(cardPrefab.name, position,
                                                           Quaternion.identity, 0);
            int id = newCard.GetPhotonView().viewID;

            photonView.RPC("SetCardName", PhotonTargets.AllBuffered, id, card);
        }
    }

    /* Takes in the vector3 position of the missing level-4 card on the board,
     * and fill in the top card from the level-4 building deck.*/
    public void CiweilizationFill4(Vector3 position)
    {
        if (player1.photonView.isMine)
        {
            string card = deck4.Last<string>();
            deck4.RemoveAt(deck4.Count - 1);

            GameObject newCard = PhotonNetwork.Instantiate(cardPrefab.name, position,
                                                           Quaternion.identity, 0);
            int id = newCard.GetPhotonView().viewID;

            photonView.RPC("SetCardName", PhotonTargets.AllBuffered, id, card);
        }
    }

    /* Discards all card on the board and fill in new cards. */
    public virtual IEnumerator CiweilizationResetBoard()
    {
        //Does nothing if the client doesn't own player 1.
        //(This function only works when called by the main client.)
        if (player1.photonView.isMine == false)
        {
            yield break;
        }
            
        //If it's Winter already
        if (isWinter)
        {
            for (int i = 0; i < 1; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(level4Pos[i].transform.position, Vector2.zero);
                if (hit)
                {
                    int objID = hit.collider.gameObject.GetPhotonView().viewID;
                    player1.photonView.RPC("DiscardCard", PhotonTargets.AllBuffered, objID);
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }

        //If its Fall of later
        if (isFall || isWinter)
        {
            for (int i = 0; i < 2; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(level3Pos[i].transform.position, Vector2.zero);
                if (hit)
                {
                    int objID = hit.collider.gameObject.GetPhotonView().viewID;
                    player1.photonView.RPC("DiscardCard", PhotonTargets.AllBuffered, objID);
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }

        //If it's Summer or later
        if (isSummer || isFall || isWinter)
        {
            for (int i = 0; i < 3; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(level2Pos[i].transform.position, Vector2.zero);
                if (hit)
                {
                    int objID = hit.collider.gameObject.GetPhotonView().viewID;
                    player1.photonView.RPC("DiscardCard", PhotonTargets.AllBuffered, objID);
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }

        //If it's Spring or later
        if (isSpring || isSummer || isFall || isWinter)
        {
            for (int i = 0; i < 4; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(level1Pos[i].transform.position, Vector2.zero);
                if (hit)
                {
                    int objID = hit.collider.gameObject.GetPhotonView().viewID;
                    player1.photonView.RPC("DiscardCard", PhotonTargets.AllBuffered, objID);
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }
    }

    #endregion

    #region Next Turn Functions
    /* Start next turn, give players moves, and changes season if needed.*/
    public void CiweilizationNextTurn()
    {
        if (isLastTurn == true)
        {
            if (win == true)
            {
                audioManager.Play("Player Win");
                turnText.text = "You Won!";
            }
            else
            {
                audioManager.Play("Player Lose");
                turnText.text = "You Lost.";
            }
            return;
        }

        turn += 1;

        if (turn == 1)
        {
            //audioManager.StopPlaying("Theme");
            //audioManager.Play("Spring Theme");
            audioManager.Play("Season Start");

            isSpring = true;
            Debug.Log("You've selected heroes. Spring comes!");
            ruleText.text = "Era of Agriculture!\n\n"
                    + "(1) Gain 0.25 move for every level-1.\n"
                    + "(2) Gain 0.5 move for every level-2.\n"
                    + "(3) Doubles for Green.";
            if (player1.photonView.isMine)
            {
                StartCoroutine(CiweilizationDeal1());
            }
            player1.PlayerStartSpring();
            if (player2)
            {
                player2.PlayerStartSpring();
            }
            if (player3)
            {
                player3.PlayerStartSpring();
            }
        }
        if (turn == 4 && isSpring == true)
        {
            //audioManager.StopPlaying("Spring Theme");
            //audioManager.Play("Summer Theme");
            audioManager.Play("Season Start");

            isSpring = false;
            isSummer = true;
            ruleText.text = "Era of Industry! \n\n"
                    + "(1) Gain 0.25 move for every level-2.\n"
                    + "(2) Gain 0.5 move for every level-3.\n"
                    + "(3) Doubles for Red.";
            //ruleText.color = Color.red;
            Debug.Log("Spring has past and summer has come.");
            if (player1.photonView.isMine)
            {
                StartCoroutine(CiweilizationDeal2());
            }

            player1.PlayerStartSummer();
            if (player2)
            {
                player2.PlayerStartSummer();
            }
            if (player3)
            {
                player3.PlayerStartSummer();
            }
        }
        else if (turn == 7 && isSummer == true)
        {
            //audioManager.StopPlaying("Summer Theme");
            //audioManager.Play("Fall Theme");
            audioManager.Play("Season Start");

            isSummer = false;
            isFall = true;
            ruleText.text = "Era of Commerce! \n\n"
                    + "(1) Gain 0.25 move for every level-3.\n"
                    + "(2) Gain 0.5 move for every level-4.\n"
                    + "(3) Doubles for Yellow.";
            //ruleText.color = Color.yellow;
            Debug.Log("Summer has past and Fall has come.");
            if (player1.photonView.isMine)
            {
                StartCoroutine(CiweilizationDeal3());
            }
            player1.PlayerStartFall();
            if (player2)
            {
                player2.PlayerStartFall();
            }
            if (player3)
            {
                player3.PlayerStartFall();
            }
        }
        else if (turn == 10 && isFall == true)
        {
            //audioManager.StopPlaying("Fall Theme");
            //audioManager.Play("Winter Theme");
            audioManager.Play("Season Start");

            isFall = false;
            isWinter = true;
            ruleText.text = "Era of Science! \n\n"
                    + "(1) Gain 0.5 move for every level-4,\n"
                    + "doubles for Blue-4.\n"
                    + "(2) Gain 0.25 move for every blue-3.";
            //ruleText.color = Color.cyan;
            Debug.Log("Fall has past and winter has come.");
            if (player1.photonView.isMine)
            {
                StartCoroutine(CiweilizationDeal4());
            }
            player1.PlayerStartWinter();
            if (player2)
            {
                player2.PlayerStartWinter();
            }
            if (player3)
            {
                player3.PlayerStartWinter();
            }
        }

        turnText.text = "Turn" + " " + turn.ToString();
        
        /* Make players reset their moves, 
        * and possibly do some hero-unique things at the start of a big turn (i.e. round). */
        player1.PlayerStartRound();
        if (player2)
        {
            player2.PlayerStartRound();
        }
        if (player3)
        {
            player3.PlayerStartRound();
        }
    }

    /* Deal out the starting building cards according to season. */
    public void CiweilizationNextTurnDeal()
    {
        if (turn == 1)
        {
            StartCoroutine(CiweilizationDeal1());
        }
        else if (turn == 4)
        {
            StartCoroutine(CiweilizationDeal2());
        }
        else if (turn == 7)
        {
            StartCoroutine(CiweilizationDeal3());
        }
        else if (turn == 10)
        {
            StartCoroutine(CiweilizationDeal4());
        }
        else
        {
            Debug.Log("Error! Invalid turn number.");
        }
    }
    #endregion

    #region Miscellaneous Functions
    /* Return 1 or 0 based on the given boolean. */
    public int BoolToInt(bool b)
    {
        if (b)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    #endregion
}
