using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl2Button : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level2");
    }
}
