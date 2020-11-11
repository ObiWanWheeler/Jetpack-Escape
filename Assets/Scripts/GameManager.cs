using UnityEngine;

public class GameManager : MonoBehaviour
{

    private void Start()
    {
        GameEvents.current.onLevelComplete += CompleteLevel;
    }
  
    private void CompleteLevel()
    {
        Debug.Log("Level complete");
        // go to next level
    }
}
