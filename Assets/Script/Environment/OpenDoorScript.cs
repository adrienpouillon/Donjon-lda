using UnityEngine;

public class OpenDoorScript : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] int _rankCoinRequired;

    public int rankCoinRequired => _rankCoinRequired;
}
