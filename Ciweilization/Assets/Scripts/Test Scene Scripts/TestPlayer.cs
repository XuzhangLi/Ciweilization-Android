using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPlayer : Photon.MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject PlayerCamera;
    public SpriteRenderer sr;
    public Text PlayerNameText;

    public float moveSpeed;
    public bool isGrounded;
    public float jumpForce;

    [HideInInspector] public TestGameManager testGameManager;
    [HideInInspector] public AudioManager audioManager;

    private void Awake()
    {

    }

    private void Start()
    {

        audioManager = FindObjectOfType<AudioManager>();

        testGameManager = FindObjectOfType<TestGameManager>();

    }

    private void Update()
    {
        if (photonView.isMine)
        {
            CheckInput();
        }
    }
    private void CheckInput()
    {
        var move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        this.gameObject.transform.position += move * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.A))
        {
            photonView.RPC("FlipTrue", PhotonTargets.AllBuffered);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            photonView.RPC("FlipFalse", PhotonTargets.AllBuffered);
        }
    }

    [PunRPC]
    public void FlipTrue()
    {
        sr.flipX = true;
    }

    [PunRPC]
    public void FlipFalse()
    {
        sr.flipX = false;
    }
}

