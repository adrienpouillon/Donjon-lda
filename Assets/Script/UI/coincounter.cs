using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CoinCounter : MonoBehaviour
{

    [Header("Text Dependances")]
    [SerializeField] TextMeshProUGUI _expTextMeshPro;
    [SerializeField] TextMeshProUGUI _rankTextMeshPro;
    [SerializeField] TextMeshProUGUI _ecuTextMeshPro;

    [Header("Script Dependances")]
    [SerializeField]  coinInventory _coinInventory;

    void Reset()
    {

        _expTextMeshPro = transform.Find("Exp Text").GetComponent<TextMeshProUGUI>();
        _rankTextMeshPro = transform.Find("Rank Text").GetComponent<TextMeshProUGUI>();
        _ecuTextMeshPro = transform.Find("Ecu Text").GetComponent<TextMeshProUGUI>();
        _coinInventory = GameObject.FindFirstObjectByType<coinInventory>();
    }

    void Start()
    {
        _coinInventory._onRecoltCoin += UpdateCounter;
    }


    void OnDestroy()
    {
        _coinInventory._onRecoltCoin -= UpdateCounter;
    }

    void UpdateCounter()
    {
        _expTextMeshPro.text = _coinInventory.ExpQuantity.ToString();
        _rankTextMeshPro.text = _coinInventory.RankQuantity.ToString();
        _ecuTextMeshPro.text = _coinInventory.EcuQuantity.ToString();
    }
}
