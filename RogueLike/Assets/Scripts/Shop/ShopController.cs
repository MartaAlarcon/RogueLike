using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private GameObject shopPrefab;
    private GameObject instantiatedPanel; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (shopPrefab != null)
            {


                instantiatedPanel = Instantiate(shopPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                instantiatedPanel.SetActive(true);

            }
            else
            {
                Debug.LogWarning("No se asignó el prefab del panel Shop en el Inspector.");
            }
        }
    }
}
