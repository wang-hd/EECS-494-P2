using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void LoadPuzzle()
    {
        SceneManager.LoadScene("Puzzle_1");
    }
    public void LoadTest()
    {
        SceneManager.LoadScene("Test");
    }
    public void Quit() 
    {
        Application.Quit();
    }
}
