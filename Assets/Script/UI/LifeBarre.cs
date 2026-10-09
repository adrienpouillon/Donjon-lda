using UnityEngine;
using UnityEngine.UI;

public class LifeBarre : MonoBehaviour
{
    [Header("Component required")]
    [SerializeField] Slider _slider;
    [SerializeField] LifeComponent _life;

    void Reset()
    {
        _life = GetComponent<LifeComponent>();
    }

    void Start()
    {
        _life.OnUpdateLife += SliderUpdate;

        _slider.maxValue = _life.MaxPV;
        _slider.value = _life.CurrentPV;
    }

    void OnDestroy()
    {
        _life.OnUpdateLife -= SliderUpdate;
    }

    void SliderUpdate(int currentLife)
    {
        _slider.value = currentLife;
    }

}
