using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Sword : Weapon
{
    [SerializeField] private AnimatorCharacter _animatorCharacter;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.enabled = false;
    }

    public override void Attack()
    {
        _animatorCharacter.SetAttack();
    }

    public override void FrameAttack()
    {
        _boxCollider.enabled = true;
        StartCoroutine(OffCollider());
    }

    private IEnumerator OffCollider()
    {
        yield return new WaitForSeconds(.1f);
        _boxCollider.enabled = false;
    }
}
