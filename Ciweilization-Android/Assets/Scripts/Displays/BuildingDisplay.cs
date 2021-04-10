using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDisplay : MonoBehaviour
{
    public string testName;
    public Sprite wonderSprite;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GameObject.Find("Card Zoom").GetComponent<SpriteRenderer>();
    }

    void OnMouseOver()
    {
        spriteRenderer.sprite = wonderSprite;
        Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {
        spriteRenderer.sprite = null;
        Debug.Log("Mouse is NOT over GameObject.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
