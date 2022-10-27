using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    Controller control;
    private void Awake()
    {
        control = new Controller();
    }
    private void OnEnable()
    {
        control.Gameplay.Enable();
        control.Gameplay.Move.performed += movePlayer;
        control.Gameplay.Move.canceled += movePlayer;
        control.Gameplay.Jump.started += jumpPlayer;
        control.Gameplay.Attack.started += attackplayer;
    }

    public void jumpPlayer(InputAction.CallbackContext obj)
    {
        FindObjectOfType<MoveUnity>().Jump();
    }

    private void movePlayer(InputAction.CallbackContext obj)
    {
        Vector2 moveDir = obj.ReadValue<Vector2>();
    }

    private void attackplayer(InputAction.CallbackContext obj)
    {
        //FindObjectOfType<MoveUnity>().Shoot();
    }

}
