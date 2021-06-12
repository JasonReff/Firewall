using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteriskEnemy : Enemy
{

    public override void EnemyFire()
    {
        GameObject projectile = Instantiate(enemyProjectile);
        projectile.GetComponent<Rigidbody2D>().AddForce(enemyToPlayer * projectile.GetComponent<Projectile>().projectileForce);
    }
}
