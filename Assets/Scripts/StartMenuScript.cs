using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    public Text highScoreText;
     void Start(){
         if(PlayerPrefs.GetString("HIGHSCORENAME") != ""){
          highScoreText.text = " Meilleur Score " + " " + PlayerPrefs.GetString("HIGHSCORENAME")+ " " + PlayerPrefs.GetInt ("RECORD") +"\n" ;   
         }
     }
 public void QuitGame(){
      Application.Quit ();
  }
  public void StartGame(){
      SceneManager.LoadScene ("main");
  }
}
