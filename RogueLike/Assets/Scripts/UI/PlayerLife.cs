using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public int maxLife =  3;
    private int currentLife; 
    [SerializeField] private Image[] lifeImage;

    private void Start()
    {
        currentLife = maxLife;
        updateUI();
    }
    void updateUI()
    {
        for (int i = 0; i < lifeImage.Length; i++)
        {
            lifeImage[i].enabled = i < currentLife;
        }
        if (currentLife <= 0)
        {
            SceneManager.LoadScene("End");
        }
    }
    public void takeLife()
    {
        currentLife --;
        currentLife = Mathf.Clamp(currentLife, 0, maxLife);
        updateUI();
    }
    

}
