using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    public int price;
    //[SerializeField] GameObject item;
    [SerializeField] List<InventorySlot> inventorySlots; // Lista de los slots del inventario
    public int position;

    public void Start()
    {
        inventorySlots = new List<InventorySlot>(FindObjectsOfType<InventorySlot>());
    }

    public void ClickItem()
    {
        if (UIManager.Instance.totalCoins >= price)
        {
            BuyItems(price);
        }
    }

    void BuyItems(int price)
    {
        UIManager.Instance.SpendCoins(price);
        //item.SetActive(true);
        inventorySlots[position].isUnlocked = true;
        
    }
}
