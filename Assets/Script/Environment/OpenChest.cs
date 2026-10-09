using UnityEngine;

public class OpenChest : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] coinType _coinType;

    public coinType CoinType
    {
        get => _coinType;
        set => _coinType = value;
    }

    void Reset()
    {
        _coinType = GetComponentInChildren<coinType>();
    }

    void Awake()
    {
        _coinType.Lock = true;
    }


}
