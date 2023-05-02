using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTwoCtrl : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rbOfSkillObj;
    [SerializeField] private float speedSkillTwo = 10f;
    public Collider2D[] inExplosionRadius = null;
    [SerializeField] private float ExplosionForce = 2f;
    [SerializeField] private float ExplosionZone = 10f;

    [SerializeField] private GameObject _Player;
    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        rbOfSkillObj = GetComponent<Rigidbody2D>();
        rbOfSkillObj.velocity = transform.right * speedSkillTwo;
    }
    // Update is called once per frame
    void Update()
    {
        Explode();
        DeleteObj();
    }

    private void DeleteObj(){
        float distanceSkill2AndPlayer = Vector2.Distance(_Player.transform.position, gameObject.transform.position);
        if(distanceSkill2AndPlayer >= 10){
            Destroy(gameObject);
        }
    }
    //Skill 2
    void Explode()
    {
        inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, ExplosionZone);
        foreach (Collider2D enemy in inExplosionRadius)
        {
            if (enemy.tag == "enemy")
            {
                StartCoroutine(PushEnemy(enemy));
            }
        }
    }
    IEnumerator PushEnemy(Collider2D enemyCollider)
    {
        Rigidbody2D enemyRigidbody = enemyCollider.GetComponent<Rigidbody2D>();

        // Tính toán hướng và lực cần đẩy enemy
        if (enemyRigidbody != null)
        {
            Vector2 pushDirection = enemyCollider.transform.position - transform.position;
            pushDirection = pushDirection.normalized;
            float pushForce = ExplosionForce / pushDirection.magnitude;
            enemyRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Force);
            yield return new WaitForSeconds(0.1f);
            enemyRigidbody.velocity = Vector2.zero;
            enemyRigidbody.angularVelocity = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosionZone);
    }

}
