using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour, IPowerup
{
    private HashSet<GameObject> _usedTeleporter = new HashSet<GameObject>();
    [SerializeField] private Teleporter _destination;
    [SerializeField] private float _coolDown = 1f;

    public void ApplyEffect(GameObject target)
    {
        if (_usedTeleporter.Contains(target))
        {
            return;
        }

        _destination._usedTeleporter.Add(target);
        target.transform.position = _destination.transform.position;
    }



    void OnTriggerExit2D(Collider2D collision)
    {
        if(_usedTeleporter.Contains(collision.gameObject))
        _usedTeleporter.Remove(collision.gameObject);
    }
}
