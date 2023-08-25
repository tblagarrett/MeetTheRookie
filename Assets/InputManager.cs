using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;

    private PlayerInput.OnFootActions onFoot;

    private CharacterControl control; 
    private Player_Interaction interactor; 
    // Start is called before the first frame update

    public static event Action<InputActionMap> actionMapChange;
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        control = GetComponent<CharacterControl>();
        interactor = GetComponent<Player_Interaction>();
        onFoot.Interact.performed += ctx => interactor.InteractionCheck();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        control.HandleMovement(onFoot.Movement.ReadValue<Vector2>());
    }
    private void OnEnable(){
        onFoot.Enable();
    }
    private void OnDisable(){
        onFoot.Disable();
    }

    public void SwitchActionMap(string map){
        switch(map){
            case "OnFoot":
                ToggleActionMap(playerInput.OnFoot);
                break;
            case "UI":
                ToggleActionMap(playerInput.UI);
                break;
            default:
                ToggleActionMap(playerInput.OnFoot);
                break;
        }
    }
    public void ToggleActionMap(InputActionMap actionMap){
        if(actionMap.enabled) return;
        playerInput.Disable();
        actionMapChange?.Invoke(actionMap);
        actionMap.Enable();
    }
}
