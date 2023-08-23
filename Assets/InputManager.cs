using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;

    private PlayerInput.OnFootActions onFoot;

    private CharacterControl control; 
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        control = GetComponent<CharacterControl>();
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
}
