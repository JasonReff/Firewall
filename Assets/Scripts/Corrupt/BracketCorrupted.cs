using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BracketCorrupted : PlayerCharacter
{

    public override void Start()
    {
        anim.SetBool("Corrupt", true);
        Invoke("StopAnimation", .8f);
    }

    public void StopAnimation()
    {
        anim.SetBool("Corrupt", false);
        StartCoroutine(LifespanTick());
    }
    public override void PlayerCollision(Collision2D collision)
    {
        Uncorrupt();
    }

    public override void Uncorrupt()
    {
        Glitch.SetActive(true);
        Glitch.transform.position = gameObject.transform.position;
        GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().activePlayer = Glitch.GetComponent<PlayerCharacter>();
        Destroy(gameObject);
        Glitch.GetComponent<PlayerCharacter>().Uncorrupt();
        //add uncorrupt animation
    }

    public override void Fire()
    {
        if (readyToFire)
        {
            audioSource.clip = shootSound;
            audioSource.Play();
            Vector2 newDirection = reticle.characterToReticle / 10;
            float rotation = Vector2.Angle(newDirection, new Vector2(-1, 0));
            GameObject projectile = Instantiate(projectilePrefab, gameObject.transform.position + (Vector3)newDirection, Quaternion.identity);
            projectile.transform.Rotate(0, 0, rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce(newDirection * 10 * projectile.GetComponent<Projectile>().projectileForce);
            readyToFire = false;
            StartCoroutine(FireRecharge());
        }
    }
}
