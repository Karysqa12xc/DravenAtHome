using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillPlayer : MonoBehaviour
{
    private bool[] coolDownSkill;

    [SerializeField] private GameObject skillStartLeft, skillStartRight;
    [SerializeField] private GameObject PrefabsSkillTwoLeft, PrefabsSkillTwoRight;
    [SerializeField] private GameObject PrefabsSkillThreeLeft, PrefabsSkillThreeRight;
    [SerializeField] private GameObject wrapPosPlayer, wrapPosMous;
    [Header("Time cooldown")]
    [SerializeField] private float bloodRushCooldown = 15f;
    [SerializeField] private float bloodRushTimer = 0f;
    [SerializeField] private float standAsideCooldown = 7f;
    [SerializeField] private float standAsideTimer = 0f;
    [SerializeField] private float whirlingDeathCooldown = 30f;
    [SerializeField] private float whirlingDeathTimer = 0f;
    [SerializeField] private float warpCoolDown = 15f;
    [SerializeField] private float warpTimer = 0f;
    [SerializeField] private float warpDistace = 5f;
    [SerializeField] private TrailRenderer trail; 
    [Header("Skill cooldown UI parameter")]
    [SerializeField] private Image[] skillImgCoolDown;
    // Update is called once per frame 
    void Update()
    {

        SkillBloodRush();
        SkillStandAside();
        SkillWhirlingDeath();
        skillWarp();
    }

    private void SkillBloodRush()
    {
        if (bloodRushTimer <= 0f && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Active skill blood rush");
            GameManager.instantiate.audioHandler.source.PlayOneShot(GameManager.instantiate.audioHandler.clips[1]);
            trail.emitting = true;
            GameManager.instantiate.playerCtrl.SPEED = 10;
            skillImgCoolDown[0].fillAmount += 1 / bloodRushCooldown * Time.deltaTime;
            bloodRushTimer = bloodRushCooldown;
        }
        else if (bloodRushTimer > 0f)
        {
            GameManager.instantiate.playerCtrl.SPEED = 10;
            skillImgCoolDown[0].fillAmount += 1 / bloodRushCooldown * Time.deltaTime;
            bloodRushTimer -= Time.deltaTime;
            if (bloodRushTimer <= 0)
            {
                GameManager.instantiate.playerCtrl.SPEED = 4;
                trail.emitting = false;
                skillImgCoolDown[0].fillAmount = 0;
                bloodRushTimer = 0f;
            }
        }
    }

    private void SkillStandAside()
    {
        if (standAsideTimer <= 0 && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Active skill Stand Aside");
            GameObject spawnSkillTwo1 = Instantiate(PrefabsSkillTwoLeft, skillStartLeft.transform.position, skillStartLeft.transform.rotation);
            GameObject spawnSkillTwo2 = Instantiate(PrefabsSkillTwoRight, skillStartRight.transform.position, skillStartRight.transform.rotation);
            skillImgCoolDown[1].fillAmount += 1 / standAsideCooldown * Time.deltaTime;
            standAsideTimer = standAsideCooldown;
        }
        else
        {
            skillImgCoolDown[1].fillAmount += 1 / standAsideCooldown * Time.deltaTime;
            standAsideTimer -= Time.deltaTime;
            if (standAsideTimer <= 0)
            {
                skillImgCoolDown[1].fillAmount = 0;
                standAsideTimer = 0f;
            }
        }
    }

    private void SkillWhirlingDeath()
    {
        if (whirlingDeathTimer <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Active skill Whirling Death");
            GameObject spawnSkillThree1 = Instantiate(PrefabsSkillThreeLeft, skillStartLeft.transform.position, skillStartLeft.transform.rotation);
            GameObject spawnSkillThree2 = Instantiate(PrefabsSkillThreeRight, skillStartRight.transform.position, skillStartRight.transform.rotation);
            skillImgCoolDown[2].fillAmount += 1 / whirlingDeathCooldown * Time.deltaTime;
            whirlingDeathTimer = whirlingDeathCooldown;
        }
        else
        {
            skillImgCoolDown[2].fillAmount += 1 / whirlingDeathCooldown * Time.deltaTime;
            whirlingDeathTimer -= Time.deltaTime;
            if (whirlingDeathTimer <= 0)
            {
                skillImgCoolDown[2].fillAmount = 0;
                whirlingDeathTimer = 0f;
            }
        }
    }
    private void skillWarp()
    {
        if (warpTimer <= 0 && Input.GetKeyDown(KeyCode.F))
        {
            Vector3 targetTele = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distaceTele = Vector2.Distance(transform.position, targetTele);
            if (distaceTele <= warpDistace)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, targetTele - transform.position, distaceTele);
                if (hit.collider.tag != "Wall" && wrapPosPlayer != null && wrapPosMous != null)
                {
                    Debug.Log("Active Wrap");
                    GameManager.instantiate.audioHandler.source.PlayOneShot(GameManager.instantiate.audioHandler.clips[2]);
                    GameObject wraplayerpos =  Instantiate(wrapPosPlayer, transform.position, Quaternion.identity);
                    targetTele.z = transform.position.z;
                    transform.position = targetTele;
                    GameObject wrapmouse =  Instantiate(wrapPosMous, targetTele, Quaternion.identity);
                    skillImgCoolDown[3].fillAmount += 1 / warpCoolDown * Time.deltaTime;
                    Destroy(wraplayerpos, 0.6f);
                    Destroy(wrapmouse, 0.7f);
                    warpTimer = warpCoolDown;
                }
            }
        }
        else
        {
            skillImgCoolDown[3].fillAmount += 1 / warpCoolDown * Time.deltaTime;
            warpTimer -= Time.deltaTime;
            if (warpTimer <= 0)
            {
                skillImgCoolDown[3].fillAmount = 0;
                warpTimer = 0f;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, warpDistace);
    }


}
