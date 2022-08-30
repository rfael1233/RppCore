using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Start, Play, GameOver, Victory, Ending
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState gameState;
    
    
    
    [SerializeField]
    private GameObject playerAndCameraPrefab;

    [SerializeField] 
    private string locationToLoad;

    [SerializeField] 
    private string guiScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Initialization")
        {
            StartGameFromInitialization();
        }
        else
        {
            StartGameFromLevel();
        }
    }

    private void StartGameFromLevel()
    {
        SceneManager.LoadScene(guiScene, LoadSceneMode.Additive);
        Vector3 startPosition = GameObject.Find("PlayerStart").transform.position;

        Instantiate(playerAndCameraPrefab, startPosition, Quaternion.identity);
        gameState = GameState.Play;
    }

    private void StartGameFromInitialization()
    {
        SceneManager.LoadScene("Splash");
        gameState = GameState.Start;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        gameState = GameState.Start;
    }


    // Start is called before the first frame update
    public void StartGame()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(guiScene);
        //SceneManager.LoadScene(locationToLoad, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(locationToLoad, LoadSceneMode.Additive).completed += operation =>
        {
            Scene locationScene = default;
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                locationScene = SceneManager.GetSceneAt(i);
            }
            if (locationScene != default) SceneManager.SetActiveScene(locationScene);
            
            Vector3 startPosition = GameObject.Find("PlayerStart").transform.position;
            
            Instantiate(playerAndCameraPrefab, startPosition, Quaternion.identity);
            
        }; 
        gameState = GameState.Play;
        

        //Vector3 startPosition = GameObject.Find("PlayerStart").transform.position;
        //Instantiate(playerAndCameraPrefab, startPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CallVictory()
    {
        SceneManager.LoadScene("Victory", LoadSceneMode.Additive);
        gameState = GameState.Victory;
    }

    public void CallGameOver()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        gameState = GameState.GameOver;
    }

    public void LoadEnding()
    {
        SceneManager.LoadScene("Ending");
        gameState = GameState.Ending;
    }
}
