using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int playerScore = 0;
    public Text scoreUI;
    public GameObject gameOver;
    public GameObject gameEnd;
    public int totalEnemy = 3;
    public GameObject levelEnd;
    public GameObject leaderBoard;
    public Text leaderBoardScoreUI;
    public bool isWin = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // here we create a Score updating function 
    public void AddScore()
    {
        playerScore++;                                          //increment score whenever this function is call
        scoreUI.text = "Score: " + playerScore;                 //updating score UI on the game screen
        leaderBoardScoreUI.text = "" + playerScore;                //update leaderboard score

    }
    //this simple function will just display GameOver UI and restart and leaderboard button on the screen
    public void GameOver()
    {
        gameOver.SetActive(true);
    }
    //this function will be used to restart our current scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //this fucntion is used for game end when all enemy are dead then game is end 
    public void Enemy()
    {
        if (totalEnemy > 0)
        {
            totalEnemy--;
        }
        if (totalEnemy == 0)
        {
            levelEnd.SetActive(true);                   //display you win UI and restart and leaderboard
            isWin = true;                               //check to stop player and enemy movement
        }
    }
    // this will active leader board when user click on leaderboard button
    public void LeaderBoard()
    {
        leaderBoard.SetActive(true);
    }

}
