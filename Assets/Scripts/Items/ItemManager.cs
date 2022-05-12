//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class ItemManager : MonoBehaviour
//{
//    private Image itemIcon;

//    [Header("道具Prefab")]
//    public GameObject rockPrefab;
//    public GameObject bombPrefab;

//    void Start()
//    {

//    }


//    void Update()
//    {

//    }

//    public void useItem1(Transform throwPos)
//    {
//        itemIcon = GameObject.Find("ItemIcon_1").GetComponent<Image>();
//        //如果有道具才能使用
//        if (itemIcon.color.a == 1.0f)
//        {
//            ThrowItem(throwPos);
//        }
//        Image nextItemIcon = GameObject.Find("ItemIcon_2").GetComponent<Image>();
//        Image thirdItemIcon = GameObject.Find("ItemIcon_3").GetComponent<Image>();
//        // 如果下一个panel里有道具，移动：下一个panel的透明度为0
//        if (nextItemIcon.color.a == 1.0f)
//        {
//            //如果第三个道具栏也有道具
//            if (thirdItemIcon.color.a == 1.0f)
//            {
//                thirdItemIcon.color = new Color(255, 255, 255, 0.0f);
//                itemIcon.color = new Color(255, 255, 255, 1.0f);
//                itemIcon.sprite = nextItemIcon.sprite;
//                nextItemIcon.color = new Color(255, 255, 255, 1.0f);
//                nextItemIcon.sprite = thirdItemIcon.sprite;

//            }
//            else
//            {
//                nextItemIcon.color = new Color(255, 255, 255, 0.0f);
//                itemIcon.color = new Color(255, 255, 255, 1.0f);
//                itemIcon.sprite = nextItemIcon.sprite;
//            }
//        }
//        else
//        {
//            itemIcon.color = new Color(255, 255, 255, 0.0f);
//        }
//    }

//    public void useItem2(Transform throwPos)
//    {
//        itemIcon = GameObject.Find("ItemIcon_2").GetComponent<Image>();
//        //如果有道具才能使用
//        if (itemIcon.color.a == 1.0f)
//        {
//            ThrowItem(throwPos);
//        }
//        Image nextItemIcon = GameObject.Find("ItemIcon_3").GetComponent<Image>();
//        // 如果下一个panel里有道具，移动：下一个panel的透明度为0
//        if (nextItemIcon.color.a == 1.0f)
//        {
//            nextItemIcon.color = new Color(255, 255, 255, 0.0f);
//            itemIcon.color = new Color(255, 255, 255, 1.0f);
//            itemIcon.sprite = nextItemIcon.sprite;
//        }
//        else
//        {
//            itemIcon.color = new Color(255, 255, 255, 0.0f);
//        }
//    }

//    public void useItem3(Transform throwPos)
//    {
//        itemIcon = GameObject.Find("ItemIcon_3").GetComponent<Image>();
//        //如果有道具才能使用
//        if (itemIcon.color.a == 1.0f)
//        {
//            ThrowItem(throwPos);
//        }
//        itemIcon.color = new Color(255, 255, 255, 0.0f);
//    }


//    public void ThrowItem(Transform throwPos)
//    {
//        switch (itemIcon.sprite.name)
//        {
//            case "bomb":
//                Debug.Log("丢炸弹");
//                // 生成rock
//                var rock = Instantiate(bombPrefab, throwPos.position, Quaternion.identity);
//                break;
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

static class ItemManager
{
    private static Image itemIcon;

    [Header("道具Prefab")]
    public static GameObject bombPrefab;

    public static void useItem1(Transform throwPos)
    {
        itemIcon = GameObject.Find("ItemIcon_1").GetComponent<Image>();
        //如果有道具才能使用
        if (itemIcon.color.a == 1.0f)
        {
            ThrowItem(throwPos);
        }
        Image nextItemIcon = GameObject.Find("ItemIcon_2").GetComponent<Image>();
        Image thirdItemIcon = GameObject.Find("ItemIcon_3").GetComponent<Image>();
        // 如果下一个panel里有道具，移动：下一个panel的透明度为0
        if (nextItemIcon.color.a == 1.0f)
        {
            //如果第三个道具栏也有道具
            if (thirdItemIcon.color.a == 1.0f)
            {
                thirdItemIcon.color = new Color(255, 255, 255, 0.0f);
                itemIcon.color = new Color(255, 255, 255, 1.0f);
                itemIcon.sprite = nextItemIcon.sprite;
                nextItemIcon.color = new Color(255, 255, 255, 1.0f);
                nextItemIcon.sprite = thirdItemIcon.sprite;

            }
            else
            {
                nextItemIcon.color = new Color(255, 255, 255, 0.0f);
                itemIcon.color = new Color(255, 255, 255, 1.0f);
                itemIcon.sprite = nextItemIcon.sprite;
            }
        }
        else
        {
            itemIcon.color = new Color(255, 255, 255, 0.0f);
        }
    }

    public static void useItem2(Transform throwPos)
    {
        itemIcon = GameObject.Find("ItemIcon_2").GetComponent<Image>();
        //如果有道具才能使用
        if (itemIcon.color.a == 1.0f)
        {
            ThrowItem(throwPos);
        }
        Image nextItemIcon = GameObject.Find("ItemIcon_3").GetComponent<Image>();
        // 如果下一个panel里有道具，移动：下一个panel的透明度为0
        if (nextItemIcon.color.a == 1.0f)
        {
            nextItemIcon.color = new Color(255, 255, 255, 0.0f);
            itemIcon.color = new Color(255, 255, 255, 1.0f);
            itemIcon.sprite = nextItemIcon.sprite;
        }
        else
        {
            itemIcon.color = new Color(255, 255, 255, 0.0f);
        }
    }

    public static void useItem3(Transform throwPos)
    {
        itemIcon = GameObject.Find("ItemIcon_3").GetComponent<Image>();
        //如果有道具才能使用
        if (itemIcon.color.a == 1.0f)
        {
            ThrowItem(throwPos);
        }
        itemIcon.color = new Color(255, 255, 255, 0.0f);
    }


    public static void ThrowItem(Transform throwPos)
    {
        switch (itemIcon.sprite.name)
        {
            case "bomb":
                Debug.Log("丢炸弹");
                // 生成bomb

                GameObject bombObj = new GameObject("Mbomb");
                //bulletObj.layer = LayerMask.NameToLayer("Bullet");
                Bomb mbomb = bombObj.AddComponent<Bomb>();
                mbomb.Init();
                
                //位置
                mbomb.transform.position = throwPos.position;
                mbomb.transform.rotation = Quaternion.identity;

                //bombPrefab = Resources.Load("Prefabs/Items/Bomb", typeof(GameObject)) as GameObject;
                var bomb = MonoBehaviour.Instantiate(bombPrefab, throwPos.position, Quaternion.identity);
                break;
        }
    }
}
