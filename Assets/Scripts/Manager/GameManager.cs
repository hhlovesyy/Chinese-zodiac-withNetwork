using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : Singleton<GameManager>
{
    [Header("GameObject")]
    public GameObject keyObject;

    [Header("UI")]
    public Text timeScore;
    public GameObject gameOverUI;

    public static float  timeBegin=0;
    //public void settimebegin(Time timeBe)
    //{
    //    timeBegin = timeBe;
    //}
    //internal static void settimebegin(float time)
    //{
    //    throw new NotImplementedException();
    //}
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        // gameOverUI.SetActive(false);
        initGame();
    }

    void Update()
    {
        // 显示时间
        int minute = (int)(Time.timeSinceLevelLoad- timeBegin) / 60;
        int second = (int)(Time.timeSinceLevelLoad - timeBegin) - minute * 60;
        timeScore.text = string.Format("{0:D2}:{1:D2}", minute, second);

        // 五分钟时
        if (Time.timeSinceLevelLoad / 60 >= 5)
        {
            // 钥匙明示
            // 地图显现范围圈
        }

    }

    private void initGame()
    {
        randomKey();
    }

    private void randomKey()
    {
        Debug.Log("自动生成钥匙");
        float x, z;
        x = UnityEngine.Random.Range(-20f, 70f); //-5f和(float)-5效果一样
        z = UnityEngine.Random.Range(-20f, 20f);
        Instantiate(keyObject, new Vector3(x, 4f, z), Quaternion.identity);
        Debug.Log(keyObject.transform.position);
        Debug.Log("正常吗");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 重新加载场景
        Time.timeScale = 1; // 开始记录时间
    }

    public void QuitGame()
    {
        Application.Quit(); // 退出游戏
    }

    public void GameOver(bool win)
    {
        if (win)
        {
            Debug.Log("玩家胜利");
            Instance.gameOverUI.SetActive(true); // 显示游戏胜利UI
            Time.timeScale = 0f; // 时间停止记录
        }
        else
        {
            Debug.Log("玩家失败");
            // 显示游戏失败UI
            Time.timeScale = 0f; // 时间停止记录
        }
    }

    
}
