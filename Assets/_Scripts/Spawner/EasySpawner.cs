using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EasySpawner : MonoBehaviour
{
    [Min(0.1f)][SerializeField] private float _timeSpawn = 1f;
    [SerializeField] private string _enemyKey = "Enemy"; // Addressables ключ
    [SerializeField] private Material _material;
    [SerializeField] private Transform _parentContainer;

    private CancellationTokenSource _cts;
    private GameObject _enemyPrefab;
    private AsyncOperationHandle<GameObject> _handleEnemy;
    private List<GameObject> _enemies = new();

    private async void Start()
    {
        bool loaded =  await LoadEnemyPrefab();

        if (loaded == false)
        {
            Debug.LogError("Не удалось загрузить префаб врага, спавнер отключён");
            enabled = false;
            return;
        }

        StartSpawn();
    }

    private void OnDestroy()
    {
        // Отменяем цикл при уничтожении
        _cts?.Cancel();
        _cts?.Dispose();

        foreach (var enemy in _enemies)
        {
            if (enemy != null)
                Destroy(enemy);
        }

        Addressables.Release(_handleEnemy);
    }

    private async UniTask<bool> LoadEnemyPrefab()
    {
        _handleEnemy = Addressables.LoadAssetAsync<GameObject>(_enemyKey);
        await _handleEnemy.Task;

        if (_handleEnemy.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Не удалось загрузить префаб: {_enemyKey}");
            return false;
        }

        _enemyPrefab = _handleEnemy.Result;
        return true;
    }

    private async UniTask StartSpawn()
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

                if (_enemyPrefab == null)
                {
                    Debug.LogError("Префаб не загружен, спавн невозможен");
                    break;
                }

                var enemy = CreateObject();
                _enemies.Add(enemy);
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

    private GameObject CreateObject()
    {
        return Instantiate(_enemyPrefab);
    }
}
