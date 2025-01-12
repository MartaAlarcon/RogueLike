using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PharaoController : MonoBehaviour
{
    public Transform jugador;
    public GameObject projectilePrefab;
    public float velocidadDisparo = 8f;
    public float timeBetweenShot = 2f;
    public int poolSize = 5; 

    private float shotCooldown = 0f; 
    private Queue<GameObject> poolDeProyectiles; 

    void Start()
    {
        poolDeProyectiles = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject proyectil = Instantiate(projectilePrefab);
            proyectil.SetActive(false);
            ShooterDamage shooterDamage = proyectil.GetComponent<ShooterDamage>();
            if (shooterDamage != null)
            {
                shooterDamage.SetPoolController(this);
            }

            poolDeProyectiles.Enqueue(proyectil);
        }
    }

    void Update()
    {
        if (jugador == null)
        {
            GameObject jugadorEncontrado = GameObject.FindGameObjectWithTag("Player");
            if (jugadorEncontrado != null)
            {
                jugador = jugadorEncontrado.transform;
            }
        }
        if (Time.time > shotCooldown && jugador != null)
        {
            Shoot();
            shotCooldown = Time.time + timeBetweenShot;
        }
    }

    void Shoot()
    {
        if (jugador != null && poolDeProyectiles.Count > 0)
        {
            GameObject proyectil = poolDeProyectiles.Dequeue();
            proyectil.SetActive(true);
            proyectil.transform.position = transform.position;
            proyectil.transform.rotation = Quaternion.identity;
            Vector2 direccion = (jugador.position - transform.position).normalized;
            Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direccion * velocidadDisparo;
            }
        }
    }

    public void RegresarProyectilALaPool(GameObject proyectil)
    {
        Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        proyectil.SetActive(false);
        poolDeProyectiles.Enqueue(proyectil);
    }
}
