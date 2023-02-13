using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int lives;
    public int score;
    public Text livesText;
    public Text scoreText;
    public Text highScoreText;
    public InputField highscoreTnput;
    public bool gameOver;
    public GameObject gameOverPanel;
    public GameObject loadLevelPanel;
    public int numberOfBricks;
    public Transform[] levels;
    public int currentLevelIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateLives(int changeInLives){
        lives += changeInLives;

        if(lives <= 0){
            lives = 0;
            GameOver ();
        }

        livesText.text = "lives: " + lives;
    }
    public void UpdateScore(int points){
        score += points;

        scoreText.text = "Score: " + score;
    }
    public void UpdateNumberOfBricks(){
           numberOfBricks--;
           if(numberOfBricks <= 0) {
               if(currentLevelIndex >= levels.Length - 1){
                   GameOver ();
               }  
               else{
                   loadLevelPanel.SetActive(true);
                   loadLevelPanel.GetComponentInChildren<Text>().text = " Niveau " + ( currentLevelIndex + 2 );
                   gameOver = true;
                   Invoke("LoadLevel",2f);
               }            
           }
    }
    void LoadLevel(){
     currentLevelIndex++;
     Instantiate(levels[currentLevelIndex],Vector2.zero,Quaternion.identity);        
     numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
     gameOver = false;
     loadLevelPanel.SetActive(false);
    }

    void GameOver(){
         gameOver= true;
         gameOverPanel.SetActive (true);
         int HighScore = PlayerPrefs.GetInt("RECORD");
         if (score > HighScore){
             PlayerPrefs.SetInt("RECORD",score);

             highScoreText.text = " Nouveau Record! " + "\n" + "Entrez Votre Nom Ci dessous";
             highscoreTnput.gameObject.SetActive (true);
         }else {
             highScoreText.text = PlayerPrefs.GetString ("HIGHSCORENAME") + " A Pour Record: " + HighScore + "\n" + "Pouvez vous le battre?";
         }
    }
    public void NewHighScore(){
       string highScoreName = highscoreTnput.text;
       PlayerPrefs.SetString("HIGHSCORENAME" , highScoreName);
       highscoreTnput.gameObject.SetActive (false);
       highScoreText.text = " Felicitation " + highScoreName + "\n" + " Votre Nouveau Score Est De " + score;
    } 

    public void PlayAgainButton(){
      SceneManager.LoadScene ("main");
    }

    public void Quit(){
        SceneManager.LoadScene ("Start Menu");
        Application.Quit ();
    }
}
