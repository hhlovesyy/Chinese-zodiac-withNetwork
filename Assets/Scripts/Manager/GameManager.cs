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
        // ��ʾʱ��
        int minute = (int)(Time.timeSinceLevelLoad- timeBegin) / 60;
        int second = (int)(Time.timeSinceLevelLoad - timeBegin) - minute * 60;
        timeScore.text = string.Format("{0:D2}:{1:D2}", minute, second);

        // �����ʱ
        if (Time.timeSinceLevelLoad / 60 >= 5)
        {
            // Կ����ʾ
            // ��ͼ���ַ�ΧȦ
        }

    }

    private void initGame()
    {
        randomKey();
    }

    private void randomKey()
    {
        Debug.Log("�Զ�����Կ��");
        float x, z;
        x = UnityEngine.Random.Range(-20f, 70f); //-5f��(float)-5Ч��һ��
        z = UnityEngine.Random.Range(-20f, 20f);
        Instantiate(keyObject, new Vector3(x, 4f, z), Quaternion.identity);
        Debug.Log(keyObject.transform.position);
        Debug.Log("������");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���¼��س���
        Time.timeScale = 1; // ��ʼ��¼ʱ��
    }

    public void QuitGame()
    {
        Application.Quit(); // �˳���Ϸ
    }

    public void GameOver(bool win)
    {
        if (win)
        {
            Debug.Log("���ʤ��");
            Instance.gameOverUI.SetActive(true); // ��ʾ��Ϸʤ��UI
            Time.timeScale = 0f; // ʱ��ֹͣ��¼
        }
        else
        {
            Debug.Log("���ʧ��");
            // ��ʾ��Ϸʧ��UI
            Time.timeScale = 0f; // ʱ��ֹͣ��¼
        }
    }

    
}
