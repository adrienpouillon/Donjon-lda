using UnityEngine;

public class coinType : MonoBehaviour
{

    [System.Serializable]
    public enum TypeCoin
    {
        expCoin,
        rankCoin,
        ecuCoin
    }

    [Header("Config")]
    [SerializeField] TypeCoin _typePiece;
    [SerializeField] int _quantity;
    [SerializeField] bool _lock;

    [Header("Component Optionnal")]
    [SerializeField] Clock _clock;

    public TypeCoin type => _typePiece;
    public bool Lock
    {
        get { return _lock;}
        set 
        {
            if (_clock != null)
            {
                if (value)
                {
                    _clock.ResetClock();
                    _clock.OnFinish += SetLockTrue;
                }
                else
                {
                    _clock.ResetClock();
                    _clock.OnFinish += SetLockfalse;
                }
            }
            else
            {
                _lock = value;
            }
        }
    }

    public int Value => _quantity;

    void Reset()
    {
        _quantity = 1;
        _lock = false;
    }

    void Awake()
    {
        if (_clock == null)
        {
            _clock = null;
        }
    }

    void SetLockTrue()
    {
        _lock = true;
        _clock.OnFinish -= SetLockTrue;
    }

    void SetLockfalse()
    {
        _lock = false;
        _clock.OnFinish -= SetLockfalse;
    }

}
