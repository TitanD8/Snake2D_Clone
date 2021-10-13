
using UnityEngine;

public static class GameManager 
{
    public static int score = 0;

    public static void UpdateScore()
    {
        score = score + 10;
    }

    public static void ClearScore()
    {
        score = 0;
    }
}
