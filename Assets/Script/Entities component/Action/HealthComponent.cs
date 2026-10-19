using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("Component Required")]
    [SerializeField] coinInventory _coinInventory;
    [SerializeField] LifeComponent _life;

    private void Reset()
    {
        _coinInventory = GetComponent<coinInventory>();
        _life = GetComponent<LifeComponent>();
    }
    void Start()
    {
        _coinInventory._onRecoltExpCoin += _life.DefaultHealth;
        _coinInventory._onRecoltMuchExpCoin += _life.AllHealth;
    }

    void OnDestroy()
    {
        _coinInventory._onRecoltExpCoin -= _life.DefaultHealth;
        _coinInventory._onRecoltMuchExpCoin -= _life.AllHealth;
    }
}
