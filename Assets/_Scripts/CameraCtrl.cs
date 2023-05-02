using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraPos;
    [SerializeField] private float speedCam;
    public AudioBehaviour audioBehaviour;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cameraPos = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update() {
        if(cameraPos.position != player.position){
            Vector3 cameraFollow = new Vector3(player.position.x, player.position.y, cameraPos.position.z);
            cameraPos.position = cameraFollow;
        }
    }
}
