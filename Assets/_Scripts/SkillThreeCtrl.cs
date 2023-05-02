using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillThreeCtrl : MonoBehaviour
{
    private Rigidbody2D rbOfSkillThree;
    [SerializeField] private float speedForceOfSkillThree = 10f;
    [SerializeField]private float returnSpeedOfSkill = 5f;
    private bool returning = false;
    private void Start() {
        rbOfSkillThree = GetComponent<Rigidbody2D>();
    } 
    private void Update() {
        if(!returning) SkillActive();
        if(returning) ReturnAfterFight();
    }
    protected void SkillActive(){
       rbOfSkillThree.velocity = transform.right * speedForceOfSkillThree; 
    }
    protected void ReturnAfterFight(){
        rbOfSkillThree.velocity = Vector2.zero;
        transform.position = Vector2.MoveTowards(transform.position, GameManager.instantiate.playerCtrl.transform.position, returnSpeedOfSkill * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
         if(other.gameObject.tag == "Player"){ 
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "enemy"){
            Debug.Log("Chạm Enemy");
        }
        if(other.gameObject.tag == "Wall"){
            Debug.Log("Chạm tường");
            returning = true;
        }
    }
}
