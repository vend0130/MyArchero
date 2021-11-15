using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
            I.Character.GetHit(_enemy.Damage);
    }
}
