//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class PlayerController : MonoBehaviour
//{
//    [Header("����")]
//    public Transform throwPos;
//    //public float attackRadius;//������Χ����ʱ�ȷ���
//    //public GameObject attackTarget;//��������
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
//    /// ����ɫ��ѣ��ʱ���޷��ж�
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
//    /// ��ȡ�������
//    /// </summary>
//    void Update()
//    {
//        GetCurrentState();
//        //Vector3 dir = transform.forward;
//        //Debug.Log("ǰ������" + dir);

//        if (!isDizzy)
//        {
//            // Q����ͨ����
//            //if (Input.GetKeyDown(KeyCode.Q))
//            //{
//            //    ThrowRock();
//            //}
//            // 1��ʹ�õ���1
//            if (Input.GetKeyDown(KeyCode.Alpha1))
//            {
//                itemManager.useItem1(throwPos);
//            }
//            // 2��ʹ�õ���2
//            else if (Input.GetKeyDown(KeyCode.Alpha2))
//            {
//                itemManager.useItem2(throwPos);
//            }
//            // 3��ʹ�õ���3
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
//        Debug.Log("��ʯͷ");
//        // ����rock
//        var rock = Instantiate(itemManager.rockPrefab, throwPos.position, Quaternion.identity);
//        // ���ù���Ŀ�꣺�������ĳ����Χ�ڵĽ�ɫ��������Ҫһ��flag�����Ƿ�Ϊ��ɫ��
//        //FindAttackTarget();
//        //rock.GetComponent<Rock>().target = attackTarget;
//        //float distance = Vector3.Distance(attackTarget.transform.position, transform.position);
//        //Debug.Log("�빥�������ľ��룺" + distance);
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        // �������ߣ�ʰȡ����ʾ��UI��
//        if (other.gameObject.CompareTag("ItemBox"))
//        {
//            Debug.Log("ʰȡ���ߣ�");
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
    [Header("����")]
    public Transform throwPos;
    //public float attackRadius;//������Χ����ʱ�ȷ���
    //public GameObject attackTarget;//��������

    private Animator anim;

    [HideInInspector]
    public bool isDizzy = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        Debug.Log("throwpos:" + throwPos.position);
    }

    /// <summary>
    /// ����ɫ��ѣ��ʱ���޷��ж�
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
    /// ��ȡ�������
    /// </summary>
    void Update()
    {
        GetCurrentState();
        //Vector3 dir = transform.forward;
        //Debug.Log("ǰ������" + dir);

        if (!isDizzy)
        {
            // Q����ͨ����
            //if (Input.GetKeyDown(KeyCode.Q))
            //{
            //    ThrowRock();
            //}
            // 1��ʹ�õ���1
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ItemManager.useItem1(throwPos);
            }
            // 2��ʹ�õ���2
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ItemManager.useItem2(throwPos);
            }
            // 3��ʹ�õ���3
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
        Debug.Log("��ʯͷ");
        // ����rock
        //var rock = Instantiate(ItemManager.rockPrefab, throwPos.position, Quaternion.identity);
        // ���ù���Ŀ�꣺�������ĳ����Χ�ڵĽ�ɫ��������Ҫһ��flag�����Ƿ�Ϊ��ɫ��
        //FindAttackTarget();
        //rock.GetComponent<Rock>().target = attackTarget;
        //float distance = Vector3.Distance(attackTarget.transform.position, transform.position);
        //Debug.Log("�빥�������ľ��룺" + distance);
    }

    private void OnTriggerEnter(Collider other)
    {
        // �������ߣ�ʰȡ����ʾ��UI��
        if (other.gameObject.CompareTag("ItemBox"))
        {
            Debug.Log("ʰȡ���ߣ�");
            other.gameObject.GetComponent<ItemBox>().DestorySelf();
        }
       
    }
}

