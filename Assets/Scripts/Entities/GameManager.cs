using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    protected static GameManager instance;
    public static GameManager Instance 
    {
        get 
        {
            if (instance != null)
            {
                GameManager obj = FindAnyObjectByType<GameManager>();
                if (obj != null)
                    instance = obj;
                else
                {
                   GameObject obj2 = new GameObject("GameManager");
                    obj2.AddComponent<GameManager>();
                    instance = obj2.GetComponent<GameManager>();
                }
                    
            }
            return instance; 
        }
    }
    
    void Start()
    {
        
        if (instance == null)
            instance = this;
    }


    public int Score { get; set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("¾²·¯Áø ÇÉÀÇ °¹¼ö : "+Score);
        }
    }
}
