using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _carPrefab;
    [SerializeField] private Transform _spawnPosition;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_carPrefab, _spawnPosition.position, Quaternion.identity, null); 
        }
    }
}
