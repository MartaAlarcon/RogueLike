using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;

    private void Update()
    {
        MoveProjectile();
    }
    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Walls"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = Vector2.zero;

            Spawner.Instance.ReturnToPool(this.gameObject);
        }
    }

}

