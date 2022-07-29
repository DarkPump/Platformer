using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform[] options;
    private RectTransform rect;

    private int currentPosition;

    private void Awake() 
    {
        rect = GetComponent<RectTransform>();
        //InputManager.instance.EnableUIActionMap();
    }

    private void Start() 
    {
        InputManager.instance.playerControls.UI.MoveUp.performed += ctx => MoveUp();
        InputManager.instance.playerControls.UI.MoveDown.performed += ctx => MoveDown();

        InputManager.instance.playerControls.UI.Interact.performed += ctx => Interact();

        rect.position = new Vector2(options[currentPosition].position.x - 10, options[currentPosition].position.y - 1.25f);
    }

    private void OnDestroy()
    {
        InputManager.instance.playerControls.UI.MoveUp.performed -= ctx => MoveUp();
        InputManager.instance.playerControls.UI.MoveDown.performed -= ctx => MoveDown();
        InputManager.instance.playerControls.UI.Interact.performed -= ctx => Interact();
    }

    //Zmiana pozycji o jedną w górę
    private void MoveUp()
    {
        ChangePosition(-1);
    }
    //Zmiana pozycji o jedną w dół
    private void MoveDown()
    {
        ChangePosition(1);
    }
    //Zmiana pozycji strzałki pieniądza
    private void ChangePosition(int change)
    {
        currentPosition += change;

        if(currentPosition < 0)
            currentPosition = options.Length - 1;
        else if(currentPosition > options.Length - 1)
            currentPosition = 0;

        rect.position = new Vector2(options[currentPosition].position.x - 10, options[currentPosition].position.y - 1.25f);
    }
    //Wybór opcji
    private void Interact()
    {
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}

//Game manager sprawdzający czy jest koniec gry/początek/pauzy
