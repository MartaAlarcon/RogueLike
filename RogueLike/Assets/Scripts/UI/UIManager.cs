using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI coinText;
    public int totalCoins = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Multiple instances of UIManager found. Destroying the duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void UpdateCoinUI()
    {
        coinText.text = totalCoins.ToString("D3");
    }

    public void AddCoin()
    {
        totalCoins++;
        UpdateCoinUI();
    }

    public void SpendCoins(int amount)
    {
        if (totalCoins >= amount)
        {
            totalCoins -= amount;
            UpdateCoinUI();
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }
}
