using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterAttack))]
public class Character : MonoBehaviour
{
    [Range(1, 6)] [SerializeField] private float _speed;
    [Range(5, 15)] [SerializeField] private float _rotationSpeed;
    [SerializeField] private GameObject _collider;

    private CharacterController _characterController;
    private CharacterAttack _characterAttack;
    private Vector3 _direction;
    private int _maxHp = 100;
    private int _incomingCollisionDamage = 20;
    private int _hp;
    private UIhp _uihp;
    private ActionType _action;

    public ActionType Action { get => _action; set => _action = value; }
    public Vector3 Direction { get => _direction; set => _direction = value; }
    public int Damage { get; set; }

    public event Action OnGetHit;
    public event Action OnDeath;

    private void Awake()
    {
        I.Character = this;
        _characterController = GetComponent<CharacterController>();
        _characterAttack = GetComponent<CharacterAttack>();
        _hp = _maxHp;
    }

    private void Start()
    {
        _uihp = Instantiate(I.Ui.HpBarPrafab, I.Ui.UIhpPanel).GetComponent<UIhp>();
        _uihp.Init(Who.Characte, transform.position);
    }

    private void Update()
    {
        if (_direction.magnitude > 0 && _action != ActionType.GetHit)
            Move();

        Rotation();
    }

    public void Move()
    {
        _characterController.Move(_direction * Time.deltaTime * _speed);
        Vector3 zeroY = transform.position;
        zeroY.y = 0;
        transform.position = zeroY;

        _uihp.SetPosition(transform.position);
    }

    public void GetHit(int damage)
    {
        _hp = _hp - damage < 0 ? 0 : _hp - damage;
        _uihp.UpdateHpBar(_maxHp, _hp);

        if (_hp == 0)
            Death();
        else
            OnGetHit?.Invoke();
    }

    private void Death()
    {
        OnDeath?.Invoke();
        _characterController.enabled = false;
        _collider.SetActive(false);
        I.Ui.EndGame();
        this.enabled = false;
    }

    private void Rotation()
    {
        Vector3 target = _direction;

        if(_action != ActionType.Move && _action != ActionType.GetHit && _characterAttack.NearEnemy)
        {
            target = _characterAttack.NearEnemy.transform.position - transform.position;
            target.y = 0;
        }
        
        Vector3 newTarger = Vector3.RotateTowards(transform.forward, target, _rotationSpeed * Time.deltaTime, 0);
        transform.rotation = Quaternion.LookRotation(newTarger);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.tag == "Enemy" && _characterAttack.Weapon.TypeWeapon == TypeWeapon.Ranged)
            GetHit(_incomingCollisionDamage);
    }
}
