using UnityEngine;
using UnityEngine.AI;

public class Beholder : Enemy
{
    [SerializeField] private NavMeshAgent _agent;
    [Range(0, 5)] [SerializeField] private float _speedRotation;
    [SerializeField] private LayerMask _layerMask;

    private void Update()
    {
        if (_action == ActionType.Death)
        {
            _agent.isStopped = true;
            return;
        }

        if (Vector3.Distance(I.Character.transform.position, transform.position) > 2 && _action != ActionType.Attack)
        {
            if (_action != ActionType.Move && _action != ActionType.GetHit)
            {
                _agent.isStopped = false;
                SetAnimation(ActionType.Move);
            }
            _agent.SetDestination(I.Character.transform.position);
        }
        else if(_action != ActionType.Attack)
        {
            if (_action != ActionType.Idle && _action != ActionType.GetHit)
            {
                _agent.isStopped = true;
                SetAnimation(ActionType.Idle);
            }
            Attack();
        }

        _uihp.SetPosition(transform.position);
    }

    public override void Attack()
    {
        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up * .5f;
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(origin, direction, out hit, Mathf.Infinity, _layerMask))
            SetAnimation(ActionType.Attack);
        else
            Rotation();
    }

    private void Rotation()
    {
        if (_action == ActionType.Death)
            return;
        Vector3 target = I.Character.transform.position - transform.position;
        target.y = 0;
        Vector3 newTarger = Vector3.RotateTowards(transform.forward, target, _speedRotation * Time.deltaTime, 0);
        transform.rotation = Quaternion.LookRotation(newTarger);
    }
}
