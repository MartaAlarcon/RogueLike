using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public void CloseButton()
    {
        Destroy(gameObject); // Destruye el panel instanciado
    }
}
