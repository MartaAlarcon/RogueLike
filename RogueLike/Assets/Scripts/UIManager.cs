using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    private int totalCoins = 0;
    public void UpdateCoinUI()
    {
        coinText.text = totalCoins.ToString("D3");
    }
    public void AddCoin()
    {
        totalCoins++;
        UpdateCoinUI();
    }
}
