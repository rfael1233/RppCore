using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    
    private GameInput _gameInput;
    private PlayerInput _playerInput;
    private Camera _mainCamera;
    private Rigidbody _rigidbody;

    private Vector2 _moveInput;

    private void OnEnable()
    {
        // inicialização
        _gameInput = new GameInput();

        // referencias dos componentes no mesmo objeto da unity
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();

        //referencia para a camera main 
        _mainCamera = Camera.main;

        // delegar do action triggered no player input
        _playerInput.onActionTriggered += OnActionTriggered;
    }

    private void OnDisable()
    {
        // retirando a atribuição ao delegante
        _playerInput.onActionTriggered -= OnActionTriggered;
    }

    private void OnActionTriggered(InputAction.CallbackContext obj)
    {
        //comparando o nome action que esta chegando com o nome action de movimento
        if (obj.action.name.CompareTo(_gameInput.Gameplay.Movement.name) == 0)
        {
            //atribuir ao moveInput i valoe preveniente do input do jogadir como um vector 2
            _moveInput = obj.ReadValue<Vector2>();

        }
    }

    private void Move()
    {
        //calcula o movimento no eixo da camera para o movimenti frete/tras
        
        Vector3 moveVertical = _mainCamera.transform.forward * _moveInput.y;
        
        //calcula o movimento no eixo da camera para o movimenti esquerda/direita
        Vector3 moveHorizontal = _mainCamera.transform.right * _moveInput.x;
        
        //Adicionar a força no objeto atraves dp rigibody, com intensidade definida por moveSpeed   
        _rigidbody.AddForce((_mainCamera.transform.forward * _moveInput.y + 
                             _mainCamera.transform.right * _moveInput.x) * 
                            moveSpeed * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        Move();
    }
}
