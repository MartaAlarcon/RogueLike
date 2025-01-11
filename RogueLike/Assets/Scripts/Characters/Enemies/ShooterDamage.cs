using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterDamage : MonoBehaviour
{
    public int damage = 2; // Daño que hace el proyectil
    private PharaoController poolController; // Referencia al controlador del enemigo para regresar proyectiles

    // Método para inicializar la referencia al controlador
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

        // Siempre devuelve el proyectil a la pool, sin importar con qué colisiona
        if (poolController != null)
        {
            poolController.RegresarProyectilALaPool(gameObject);
        }
    }
}
