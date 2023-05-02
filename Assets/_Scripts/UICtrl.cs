using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UICtrl : MonoBehaviour
{
    /// <summary>
    /// popUIs[0]: skillUI
    /// popUIs[1]: PLayer parameter
    /// popUIs[2]: Score
    /// popUIs[3]: Time
    /// popUis[4]: LobbyUI
    /// popUIs[5]: gameOverUi
    /// </summary>
    [SerializeField] private GameObject[] PopUIs;
    [SerializeField] private Text[] textParameters;
    [SerializeField] private Animator anim;
    private float Timer = 0f;

    private void Update()
    {
        updateTime();
        updateParameterUi(
        GameManager.instantiate.playerCtrl.HEALTHPLAYER,
        GameManager.instantiate.playerCtrl.DAMAGE,
        GameManager.instantiate.playerCtrl.SPEED,
        GameManager.instantiate.weapon.speedWeapon);
        updateScore(GameManager.instantiate.enemyCtrl.Score);
        missionComplete(GameManager.instantiate.enemyCtrl.Score);
    }
    public void playGame(int _indexScence)
    {
        StartCoroutine(loadScene(_indexScence, 0.5f));
        Time.timeScale = 1f;
    }

    IEnumerator loadScene(int _indexScence, float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        SceneManager.LoadScene(_indexScence);
    }

    public void gameOver()
    {
        Time.timeScale = 0f;
        if (PopUIs[4] != null)
        {
            PopUIs[4].SetActive(true);
        }
        for (int i = 0; i < PopUIs.Length - 1; i++)
        {
            if (PopUIs[i] != null)
            {
                PopUIs[i].SetActive(false);
            }
        }
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void backToLobby()
    {
        SceneManager.LoadScene(0);
    }

    private void updateParameterUi(float health, float dmg, float speed, float attackSpeed)
    {
        textParameters[0].text = "Health: " + health;
        textParameters[1].text = "Damage: " + dmg;
        textParameters[2].text = "Speed: " + speed;
        textParameters[3].text = "Attack speed: " + attackSpeed;
    }

    public void updateScore(int _score)
    {
        textParameters[4].text = "" + _score;
    }
    private void updateTime()
    {
        Timer += Time.deltaTime;
        int seconds = (int)(Timer % 60);
        int minutes = (int)(Timer / 60) % 60;
        int hours = (int)(Timer / 3600) % 24;

        string timeString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        textParameters[5].text = timeString;
    }

    private void missionComplete(int Score)
    {
        if (anim != null)
        {
            if (Score == 0)
            {
                textParameters[6].enabled = false;
            }
            if (Score == 5)
            {
                textParameters[6].enabled = true;
                textParameters[6].text = "Tiêu diệt địch và nhặt vũ khí: " + Score + "/5 lần";
                anim.SetTrigger("AnimTrg");
            }
            if (Score == 30)
            {
                textParameters[6].text = "Tiêu diệt địch và nhặt vũ khí: " + Score + "/30 lần";
                anim.SetTrigger("AnimTrg");
            }
            if (Score == 60)
            {
                textParameters[6].text = "Tiêu diệt địch và nhặt vũ khí: " + Score + "/60 lần";
                anim.SetTrigger("AnimTrg");
            }
        }
    }


}
