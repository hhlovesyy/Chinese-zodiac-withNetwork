using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    
    private Rigidbody rb;

    [Header("发射设置")]
    public float force = 8.0f;
    //public Vector3 velocity;
    public float angle;//仰角
    [Header("使用效果")]
    public GameObject expPrefab;
    //public float expForce;
    public float radius;
    [Header("效果时长")]
    public float effectTime = 3.0f;
    private GameObject[] players;
    private GameObject player;
    public enum BombStates { HitOtherPlayer, HitNothing };
    private BombStates bombStates;

    private Vector3 direction;
    //发射者
    public BaseAnimal animal;
    //炮弹模型
    private GameObject skin;

    //物理
    Rigidbody rigidBody;
    public void Init()
    {
        //皮肤
        GameObject skinRes = ResManager.LoadPrefab("Bomb");
        skin = (GameObject)Instantiate(skinRes);
        Vector3 Firepos = new Vector3(0, 1.75f, 2.34f);
        skin.transform.parent = this.transform;
        skin.transform.localPosition = Firepos;
        skin.transform.localEulerAngles = Vector3.zero;
        //物理
        rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;
    }

    // Start is called before the first frame update
    void Start()
    {
       
        Debug.Log("enterBomb");
        rb = GetComponent<Rigidbody>();
        // 生成时，状态为攻击其他玩家
        bombStates = BombStates.HitOtherPlayer;
        players = GameObject.FindGameObjectsWithTag("Player"); //查找Player
        player = players[0];
        BeThrowed();
    }

    /// <summary>
    /// 根据已知仰角和初始速度发射
    /// </summary>
    public void BeThrowed()
    {
        // 仰角：45度
        direction = player.transform.forward.normalized + Mathf.Tan(angle) * Vector3.up;
        Debug.Log("投掷方向：" + direction);
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        // 爆炸效果
        GameObject exp = Instantiate(expPrefab, transform.position, Quaternion.identity);
        Destroy(exp,effectTime);
        switch (bombStates)
        {
            case BombStates.HitOtherPlayer:
                Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
                foreach (Collider nearby in colliders)
                {
                    if (nearby.gameObject.CompareTag("Player"))
                    {
                        Debug.Log("其他玩家眩晕！");
                        nearby.gameObject.GetComponent<Animator>().SetTrigger("BeBomb");
                        bombStates = BombStates.HitNothing;
                    }
                }
                break;
        }
        // 爆炸后消失
        Destroy(gameObject);
    }

    ///// <summary>
    ///// 对周边刚体产生推力
    ///// </summary>
    //void KnockBack()
    //{
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

    //    int num = 0;
    //    foreach(Collider nearby in colliders)
    //    {
    //        num += 1;
    //        Debug.Log("检测到碰撞体" + num);
    //        Rigidbody rigg = nearby.GetComponent<Rigidbody>();
    //        if (rigg != null)
    //        {
    //            rigg.AddExplosionForce(expForce, transform.position, radius);
    //        }
    //    }
    //}
}
