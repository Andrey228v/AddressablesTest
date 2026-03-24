using Assets._Scripts.Enemy;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class EasySpawner : MonoBehaviour
{
    [Min(0.1f)][SerializeField] private float _timeSpawn = 1f;
    [SerializeField] private EnemyView _objectPrefab;
    [SerializeField] private Material _material;
    [SerializeField] private Transform _parentContainer;

    private CancellationTokenSource _cts;

    private void Start()
    {
        StartSpawn();
    }

    private void OnDestroy()
    {
        // Отменяем цикл при уничтожении
        _cts?.Cancel();
        _cts?.Dispose();
    }

    private async void StartSpawn()
    {
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        try
        {
            while (!token.IsCancellationRequested)
            {

                if (this == null || this == false)
                {
                    Debug.Log("EasySpawner уничтожен, останавливаем спавн");
                    break;
                }

                EnemyView enemy = CreateObject();
                Vector3 position = GetSpawnPosition();
                enemy.gameObject.transform.position = position;
                enemy.gameObject.transform.parent = _parentContainer;

                await UniTask.Delay(TimeSpan.FromSeconds(_timeSpawn), cancellationToken: token);
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Спавн остановлен (это нормально)");
        }
    }

    private Vector3 GetSpawnPosition()
    {
        float yUp = 2f;

        Vector3 bounds = GetComponent<Collider>().bounds.extents;
        Vector3 positionS = transform.position;

        float x = UnityEngine.Random.Range(positionS.x - bounds.x, positionS.x + bounds.x);
        float y = gameObject.transform.position.y + yUp;
        float z = UnityEngine.Random.Range(positionS.z - bounds.z, positionS.z + bounds.z);

        Vector3 position = new Vector3(x, y, z);

        return position;
    }

    private EnemyView CreateObject()
    {
        return Instantiate(_objectPrefab);
    }


}
