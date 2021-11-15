using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Weapon[] _weapons;
    
    private Enemy _nearEnemy;
    private int _currentWeapon;

    public Enemy NearEnemy => _nearEnemy;
    public Weapon Weapon => _weapons[_currentWeapon];

    private void Awake()
    {
        I.CharacterAttack = this;
    }

    private void Update()
    {
        FoundNearEnemy();
    }

    private void FoundNearEnemy()
    {
        List <Enemy> enemies = I.EnemyManager.Enemies;
        if (enemies.Count == 0)
        {
            if(_nearEnemy != null)
                SelectedEnemy();
            return;
        }

        Enemy newNearEnemy = _nearEnemy != null ? _nearEnemy : enemies[0];

        for (int i = 0; i < enemies.Count; i++)
        {
            float distanceForCurrentEnemy = Vector3.Distance(newNearEnemy.transform.position, _character.transform.position);
            float distanceForNextEnemy = Vector3.Distance(enemies[i].transform.position, _character.transform.position);
            if (distanceForCurrentEnemy > distanceForNextEnemy)
                newNearEnemy = enemies[i];
        }

        float distanceForNearEnemy = Vector3.Distance(newNearEnemy.transform.position, _character.transform.position);
        if ((_weapons[_currentWeapon].TypeWeapon == TypeWeapon.Melee && distanceForNearEnemy <= 1.3f)
            || _weapons[_currentWeapon].TypeWeapon == TypeWeapon.Ranged)
            SelectedEnemy(newNearEnemy);
        else if((_weapons[_currentWeapon].TypeWeapon == TypeWeapon.Melee && distanceForNearEnemy > 1.3f))
            SelectedEnemy();
    }

    private void SelectedEnemy(Enemy newNearEnemy = null)
    {
        if(_nearEnemy != null)
            _nearEnemy.Selected(false);

        if (newNearEnemy != null)
            newNearEnemy.Selected(true);

        _nearEnemy = newNearEnemy;

        if(_character.Action != ActionType.Move)
            Weapon.Attack();
    }

    public void SetActiveWeapon(int value)
    {
        _currentWeapon = value;
        _weapons[_currentWeapon].SetActiveModel();
    }
}
