using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmpersandCorrupt : CorruptPlayer
{ 

    public override void Fire()
    {
        if (readyToFire)
        {
            StartCoroutine(FireProjectiles());
            readyToFire = false;
            StartCoroutine(FireRecharge());
        }
    }

    IEnumerator FireProjectiles()
    {
        for (int i = 0; i <= 2; i++)
        {
            yield return new WaitForSeconds(.2f);
            Fire1Projectile();
        }
    }

    void Fire1Projectile()
    {
        audioSource.clip = shootSound;
        audioSource.Play();
        Vector2 newDirection = reticle.characterToReticle.normalized;
        ShootProjectileWithForce(projectilePrefab, newDirection);
    }
}
