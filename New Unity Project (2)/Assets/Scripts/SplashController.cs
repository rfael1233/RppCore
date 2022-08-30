using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashController : MonoBehaviour
{
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.LoadMenu();
        
    }
}
