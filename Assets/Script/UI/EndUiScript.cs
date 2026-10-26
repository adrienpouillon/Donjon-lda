using System.ComponentModel.Design;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.SceneManagement;
using static coinType;

public class EndUiScript : MonoBehaviour
{
    [Header("Config")]
    [Header("Death")]
    [SerializeField] Color _colordeath;
    [SerializeField] string _deathText;

    [Header("Win")]
    [SerializeField] int _coinWinObjectif;
    [SerializeField] TypeCoin _typeCoinWinObjectif;
    [SerializeField] Color _colorWinObjectif;
    [SerializeField] string _winText;

    [Header("Win 100%")]
    [SerializeField] int _ecuCoinWinThousandPercentObjectif;
    [SerializeField] int _rankCoinWinThousandPercentObjectif;
    [SerializeField] int _expCoinWinThousandPercentObjectif;
    [SerializeField] Color _colorWinThousandPercentObjectif;
    [SerializeField] string _winThousandPercentText;

    
    
    

    [Header("Text Dependances")]
    [SerializeField] TextMeshProUGUI _text;

    [Header("Component Required")]
    [SerializeField] LifeComponent _life;
    [SerializeField] coinInventory _coin;
    [SerializeField] Clock _clock;

    bool _isReloading;

    void Awake()
    {
        _text.text = "";        
    }
    void Start()
    {
        _isReloading = false;
    }
    void Reset()
    {
        _coin = FindFirstObjectByType<coinInventory>();
        _deathText = "You are dead";
        _winText = "You win";
        _winThousandPercentText = "You win";
    }

    void Update()
    {
        //is Reloading is here to not reset clock every frame if player death
        if (_isReloading == false)
        {
            if (_life.IsDead())
            {
                _text.color = _colordeath;
                _text.text = _deathText;
                _isReloading = true;
                _clock.ResetClock();
            }
            else 
            {
                int coinWin = 0;
                int ecuCoin = _coin.EcuQuantity;
                int expCoin = _coin.ExpQuantity;
                int rankCoin = _coin.RankQuantity;
                switch (_typeCoinWinObjectif)
                {
                    case TypeCoin.ecuCoin:
                        coinWin = ecuCoin;
                        break;
                    case TypeCoin.expCoin:
                        coinWin = expCoin;
                        break;
                    case TypeCoin.rankCoin:
                        coinWin = rankCoin;
                        break;
                }
                if (ecuCoin >= _ecuCoinWinThousandPercentObjectif && expCoin >= _expCoinWinThousandPercentObjectif && rankCoin >= _rankCoinWinThousandPercentObjectif)
                {
                    _text.color = _colorWinThousandPercentObjectif;
                    _text.text = _winThousandPercentText;
                    Debug.Log("100%");
                }
                else if (coinWin >= _coinWinObjectif)
                {
                    _text.color = _colorWinObjectif;
                    _text.text = _winText;
                }
                else
                {
                    _text.text = "";
                }
            }
        }
        else
        {
            //reload the scene to restart
            if (_clock.IsFinish())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
