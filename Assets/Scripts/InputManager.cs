using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerControls playerControls;

    private void Awake() 
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable() 
    {
        EnableInput();
    }

    private void OnDisable() 
    {
        DisableInput();
    }

    public void EnableInput()
    {
        playerControls.Enable();
    }

    public void DisableInput()
    {
        playerControls.Disable();
    }

    public void EnableUIActionMap()
    {
        DisableAllActionMap();
        playerControls.UI.Enable();
    }

    public void EnablePlayerActionMap()
    {
        DisableAllActionMap();
        playerControls.Player.Enable();
    }

    public void DisableAllActionMap()
    {
        playerControls.UI.Disable();
        playerControls.Player.Disable();
    }
    //tylko metody w enablu
}

