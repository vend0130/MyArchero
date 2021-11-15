using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorCharacter : MonoBehaviour
{
    [SerializeField] private Character _character;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _character.OnGetHit += GetHit;
        _character.OnDeath += Death;
    }

    private void Update()
    {
        if (_animator.GetBool("GetHit"))
            _character.Action = ActionType.GetHit;
        else if (_character.Direction.magnitude > 0 && _character.Action != ActionType.Move)
            _character.Action = ActionType.Move;
        else if (_character.Direction.magnitude == 0)
        {
            if ((I.CharacterAttack.Weapon.TypeWeapon == TypeWeapon.Melee && _animator.GetBool(I.CharacterAttack.Weapon.Name)) 
                || (I.CharacterAttack.Weapon.TypeWeapon == TypeWeapon.Ranged && I.CharacterAttack.NearEnemy != null))
                _character.Action = ActionType.Attack;
            else
                _character.Action = ActionType.Idle;
        }

        _animator.SetFloat("Move", _character.Direction.magnitude);
    }

    public void SetAttack()
    {
        bool value = I.CharacterAttack.NearEnemy;
        _animator.SetBool(I.CharacterAttack.Weapon.Name, value);
    }

    private void Death()
    {
        _animator.SetBool("Death", true);
    }

    private void GetHit()
    {
        _animator.SetBool("GetHit", true);
    }
    public void EndAnanimation(ActionType action)
    {
        _animator.SetBool("GetHit", false);
    }

    public void KeyFrameAttack()
    {
        I.CharacterAttack.Weapon.FrameAttack();
    }

    private void OnDestroy()
    {
        _character.OnGetHit -= GetHit;
        _character.OnDeath -= Death;
    }
}