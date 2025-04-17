using System.Collections;
using UnityEngine;

public class Ninja_Projectile_ConfusingBomb : Projectile
{
    [SerializeField] private float _effectDuration;
    [SerializeField] private float _verticalLaunchForce;

    public override void LaunchProjectile(GameObject shooter)
    { 
        base.LaunchProjectile(shooter);
        _rb.AddForce(Vector2.up * _verticalLaunchForce, ForceMode2D.Impulse);
    }

    protected override void DealDamageBehavior(GameObject otherPlayer)
    {
        _otherHealthHandler.Public_DecreaseHealth(_damageAmount);
        _otherInputHandler.Public_ReverseMovementInput(_effectDuration);
    }
}
