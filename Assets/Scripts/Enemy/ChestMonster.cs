using UnityEngine;

public class ChestMonster : Enemy
{
    private void Update()
    {
        if (_action != ActionType.Death && _action != ActionType.GetHit)
            CheckCharacterForAttack();
    }

    private void CheckCharacterForAttack()
    {
        Collider[] collider = Physics.OverlapBox(transform.position + transform.forward, Vector3.one * .5f, Quaternion.identity);
        for (int i = 0; i < collider.Length; i++)
        {
            if (collider[i].tag == "Character")
                Attack();
        }
    }

    public override void Attack()
    {
        if (_action == ActionType.GetHit || _action == ActionType.Attack)
            return;

        SetAnimation(ActionType.Attack);
    }
}
