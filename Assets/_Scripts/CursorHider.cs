using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHider : MonoBehaviour
{
    void Start()
    {
        // Hide the cursor
        Cursor.visible = false;

        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Optional: Press the Escape key to show and unlock the cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
