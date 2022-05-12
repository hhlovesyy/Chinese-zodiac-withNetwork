using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
    public enum ItemType { Bomb, Ink, Landmine };

    public ItemType itemType;

    private GameObject itemUI;

    void Start()
    {
        // ����ʱ�����ȷ���Լ������
        int typeNumber = Random.Range(0, 3);  //ȡ0-3��������3��
        switch (typeNumber)
        {
            case 0:
                itemType = ItemType.Bomb;
                break;
            case 1:
                itemType = ItemType.Ink;
                break;
            case 2:
                itemType = ItemType.Landmine;
                break;
        }
    }
    public void DestorySelf()
    {
        Debug.Log("��������Ϊ"+itemType);
        // ��������δ��
        if (FindItemUI())
        {
            DisplayInItemBar();
            Destroy(gameObject);
        }
    }

    // ���������Ƿ�������ѡ����һ��panel
    private bool FindItemUI()
    {
        if(GameObject.Find("ItemIcon_1").GetComponent<Image>().color.a == 0.0f)
        {
            itemUI = GameObject.Find("ItemIcon_1");
            return true;
        }
        else if (GameObject.Find("ItemIcon_2").GetComponent<Image>().color.a == 0.0f)
        {
            itemUI = GameObject.Find("ItemIcon_2");
            return true;
        }
        else if (GameObject.Find("ItemIcon_3").GetComponent<Image>().color.a == 0.0f)
        {
            itemUI = GameObject.Find("ItemIcon_3");
            return true;
        }
        return false;
    }
    private void DisplayInItemBar()
    {       
        string imgPath;
        switch (itemType)
        {
            case ItemType.Bomb:
                // �ҵ��ļ�·��������Panel
                //string imgPath = Application.dataPath + "Images/ItemsIcon/" + "bomb.png
                imgPath = "Images/ItemsIcon/" + "bomb";
                itemUI.GetComponent<Image>().color = new Color(255, 255, 255, 1.0f);
                itemUI.GetComponent<Image>().sprite = Resources.Load(imgPath, typeof(Sprite)) as Sprite;
                Debug.Log(imgPath);
                break;
            case ItemType.Ink:
                // �ҵ��ļ�·��������Panel
                //string imgPath = Application.dataPath + "Images/ItemsIcon/" + "bomb.png
                imgPath = "Images/ItemsIcon/" + "ink";
                itemUI.GetComponent<Image>().color = new Color(255, 255, 255, 1.0f);
                itemUI.GetComponent<Image>().sprite = Resources.Load(imgPath, typeof(Sprite)) as Sprite;
                Debug.Log(imgPath);
                break;
            case ItemType.Landmine:
                // �ҵ��ļ�·��������Panel
                //string imgPath = Application.dataPath + "Images/ItemsIcon/" + "bomb.png
                imgPath = "Images/ItemsIcon/" + "landmine";
                itemUI.GetComponent<Image>().color = new Color(255, 255, 255, 1.0f);
                itemUI.GetComponent<Image>().sprite = Resources.Load(imgPath, typeof(Sprite)) as Sprite;
                Debug.Log(imgPath);
                break;
        }
    }
}
