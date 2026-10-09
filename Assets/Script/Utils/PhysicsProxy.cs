using System;
using UnityEngine;

public class PhysicsProxy : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] bool _useDebug;

    void Reset()
    {
        _useDebug = false;
    }

    //================== Trigger ===========================

    public event Action<Collider> onTriggerEnter;
    public event Action<Collider> onTriggerStay;
    public event Action<Collider> onTriggerExit;

    void OnTriggerEnter(Collider other)
    {
        if (_useDebug) { Debug.Log("Trigger Enter"); }

        onTriggerEnter?.Invoke(other);

    }

    void OnTriggerStay(Collider collision)
    {
        if (_useDebug) { Debug.Log("Trigger Stay"); }

        onTriggerStay?.Invoke(collision);

    }

    void OnTriggerExit(Collider collision)
    {
        if (_useDebug) { Debug.Log("Trigger Exit"); }

        onTriggerExit?.Invoke(collision);
    }

    //================== Collision ===========================

    public event Action<Collision> onCollisionEnter;
    public event Action<Collision> onCollisionStay;
    public event Action<Collision> onCollisionExit;
    
    void OnCollisionEnter(Collision collision)
    {
        if (_useDebug) { Debug.Log("collison Enter"); }

        onCollisionEnter?.Invoke(collision);

    }

    void OnCollisionStay(Collision collision)
    {
        if (_useDebug) { Debug.Log("collison Stay"); }

        onCollisionStay?.Invoke(collision);

    }

    void OnCollisionExit(Collision collision)
    {
        if (_useDebug) { Debug.Log("collison Exit"); }

        onCollisionExit?.Invoke(collision);
    }
   
}
