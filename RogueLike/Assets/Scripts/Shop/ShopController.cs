using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private GameObject shopPrefab; // Referencia al prefab del panel Shop
    private GameObject instantiatedPanel; // Para guardar la instancia del panel

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (shopPrefab != null)
            {
                if (instantiatedPanel == null)
                {
                    // Instancia el prefab del panel en la posición de la cámara
                    instantiatedPanel = Instantiate(shopPrefab, new Vector3(0, 0, 0), Quaternion.identity);

                    // Asegúrate de que se active
                    instantiatedPanel.SetActive(true);
                }
            }
            else
            {
                Debug.LogWarning("No se asignó el prefab del panel Shop en el Inspector.");
            }
        }
    }
}
