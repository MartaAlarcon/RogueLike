using System.Collections;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;

        EnemyHealthFSM enemyHealthFSM = collision.GetComponent<EnemyHealthFSM>();
        if (enemyHealthFSM != null)
        {
            enemyHealthFSM.Hurt(damageAmount);
            return;
        }

        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.Hurt(damageAmount);
            enemyHealth.GetKnockedBack();
        }
    }
}
