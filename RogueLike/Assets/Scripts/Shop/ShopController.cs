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
                    // Instancia el prefab del panel en la posici�n de la c�mara
                    instantiatedPanel = Instantiate(shopPrefab, new Vector3(0, 0, 0), Quaternion.identity);

                    // Aseg�rate de que se active
                    instantiatedPanel.SetActive(true);
                }
            }
            else
            {
                Debug.LogWarning("No se asign� el prefab del panel Shop en el Inspector.");
            }
        }
    }
}
