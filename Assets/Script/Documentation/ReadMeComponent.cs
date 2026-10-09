using NaughtyAttributes;
using UnityEngine;

public class ReadMeComponent : MonoBehaviour
{
    [SerializeField, TextArea(5, 10)]protected string _DocString;
}
