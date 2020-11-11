using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onPackFilled;
    public void PackFilled()
    {
        onPackFilled?.Invoke(); 
    }

    public event Action onLevelComplete;

    public void LevelComplete()
    {
        onLevelComplete?.Invoke();
    }
}
