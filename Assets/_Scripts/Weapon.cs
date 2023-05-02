using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Transform target;
    public float speedWeapon, speedWeaponBack, randomRadius;
    public float speedWeaponMax { private set; get; } = 1000f;
    public bool isThrown = false;
    public bool isReturning = false;
    private Vector2 throwTargetPosition;
    private Rigidbody2D rb;
    public GameObject posPickWeapon;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            isReturning = true;
            gameObject.tag = "Weapon";
            HandleWeaponComeBack();
        }
    }
    private void Update()
    {
        if(isReturning){
            comeBackPlayer();
        }
        if (!isReturning && target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speedWeapon * Time.deltaTime); // Di chuyển theo hướng Enemy
        }
        if(target == null){
            comeBackPlayer();
        }
        Destroy(gameObject, 4.5f);
    }   
    public void comeBackPlayer()
    {
        if (isThrown)
        {
            // Di chuyển Weapon về vị trí gần Player
            transform.position = Vector2.MoveTowards(transform.position, throwTargetPosition, speedWeaponBack * Time.deltaTime);
            // Nếu Weapon đã quay trở lại vị trí gần Player, ta gán lại giá trị isReturning và kích hoạt lại collider của Weapon
            if (Vector2.Distance(transform.position, throwTargetPosition) < 0.1f)
            {
                isThrown = false;
                GetComponent<PolygonCollider2D>().enabled = true;
            }
        }
    }

    private void ReturnToPlayer()
    {
        // Tính vị trí gần Player, nơi mà Weapon sẽ quay trở lại
        Vector2 playerPosition = GameManager.instantiate.playerCtrl.transform.position;
        Vector2 returnPosition = playerPosition + Random.insideUnitCircle.normalized * randomRadius;
        Instantiate(posPickWeapon, returnPosition, Quaternion.identity);
        // Gán giá trị targetPosition và isThrown để Weapon di chuyển về vị trí gần Player
        throwTargetPosition = returnPosition;
        isThrown = true;
    }
    public void HandleWeaponComeBack()
    {
        // Lưu vị trí của player và Weapon
        throwTargetPosition = GameManager.instantiate.playerCtrl.transform.position;
        // Tắt collider của Weapon để không va chạm với bất kỳ thứ gì khác
        GetComponent<PolygonCollider2D>().enabled = false;
        isThrown = false;
        // Sau khoảng thời gian returnDelay, ta cho Weapon quay trở lại vị trí gần Player
        Invoke("ReturnToPlayer", 0f);
    }
    
    public void SetTarget(Transform target, float speed)
    {
        this.target = target;
        this.speedWeapon = speed;
    }

    // public void OnTriggerStay2D(Collider2D other) {
    //     if(other.gameObject.tag == "Finish"){
    //         Debug.Log("Weapon");
    //         GameManager.instantiate.pick_Weapon.weaponInZone = true;
    //         GameManager.instantiate.setUpSpeedWeapon();
    //         Destroy(gameObject);
    //         Destroy(other.gameObject, .1f);
    //     }
    // }

}
