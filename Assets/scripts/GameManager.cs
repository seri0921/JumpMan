using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public PlayerC player;
 
     private void Awake()
     {
         if(instance == null)
         {
             instance = this;
             DontDestroyOnLoad(this.gameObject); 
         }
         else
         {
             Destroy(this.gameObject);
         }
     }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("TestResultScene");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("スコアが加算された");
            ScoreManager.Instance.AddCurrentScore(100);
        }
        
    }
}
