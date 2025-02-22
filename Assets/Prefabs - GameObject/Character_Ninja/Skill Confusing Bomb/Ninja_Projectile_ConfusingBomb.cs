using System.Collections;
using UnityEngine;

public class Ninja_Projectile_ConfusingBomb : Projectile
{
    [SerializeField] private float _effectDuration;
    protected override void DealDamageBehavior(GameObject otherPlayer)
    {
        _otherHealthHandler.Public_DecreaseHealth(_damageAmount);
        _otherInputHandler.Public_ReverseMovementInput(_effectDuration);
    }
}
