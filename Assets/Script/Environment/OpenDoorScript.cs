using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class OpenDoorScript : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] int _rankCoinRequired;

    [Header("Component Required")]
    [SerializeField] GameObject _canvaDoor;
    [SerializeField] TextMeshProUGUI _textForward;
    [SerializeField] TextMeshProUGUI _textBackward;

    public int RankCoinRequired => _rankCoinRequired;

    void Start()
    {
        CanvasDoorSetActive(true);
        string stringRankCoin = _rankCoinRequired.ToString();
        _textForward.text = stringRankCoin;
        _textBackward.text = stringRankCoin;
    }

    public void CanvasDoorSetActive(bool active)
    {
        _canvaDoor.SetActive(active);
    }
}
