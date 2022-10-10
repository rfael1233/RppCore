using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/GameModes/CollectCoins",fileName = "CollectCoinsGameModeSO")]
public class CollectCoinsGameMode : ScriptableObject, IGameMode<int>
{
    public int coinsToWin;

    public GameState gameState;
    
    
    public void UpdateWinState(int value)
    {
        
    }
}
