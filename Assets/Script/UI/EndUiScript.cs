using System.ComponentModel.Design;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndUiScript : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] int _maxcoinObjectif;
    [SerializeField] string _deathText;
    [SerializeField] string _victoryText;

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
        _coin = GameObject.FindFirstObjectByType<coinInventory>();
        _deathText = "You are dead";
        _deathText = "You win";
    }

    void Update()
    {
        //is Reloading is here to not reset clock every frame if player death
        if (_isReloading == false)
        {
            if (_life.IsDead())
            {
                _text.color = Color.red;
                _text.text = _deathText;
                _isReloading = true; 
                _clock.ResetClock();
            }
            else if (_coin.EcuQuantity >= _maxcoinObjectif)
            {
                _text.color = Color.yellow;
                _text.text = _victoryText;
            }
            else _text.text = "";
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
