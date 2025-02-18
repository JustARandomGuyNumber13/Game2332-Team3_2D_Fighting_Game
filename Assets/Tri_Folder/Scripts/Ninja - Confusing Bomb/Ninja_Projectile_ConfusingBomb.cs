using System.Collections;
using UnityEngine;

public class Ninja_Projectile_ConfusingBomb : Projectile
{
    [SerializeField] private float _effectDuration;
    protected override void DealDamageBehavior(GameObject otherPlayer)
    {
        _otherHealthHandler.DecreaseHealth(_damageAmount);

        _otherHealthHandler.ReverseMovementInput(_effectDuration);
    }
}
