using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject _modelWeapon;
    [SerializeField] private int _damage;
    [SerializeField] private TypeWeapon _typeWeapon;

    [SerializeField] protected float _speedAttack;

    public string Name => _name;
    public int Damage { get => _damage; set => _damage = value; }
    public float SpeedAttack { get => _speedAttack; set => _speedAttack = value; }
    public TypeWeapon TypeWeapon { get => _typeWeapon; set => _typeWeapon = value; }

    public abstract void Attack();

    public void SetActiveModel()
    {
        _modelWeapon.SetActive(true);
    }

    public virtual void FrameAttack()
    {
        Debug.Log("frame attack");
    }
}
