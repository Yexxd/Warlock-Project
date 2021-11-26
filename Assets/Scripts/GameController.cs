using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        Texture2D cursor = Resources.Load("NormalCursor") as Texture2D;
        Cursor.SetCursor(cursor, Vector3.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
