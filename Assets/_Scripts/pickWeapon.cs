using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickWeapon : MonoBehaviour
{
    public bool playerInZone = false;
    public bool weaponInZone = false;
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInZone = false;
        }
        if (other.gameObject.tag == "Player")
        {
            weaponInZone = false;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player");
            playerInZone = true;
        }
        if (other.gameObject.tag == "Weapon")
        {
            Debug.Log("Weapon");
            weaponInZone = true;
            Debug.Log(weaponInZone);
            Destroy(gameObject, 0.1f);
            Destroy(other.gameObject, 0.1f);
        }
    }

    private void Update()
    {
        if (playerInZone == true && weaponInZone == true)
        {
            Debug.Log("Player and weapon");
            Destroy(gameObject);
            GameManager.instantiate.weapon.speedWeapon += 5f;
            GameManager.instantiate.playerCtrl.DAMAGE  += 1;
            GameManager.instantiate.enemyCtrl.Score += 1;
            Debug.Log(GameManager.instantiate.weapon.speedWeapon);
        }
        if (playerInZone == false && weaponInZone == true)
        {
            Debug.Log("Missing Weapon");
            GameManager.instantiate.setUpSpeedWeapon();
            GameManager.instantiate.setUpDamagePlayer();
            Debug.Log(GameManager.instantiate.weapon.speedWeapon);
        }
        Destroy(gameObject, 5f);
    }
}
