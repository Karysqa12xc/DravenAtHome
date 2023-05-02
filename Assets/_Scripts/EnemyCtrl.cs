using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    public GameObject player;
    public float speedEnemy;
    private float distance;
    public float decelerationEnemyTime = 2f;
    public float decelerationTimer = 0f;
    public int Score = 0;
    [SerializeField] private float healEnemies;
    public float HEALENEMY
    {
        get { return healEnemies; }
        set { healEnemies = value; }
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            healEnemies = healEnemies - GameManager.instantiate.playerCtrl.DAMAGE;
            Debug.Log(healEnemies);
            if (healEnemies <= 0)
            {
                Destroy(gameObject);
                Debug.Log(Score);

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "SkillThree")
        {
            healEnemies = healEnemies - 3;
            if (healEnemies <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        //Enemy chase player
        EnemyChasePlayer();
        // mission complete
    }

    private void EnemyChasePlayer()
    {
        if (player != null)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speedEnemy * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }

}
