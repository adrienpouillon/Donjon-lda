using System;
using UnityEngine;
using static coinType;
using static UnityEngine.Rendering.DebugUI;

public class coinInventory : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] int _expMuchValue;
    [SerializeField] int _rankMuchValue;
    [SerializeField] int _ecuMuchValue;

    [SerializeField] bool _physicsProxyUseTrigger;
    [SerializeField] bool _physicsProxyUseCollider;

    [Header("Component Required")]
    [SerializeField] PhysicsProxy _physicsProxy;

    public event Action _onRecoltCoin;
    public event Action _onRecoltExpCoin;
    public event Action _onRecoltRankCoin;
    public event Action _onRecoltEcuCoin;
    public event Action _onRecoltMuchExpCoin;
    public event Action _onRecoltMuchRankCoin;
    public event Action _onRecoltMuchEcuCoin;

    int _expQuantity;
    int _rankQuantity;
    int _ecuQuantity;

    public int ExpQuantity => _expQuantity;
    public int RankQuantity => _rankQuantity;
    public int EcuQuantity => _ecuQuantity;

    void Awake()
    {
        _expQuantity = 0;
        _rankQuantity = 0;
        _ecuQuantity = 0;   
    }

    void Start()
    {
        if (_physicsProxyUseTrigger)
        {
            _physicsProxy.onTriggerStay += AddCoinTrigger;
        }

        if (_physicsProxyUseCollider)
        {
            _physicsProxy.onCollisionStay += AddCoinCollider;
        }
    }

    void OnDestroy()
    {
        if (_physicsProxyUseTrigger)
        {
            _physicsProxy.onTriggerEnter -= AddCoinTrigger;
        }

        if (_physicsProxyUseCollider)
        {
            _physicsProxy.onCollisionEnter -= AddCoinCollider;
        }
    }
    void AddCoinCollider(Collision other)
    {
        if (other.collider.attachedRigidbody == null)
        {
            return;
        }
        AddCoin(other.collider.attachedRigidbody.gameObject);
    }

    void AddCoinTrigger(Collider other)
    {
        if (other.attachedRigidbody == null)
        {
            return;
        }
        AddCoin(other.attachedRigidbody.gameObject);
    }

    void AddCoin(GameObject gameObj)
    {
        coinType type = gameObj.GetComponentInChildren<coinType>();
        if (type != null && type.Lock == false)
        {
            switch(type.type)
            {
                case TypeCoin.expCoin:
                    _expQuantity += type.Value;
                    _onRecoltExpCoin?.Invoke();
                    if(type.Value >= _expMuchValue)
                    {
                        _onRecoltMuchExpCoin?.Invoke();
                    }
                    break;
                case TypeCoin.rankCoin:
                    _rankQuantity += type.Value;
                    _onRecoltRankCoin?.Invoke();
                    if (type.Value >= _rankMuchValue)
                    {
                        _onRecoltMuchRankCoin?.Invoke();
                    }
                    break;
                case TypeCoin.ecuCoin:
                    _ecuQuantity += type.Value;
                    _onRecoltEcuCoin?.Invoke();
                    if (type.Value >= _ecuMuchValue)
                    {
                        _onRecoltMuchEcuCoin?.Invoke();
                    }
                    break;
                default:
                    break;
            }
            _onRecoltCoin?.Invoke();
            Destroy(gameObj);
        }   
    }
}
