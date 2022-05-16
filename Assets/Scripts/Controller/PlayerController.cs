//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class PlayerController : MonoBehaviour
//{
//    [Header("道具")]
//    public Transform throwPos;
//    //public float attackRadius;//攻击范围，暂时先放着
//    //public GameObject attackTarget;//攻击对象
//    public ItemManager itemManager;

//    private Animator anim;

//    [HideInInspector]
//    public bool isDizzy = false;

//    void Start()
//    {
//        anim = GetComponent<Animator>();
//        Debug.Log("throwpos:" + throwPos.position);
//    }

//    /// <summary>
//    /// 当角色在眩晕时，无法行动
//    /// </summary>
//    public void GetCurrentState()
//    {
//        AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(0);
//        if (stateinfo.IsName("Death"))
//        {
//            isDizzy = true;
//        }
//        else
//        {
//            isDizzy = false;
//        }
//    }

//    /// <summary>
//    /// 获取玩家输入
//    /// </summary>
//    void Update()
//    {
//        GetCurrentState();
//        //Vector3 dir = transform.forward;
//        //Debug.Log("前进方向：" + dir);

//        if (!isDizzy)
//        {
//            // Q：普通攻击
//            //if (Input.GetKeyDown(KeyCode.Q))
//            //{
//            //    ThrowRock();
//            //}
//            // 1：使用道具1
//            if (Input.GetKeyDown(KeyCode.Alpha1))
//            {
//                itemManager.useItem1(throwPos);
//            }
//            // 2：使用道具2
//            else if (Input.GetKeyDown(KeyCode.Alpha2))
//            {
//                itemManager.useItem2(throwPos);
//            }
//            // 3：使用道具3
//            else if (Input.GetKeyDown(KeyCode.Alpha3))
//            {
//                itemManager.useItem3(throwPos);
//            }
//        }

//    }

//    //public void FindAttackTarget()
//    //{

//    //}

//    public void ThrowRock()
//    {
//        Debug.Log("丢石头");
//        // 生成rock
//        var rock = Instantiate(itemManager.rockPrefab, throwPos.position, Quaternion.identity);
//        // 设置攻击目标：距离玩家某个范围内的角色？可能需要一个flag区分是否为角色。
//        //FindAttackTarget();
//        //rock.GetComponent<Rock>().target = attackTarget;
//        //float distance = Vector3.Distance(attackTarget.transform.position, transform.position);
//        //Debug.Log("与攻击对象间的距离：" + distance);
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        // 碰到道具，拾取（显示在UI）
//        if (other.gameObject.CompareTag("ItemBox"))
//        {
//            Debug.Log("拾取道具！");
//            other.gameObject.GetComponent<ItemBox>().DestorySelf();
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("道具")]
    public Transform throwPos;
    //public float attackRadius;//攻击范围，暂时先放着
    //public GameObject attackTarget;//攻击对象

    private Animator anim;

    [HideInInspector]
    public bool isDizzy = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        Debug.Log("throwpos:" + throwPos.position);
    }

    /// <summary>
    /// 当角色在眩晕时，无法行动
    /// </summary>
    public void GetCurrentState()
    {
        AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateinfo.IsName("Death"))
        {
            isDizzy = true;
        }
        else
        {
            isDizzy = false;
        }
    }

    /// <summary>
    /// 获取玩家输入
    /// </summary>
    void Update()
    {
        GetCurrentState();
        //Vector3 dir = transform.forward;
        //Debug.Log("前进方向：" + dir);

        if (!isDizzy)
        {
            // Q：普通攻击
            //if (Input.GetKeyDown(KeyCode.Q))
            //{
            //    ThrowRock();
            //}
            // 1：使用道具1
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ItemManager.useItem1(throwPos);
            }
            // 2：使用道具2
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ItemManager.useItem2(throwPos);
            }
            // 3：使用道具3
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ItemManager.useItem3(throwPos);
            }
        }

    }

    //public void FindAttackTarget()
    //{

    //}

    public void ThrowRock()
    {
        Debug.Log("丢石头");
        // 生成rock
        //var rock = Instantiate(ItemManager.rockPrefab, throwPos.position, Quaternion.identity);
        // 设置攻击目标：距离玩家某个范围内的角色？可能需要一个flag区分是否为角色。
        //FindAttackTarget();
        //rock.GetComponent<Rock>().target = attackTarget;
        //float distance = Vector3.Distance(attackTarget.transform.position, transform.position);
        //Debug.Log("与攻击对象间的距离：" + distance);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 碰到道具，拾取（显示在UI）
        if (other.gameObject.CompareTag("ItemBox"))
        {
            Debug.Log("拾取道具！");
            other.gameObject.GetComponent<ItemBox>().DestorySelf();
        }
       
    }
}

