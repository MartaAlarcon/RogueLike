using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBoss : MonoBehaviour
{
    public int diedEnemies;
    public bool endGame;
    public static FinalBoss instance;

    public void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (endGame && diedEnemies >= 25)
        {
            SceneManager.LoadScene("Win");

        }
    }

}
