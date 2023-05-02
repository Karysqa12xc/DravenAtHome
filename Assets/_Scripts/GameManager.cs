using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instantiate {private set; get;}
    public PlayerCtrl playerCtrl;
    public EnemyCtrl enemyCtrl;
    public pickWeapon pick_Weapon;
    public Weapon weapon;
    public UICtrl UI;
    public AudioHandler audioHandler;
    public CameraCtrl Camera;
    private void Awake() {
        if(instantiate == null) instantiate = this;
    }

    private void Start() {
        weapon.speedWeapon = 20;
        playerCtrl.DAMAGE = 1; 
        enemyCtrl.Score = 0;
    }

    private void FixedUpdate() {

    }
    private void Update() {
        
    }
    public void setUpSpeedWeapon(){
        weapon.speedWeapon = 20; 
    }
    public void setUpDamagePlayer(){
        playerCtrl.DAMAGE = 1; 
    }

   
}
