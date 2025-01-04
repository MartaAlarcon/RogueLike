using System.Collections;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private float damageAmount = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detecta enemigos que tienen el script EnemyHealthFSM
        EnemyHealthFSM enemyHealthFSM = collision.GetComponent<EnemyHealthFSM>();
        if (enemyHealthFSM != null)
        {
            enemyHealthFSM.TakeDamage(damageAmount);
            return; // Si se encontró EnemyHealthFSM, no sigue buscando
        }

        // Detecta enemigos que tienen el script EnemyHealth
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damageAmount);
            enemyHealth.GetKnockedBack();
        }
    }
}
