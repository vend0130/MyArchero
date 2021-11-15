using UnityEngine;

public class EnemyAnimationEvent: MonoBehaviour
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    public void Event(ActionType action)
    {
        _enemy.SetAnimation(ActionType.Idle);
    }
}
