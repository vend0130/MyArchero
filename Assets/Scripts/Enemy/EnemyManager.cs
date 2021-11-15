using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject _selectEnenmyPrefab;
    [SerializeField] private Transform _pool;

    private int _maxEnemy;
    private List<Enemy> _enemies = new List<Enemy>();
    
    public List<Enemy> Enemies => _enemies;
    public GameObject SelectEnemyPrefab => _selectEnenmyPrefab;

    private void Awake()
    {
        I.EnemyManager = this;
        foreach (Transform enemy in _pool)
        {
            _enemies.Add(enemy.GetComponent<Enemy>());
        }

        _maxEnemy = _enemies.Count;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        I.Ui.UpdateProgress(_maxEnemy, _enemies.Count);
        if (_enemies.Count == 0)
            I.Ui.EndGame();
    }
}
