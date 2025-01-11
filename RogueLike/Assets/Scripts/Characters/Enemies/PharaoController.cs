using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PharaoController : MonoBehaviour
{
    public Transform jugador; // Referencia al jugador
    public GameObject proyectilPrefab; // Prefab del proyectil
    public float velocidadDisparo = 5f; // Velocidad del disparo
    public float tiempoEntreDisparos = 2f; // Tiempo entre cada disparo
    public int poolSize = 5; // Tamaño inicial de la pool de proyectiles

    private float tiempoSiguienteDisparo = 0f; // Control del tiempo de disparo
    private Queue<GameObject> poolDeProyectiles; // Cola para almacenar los proyectiles

    void Start()
    {
        // Inicializa la pool de proyectiles
        poolDeProyectiles = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject proyectil = Instantiate(proyectilPrefab);
            proyectil.SetActive(false);

            // Configura la referencia al controlador en el proyectil
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
            // Busca automáticamente al jugador si no está asignado
            GameObject jugadorEncontrado = GameObject.FindGameObjectWithTag("Player");
            if (jugadorEncontrado != null)
            {
                jugador = jugadorEncontrado.transform;
            }
        }

        // Verifica si el tiempo actual permite disparar
        if (Time.time > tiempoSiguienteDisparo && jugador != null)
        {
            Disparar();
            tiempoSiguienteDisparo = Time.time + tiempoEntreDisparos;
        }
    }

    void Disparar()
    {
        // Asegura que el jugador esté presente
        if (jugador != null && poolDeProyectiles.Count > 0)
        {
            // Obtiene un proyectil de la pool
            GameObject proyectil = poolDeProyectiles.Dequeue();
            proyectil.SetActive(true);

            // Coloca el proyectil en la posición del enemigo
            proyectil.transform.position = transform.position;
            proyectil.transform.rotation = Quaternion.identity;

            // Calcula la dirección hacia el jugador
            Vector2 direccion = (jugador.position - transform.position).normalized;

            // Asigna la velocidad al proyectil
            Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direccion * velocidadDisparo;
            }
        }
    }

    public void RegresarProyectilALaPool(GameObject proyectil)
    {
        // Detiene el movimiento del proyectil y lo regresa a la pool
        Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        proyectil.SetActive(false);
        poolDeProyectiles.Enqueue(proyectil);
    }
}
