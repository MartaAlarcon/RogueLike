using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterDamage : MonoBehaviour
{
    public int damage = 2; 
    private PharaoController poolController; 

    public void SetPoolController(PharaoController controller)
    {
        poolController = controller;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Character playerHealth = collision.gameObject.GetComponent<Character>();
            playerHealth.Hurt(damage);           
        }
        poolController.RegresarProyectilALaPool(gameObject);
    }
}
