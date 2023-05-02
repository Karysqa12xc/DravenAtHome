using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    #region PRIVATE
    [SerializeField] private float Speed = 4f;
    [SerializeField] private LayerMask layerEnemy;
    [SerializeField] private GameObject attackStart;
    [SerializeField] private int HealPlayer;
    [SerializeField] private float dmg = 0f;
    #endregion
    #region PUBLIC 
    public bool moving;
    public float attackDistance;
    public GameObject AxeWeapon;
    #endregion

   
    #region GET, SET
    public Vector2 lastClickPos{private set; get;}
    #endregion

    public float SPEED{
        get{
            return Speed;
        }
        set{
            Speed = value;
        }
    }

    public float DAMAGE{
        get{
            return dmg;
        }
        set{
            dmg = value;
        }
    }
    public float HEALTHPLAYER{
        get{return HealPlayer;}
    }

    private void Update()
    {
        //Di chuyển 
        Move();
        //Tìm Enemy để tấn công
        findEnemy();
        //Xoay theo hướng chuột
        RotateFollowMouse();
    }

    private void Move()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
        }
        if (moving && (Vector2)transform.position != lastClickPos)
        {
            float step = Speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, lastClickPos, step);
        }
        else
        {
            moving = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            HealPlayer--;
            if (HealPlayer <= 0 && gameObject != null) {
                gameObject.SetActive(false);
                GameManager.instantiate.audioHandler.source.PlayOneShot(GameManager.instantiate.audioHandler.clips[4]);
                GameManager.instantiate.Camera.audioBehaviour.enabled  = false;
                GameManager.instantiate.UI.gameOver();
            }
        }
    }
    public void findEnemy()
    {
        //Nếu người dùng bấm chuột trái
        if (Input.GetMouseButtonDown(0))
        {
            //Tìm kiếm Enemy theo con vị ví chuột với giá trị từ 0 đến vô cực với vật thể có layerMask là: EnemyMask
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, layerEnemy);
            //Nếu vật thể được chọn có collider khác null và vị trí nhỏ hơn vị trí attack 
            if (hit.collider != null && hit.distance < attackDistance)
            {
                //Gán mục tiêu bằng với mục tiêu được click 
                GameObject target = hit.collider.gameObject;
                //Gọi hàm Attack
                Attack(target);
            }
        }
    }

    private void Attack(GameObject target)
    {
        if(target.tag == "enemy" && target != null){
            GameObject axes =  Instantiate(AxeWeapon, attackStart.transform.position, Quaternion.identity);
            GameManager.instantiate.audioHandler.source.PlayOneShot(GameManager.instantiate.audioHandler.clips[3]);
            axes.GetComponent<Weapon>().SetTarget(target.transform, GameManager.instantiate.weapon.speedWeapon);
        }
    }

    public void RotateFollowMouse(){
        // //Tìm vị trí con trỏ chuột
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Tìm vector hướng từ vị trí player tới vị trí con trỏ chuột
        Vector3 dir = mousePos - transform.position;
        //Chuyển vector hướng về vị trí player thành giá trị 0 - 1 
        dir.Normalize();
        //Tính góc xoay
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Cho góc quay của player bằng với vị trí góc quay
        transform.rotation = Quaternion.Euler(0f, 0f, angle);  
    }
}
