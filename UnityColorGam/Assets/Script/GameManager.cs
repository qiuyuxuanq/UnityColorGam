using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float TotalScore = 0f;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(float Carvalue)
    {
        TotalScore += Carvalue;
        Debug.Log("Total Score: " + TotalScore);
    }
}
