using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VictoryController : MonoBehaviour
{
    
    void Start()
    {
        Time.timeScale = 0f;

    }
    
    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            GameManager.Instance.StartGame();
        }
        
    }
}
