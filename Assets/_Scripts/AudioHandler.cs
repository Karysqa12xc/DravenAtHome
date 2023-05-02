using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class AudioHandler : MonoBehaviour, IPointerDownHandler
{
    /// <summary>
    /// [0] click button(lobby)
    /// [1]: skillQ
    /// [2]: tocbien
    /// [3]: player attack
    /// [4]: gameover
    /// </summary>
    public AudioClip[] clips;
    public AudioSource source;
    public AudioBehaviour audioMission;
    public bool countActive = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        source.PlayOneShot(clips[0]);
    }

    private void Update()
    {
        missionPlayAudio();
    }
    public void missionPlayAudio()
    {
        if (audioMission != null)
        {
            if (GameManager.instantiate.enemyCtrl.Score == 5 ||
            GameManager.instantiate.enemyCtrl.Score == 30 || GameManager.instantiate.enemyCtrl.Score == 60)
            {
                audioMission.enabled = true;
            }
            else
            {
                audioMission.enabled = false;
            }
        }
    }
}
