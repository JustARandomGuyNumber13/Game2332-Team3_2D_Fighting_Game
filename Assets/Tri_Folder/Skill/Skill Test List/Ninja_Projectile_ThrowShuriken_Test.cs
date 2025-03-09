using System.Collections;
using UnityEngine;

public class Ninja_Projectile_ThrowShuriken_Test : Projectile
{
    protected override void DealDamageBehavior(GameObject otherPlayer)
    {
        _otherHealthHandler.Public_DecreaseHealth(_damageAmount);
    }
}
