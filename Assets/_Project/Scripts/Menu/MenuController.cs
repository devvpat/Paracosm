using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour 
{

    private float _bgmFadeOutTime =.5f;

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1"); //load level 1 scene
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit(); //Close the game
    }

}
