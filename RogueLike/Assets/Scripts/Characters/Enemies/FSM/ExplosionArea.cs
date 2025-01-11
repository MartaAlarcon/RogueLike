using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionArea : MonoBehaviour
{
    private Character player;
    public void Start()
    {
        player = FindObjectOfType<Character>(); 
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.Hurt(2f);
        }
    }
}
