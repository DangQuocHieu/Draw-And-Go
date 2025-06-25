using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MissileBoost : MonoBehaviour, IPowerup
{
    [SerializeField] private float _boostForce = 10f;
    public void ApplyEffect(GameObject target)
    {
        Rigidbody2D targetRb = target.GetComponent<Rigidbody2D>();
        GetComponent<BoxCollider2D>().enabled = false;
        transform.parent = target.transform;
        transform.localPosition = Vector3.zero;
        StartCoroutine(MissileBoostRoutine(targetRb));
    }

    private IEnumerator MissileBoostRoutine(Rigidbody2D targetRb)
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            targetRb.AddRelativeForce(Vector2.right * _boostForce, ForceMode2D.Impulse);
        }
        

    }

}
