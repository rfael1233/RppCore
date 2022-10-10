using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2D : MonoBehaviour
{
    public float initianTime;
    public int coins = 0;
    public int coletaveis = 0;
    
    public float moveSpeed;
    public float maxVelocity;
    
    public float rayDistance;
    public LayerMask groundLayer;
    public float jumpForce;

    private GameInput _gameInput;
    private PlayerInput _playerInput;
    private Camera _mainCamera;
    private Rigidbody2D _rigidbody;

    private Vector2 _moveInput;
    private bool _isGrounded;
    private float _timeRemaining;

    private void Start()
    {
        _timeRemaining = initianTime;
    }


    private void OnEnable()
    {
        // inicialização
        _gameInput = new GameInput();

        // referencias dos componentes no mesmo objeto da unity
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();

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

        if (obj.action.name.CompareTo(_gameInput.Gameplay.Jump.name) == 0)
        {
            if(obj.performed) Jump();
        }
    }

    private void Move()
    {




        _moveInput.y = 0;
        _rigidbody.AddForce(_moveInput * moveSpeed * Time.fixedDeltaTime);

    }

    private void FixedUpdate()
    {
        Move();
        LimitVelocity();
    }

    private void LimitVelocity()
    {
        // pegar a velocidade do player
        Vector2 velocity = _rigidbody.velocity;

        //checar se a velocidade esta dentro dos limitis nos diferentes eixo
        // limitando o eixo x usando ifs, abs
        if (Mathf.Abs(velocity.x) > maxVelocity) velocity.x = Mathf.Sign(velocity.x) * maxVelocity;
       
        
        _rigidbody.velocity = velocity;
    }
    //jogador pulando...
    private void Jump()
    {
        if(_isGrounded)_rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        
    }
    private void CheckGround()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer);
        _isGrounded = hit2D.collider != null;
        

    }

    private void Update()
    {
        _timeRemaining -= Time.deltaTime;
        CheckGameOver();
        CheckGround();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.yellow);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coins++;
            PlayerObserverManager.OnPlayerCoinsChanged(coins);
            CheckVictoty();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("ObjColetaveis"))
        {
            coletaveis++;
            PlayerObserverManager.OnPlayerColetaveisChanged(coletaveis);
            Destroy(other.gameObject);
        }
        

    }
    
    
    private void CheckVictoty()
    {
        if (coins >= 10)
        {
            if (GameManager.Instance.gameState != GameState.Victory)
            {
                GameManager.Instance.CallVictory();
            }
            
        }
    }

    private void CheckGameOver()
    {
        if (_timeRemaining <= 0)
        {
            if (GameManager.Instance.gameState != GameState.GameOver)
            {
                GameManager.Instance.CallGameOver();
                //GameManager.Instance.LoadEnding();
            }
        }
    }
}

