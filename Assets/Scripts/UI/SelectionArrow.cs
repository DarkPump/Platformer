using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionArrow : UIManager
{
    [Header("References")]
    [SerializeField] private RectTransform[] options;
    [SerializeField] private InputManager inputManager;
    private RectTransform rect;
    public InputAction navigation;


    private int currentPosition;

    private void Awake() 
    {
        rect = GetComponent<RectTransform>();
        inputManager.EnableUIActionMap();
    }

    private void Start() 
    {
        inputManager.playerControls.UI.MoveUp.performed += ctx => MoveUp();
    }

    private void MoveUp()
    {
        ChangePosition(-1);
    }

    private void ChangePosition(int change)
    {
        currentPosition += change;

        if(currentPosition < 0)
            currentPosition = options.Length - 1;
        else if(currentPosition > options.Length - 1)
            currentPosition = 0;

        rect.position = new Vector2(rect.position.x, options[currentPosition].position.y);
    }
}

//Game manager sprawdzający czy jest koniec gry/początek/pauzy
