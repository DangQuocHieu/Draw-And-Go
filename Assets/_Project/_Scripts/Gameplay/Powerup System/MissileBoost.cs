using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MissileBoost : MonoBehaviour, IPowerup
{
    [SerializeField] private PhysicsMaterial2D _boostMaterial;
    [SerializeField] private float _speedChange = 3000f;
    public void ApplyEffect(GameObject target)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        transform.parent = target.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        target.GetComponent<CarMovement>().OnMissileBoost(_boostMaterial, _speedChange);
        
    }


}
