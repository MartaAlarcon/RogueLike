using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Prefab del enemigo
    [SerializeField] private int maxEnemiesToSpawn = 5; // N�mero m�ximo de enemigos a spawnnear
    [SerializeField] private float chanceOfSpawning = 0.7f; // Probabilidad de que el spawner genere enemigos (0 a 1)
    [SerializeField] private Transform spawnLocation; // Transform de la posici�n donde se generar�n los enemigos

    void Start()
    {
        // Genera un n�mero aleatorio de enemigos al iniciar el juego
        int enemiesToSpawn = GetRandomEnemyCount();

        // Genera los enemigos en base a la cantidad calculada
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }
    }

    private int GetRandomEnemyCount()
    {
        // Determina si se generar�n enemigos en este spawner
        float randomChance = Random.value; // Genera un n�mero aleatorio entre 0 y 1
        if (randomChance <= chanceOfSpawning)
        {
            // Si la probabilidad es correcta, genera entre 1 y maxEnemiesToSpawn enemigos
            return Random.Range(1, maxEnemiesToSpawn + 1); // Asegura que al menos 1 enemigo sea generado
        }
        else
        {
            // Si la probabilidad falla, no genera enemigos
            return 0;
        }
    }

    private void SpawnEnemy()
    {
        // Si no se ha asignado un punto de spawn, no hacemos nada
        if (spawnLocation != null)
        {
            // Genera los enemigos en la posici�n del GameObject vac�o
            Instantiate(enemyPrefab, spawnLocation.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No se ha asignado una ubicaci�n de spawn.");
        }
    }
}
