using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeEnemy : Enemy
{

    public override void EnemyFire()
    {
        Vector2 newDirection = -enemyToPlayer / 10;
        GameObject projectile1 = Instantiate(enemyProjectile, gameObject.transform.position + (Vector3)newDirection, Quaternion.identity);
        GameObject projectile2 = Instantiate(enemyProjectile, gameObject.transform.position + (Vector3)newDirection + new Vector3(newDirection.y, -newDirection.x), Quaternion.identity);
        GameObject projectile3 = Instantiate(enemyProjectile, gameObject.transform.position + (Vector3)newDirection + new Vector3(-newDirection.y, newDirection.x), Quaternion.identity);
        Vector2 projectile2Direction = newDirection + new Vector2(newDirection.y, -newDirection.x);
        Vector2 projectile3Direction = newDirection + new Vector2(-newDirection.y, newDirection.x);
        projectile1.GetComponent<Rigidbody2D>().AddForce(newDirection * 10 * projectile1.GetComponent<Projectile>().projectileForce);
        projectile2.GetComponent<Rigidbody2D>().AddForce(projectile2Direction * 10 * projectile2.GetComponent<Projectile>().projectileForce);
        projectile3.GetComponent<Rigidbody2D>().AddForce(projectile3Direction * 10 * projectile3.GetComponent<Projectile>().projectileForce);
    }
}
