using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int id;
    public float movementSpeed;
    public float minimumDistanceFromPlayer;
    public float distanceFromPlayer;
    public float maximumFiringRange;
    public float fireRate;
    public Vector2 enemyToPlayer;
    public GameObject player;
    public int hp;
    public GameObject enemyProjectile;
    public GameObject Glitch;
    public Animator anim;

    public virtual void Awake()
    {
        StartCoroutine(EnemyMove());
        StartCoroutine(FireAtPlayer());
        StartCoroutine(FindPlayer());
    }

    public virtual void Update()
    {
        if (player != null)
        {
            enemyToPlayer = transform.position - player.transform.position;
        }
        distanceFromPlayer = enemyToPlayer.magnitude;
    }

    public IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(0.3f);
        if (EventSystem.current.activePlayer != null)
        {
            player = EventSystem.current.activePlayer.gameObject;
        }
        StartCoroutine(FindPlayer());
    }

    public virtual IEnumerator EnemyMove()
    {
        yield return new WaitForSeconds(0.2f);
        if (distanceFromPlayer > minimumDistanceFromPlayer)
        {
            MoveTowardCharacter();
            MoveSideways();
        }
        else
        {
            MoveSideways();
        }
        StartCoroutine(EnemyMove());
    }

    public virtual void MoveSideways()
    {
        Vector2 direction = enemyToPlayer.normalized;
        int leftOrRight = UnityEngine.Random.Range(1, 4);
        Vector2 perpendicular = new Vector2();
        switch (leftOrRight)
        {
            case 1:
                perpendicular = new Vector2 (direction.y, -direction.x);
                break;
            case 2:
                perpendicular = new Vector2(-direction.y, direction.x);
                break;
            case 3:
                perpendicular = new Vector2(0, 0);
                break;
        }
        transform.position += new Vector3(perpendicular.x * movementSpeed, perpendicular.y * movementSpeed, 0);
    }

    public virtual void MoveTowardCharacter()
    {
        Vector2 direction = new Vector2(enemyToPlayer.x / enemyToPlayer.magnitude, enemyToPlayer.y / enemyToPlayer.magnitude);
        transform.position -= new Vector3(direction.x * movementSpeed, direction.y * movementSpeed, 0);
    }

    IEnumerator FireAtPlayer()
    {
        yield return new WaitForSeconds(fireRate);
        if (IsWithinBounds())
        {
            if (distanceFromPlayer < maximumFiringRange)
            {
                EnemyFire();
            }
        }
        StartCoroutine(FireAtPlayer());
    }

    bool IsWithinBounds()
    {
        if (gameObject.transform.position.x < 8.5 && gameObject.transform.position.x > -8.5 &&
            gameObject.transform.position.y < 4.5 && gameObject.transform.position.y > -4.5)
        {
            return true;
        }
        else return false;
    }

    public virtual void EnemyFire()
    {
        //must make this
    }

    public void TakeDamage()
    {
        hp--;
        if (hp <= 0)
        {
            EnemyDies();
        }
    }

    public void EnemyDies()
    {
        EventSystem.current.EnemyDeath();
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0, 0);
        Invoke("Destroy", 1.0f);
        anim.SetBool("dead", true);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
