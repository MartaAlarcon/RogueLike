using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    public static FinalDoor instance;


    public void Start()
    {
        instance = this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Key.instance.isKeyUnlocked)
            {
                FinalBoss.instance.endGame = true;
                gameObject. SetActive(false);
            }
        }
    }  


}
