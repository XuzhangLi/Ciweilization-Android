using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCursor : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D buildCursor;
    public int xCursorOffset;
    public int yCursorOffset;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void OnMouseEnter()
    {
        Cursor.SetCursor(buildCursor, new Vector2(xCursorOffset, yCursorOffset), CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(defaultCursor, new Vector2(xCursorOffset, yCursorOffset), CursorMode.Auto);
    }
}
