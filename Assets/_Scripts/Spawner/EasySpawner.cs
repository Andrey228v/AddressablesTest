using Assets._Scripts.Enemy;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EasySpawner : MonoBehaviour
{
    [Min(0.1f)][SerializeField] private float _timeSpawn;
    [SerializeField] private EnemyView _objectPrefab;
    [SerializeField] private Material _material;

    private void Start()
    {
        StartSpawn();
    }

    private async void StartSpawn()
    {
        while (true)
        { 
            EnemyView enemy = CreateObject();
            Vector3 position = GetSpawnPosition();
            enemy.gameObject.transform.position = position;

            await UniTask.Delay(50);
        }
    }


    private Vector3 GetSpawnPosition()
    {
        float yUp = 2f;

        Vector3 bounds = GetComponent<Collider>().bounds.extents;
        Vector3 positionS = transform.position;

        float x = Random.Range(positionS.x - bounds.x, positionS.x + bounds.x);
        float y = gameObject.transform.position.y + yUp;
        float z = Random.Range(positionS.z - bounds.z, positionS.z + bounds.z);

        Vector3 position = new Vector3(x, y, z);

        return position;
    }

    private EnemyView CreateObject()
    {
        return Instantiate(_objectPrefab);
    }


}
