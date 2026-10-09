using System;
using UnityEngine;
using UnityEngine.Events;

public class ColliderTag : MonoBehaviour
{
    protected bool CollideWith(Collider other, Action func, Tag tag, UnityEvent eventToCall)
    {
        TagAll component = other.gameObject.GetComponent<TagAll>();
        if (component != null && component.MyTag == tag)
        {
            func();
            eventToCall.Invoke();
            return true;
        }
        return false;
    }

    protected bool CollideWithParam<Param>(Collider other, Action<Param> func, Tag tag, UnityEvent eventToCall, Param param)
    {
        TagAll component = other.gameObject.GetComponent<TagAll>();
        if (component != null && component.MyTag == tag)
        {
            func(param);
            eventToCall.Invoke();
            return true;
        }
        return false;
    }

}