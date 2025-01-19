using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    public int price;
  
    public void ClickItem()
    {
        if (UIManager.Instance.totalCoins >= price)
        {
            BuyItems(price);
            Key.instance.UnlockKey();
        }
    }

    void BuyItems(int price)
    {
        UIManager.Instance.SpendCoins(price);       
    }

    
}
