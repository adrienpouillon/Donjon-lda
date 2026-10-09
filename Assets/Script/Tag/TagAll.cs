using UnityEngine;

public enum Tag
{
    None,
    Player, 
    Allie,
    Enemy,
    Boss,
    Trap,
    Coin,//Not Use
    Count,
}

public class TagAll : MonoBehaviour
{
    [SerializeField] Tag _tag;

    public Tag MyTag
    {
        get
        {
            return _tag;
        }

        private set
        {
            _tag = value;
        }
    }

    void Reset()
    {
        MyTag = Tag.None;
    }
}
