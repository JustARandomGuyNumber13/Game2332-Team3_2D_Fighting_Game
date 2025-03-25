using System.Collections;
using UnityEngine;

public class T_GP_Projectile_ThrowShuriken : Projectile
{
    protected override void DealDamageBehavior(GameObject otherPlayer)
    {
        _otherHealthHandler.Public_DecreaseHealth(_damageAmount);
    }
}
