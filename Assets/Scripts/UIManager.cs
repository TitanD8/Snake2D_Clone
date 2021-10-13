using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// (Needs fixing, scoring is not working) 
public class UIManager : MonoBehaviour
{
    
    //public GameManager gm;
    public Text scoreText;

    private void Start()
    {
        scoreText.text = "0";
    }

    private void Update()
    {
        scoreText.text = GameManager.score.ToString();
    }
}
