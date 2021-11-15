using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHp;
    [SerializeField] private int _damage = 20;

    public int Damage => _damage;

    private CapsuleCollider _capsuleCollider;
    private GameObject _effectSelected;

    protected UIhp _uihp;
    private int _hp;
    protected Animator _animator;
    protected ActionType _action = ActionType.Idle;
    protected Vector3 _direction;

    protected const string NAME = "Action";

    public abstract void Attack();

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        _hp = _maxHp;
    }

    private void Start()
    {
        _uihp = Instantiate(I.Ui.HpBarPrafab, I.Ui.UIhpPanel).GetComponent<UIhp>();
        _uihp.Init(Who.Enemy, transform.position);
        //_uihp.SetWho(Who.Enemy);
        //_uihp.SetPosition(transform.position);
        _effectSelected = Instantiate(I.EnemyManager.SelectEnemyPrefab, transform);

        Selected(false);
    }

    public void Selected(bool value)
    {
        _effectSelected.SetActive(value);
    }

    private void GetHit()
    {
        _hp -= I.CharacterAttack.Weapon.Damage;
        _hp = _hp < 0 ? 0 : _hp;

        _uihp.UpdateHpBar(hpmax: _maxHp, hpcurrent: _hp);

        if (_hp == 0)
            Death();
        else
            SetAnimation(ActionType.GetHit);
    }

    private void Death()
    {
        I.EnemyManager.RemoveEnemy(this);
        _capsuleCollider.enabled = false;
        SetAnimation(ActionType.Death);
    }

    public void SetAnimation(ActionType newAction)
    {
        _action = newAction;
        int value = ((int)newAction);
        _animator.SetInteger(NAME, value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CharacterAttackMelee")
            GetHit();
        else if(other.tag == "CharacterAttackRanged")
        {
            GetHit();
            other.gameObject.SetActive(false);
        }
    }
}
