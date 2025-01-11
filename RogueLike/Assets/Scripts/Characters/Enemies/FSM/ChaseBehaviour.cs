using UnityEngine;

public class ChaseBehaviour : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Chase(Transform target, Transform self)
    {
        if (target != null)
        {
            // Moverse hacia el objetivo (jugador)
            _rb.velocity = (target.position - self.position).normalized * Speed;
        }
    }

    public void Run(Transform target, Transform self)
    {
        // Retroceder (si es necesario)
        _rb.velocity = (self.position - target.position).normalized * Speed;
    }

    public void StopChasing()
    {
        _rb.velocity = Vector2.zero;
    }
}
