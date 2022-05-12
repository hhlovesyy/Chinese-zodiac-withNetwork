using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    
    private Rigidbody rb;

    [Header("��������")]
    public float force = 8.0f;
    //public Vector3 velocity;
    public float angle;//����
    [Header("ʹ��Ч��")]
    public GameObject expPrefab;
    //public float expForce;
    public float radius;
    [Header("Ч��ʱ��")]
    public float effectTime = 3.0f;
    private GameObject[] players;
    private GameObject player;
    public enum BombStates { HitOtherPlayer, HitNothing };
    private BombStates bombStates;

    private Vector3 direction;
    //������
    public BaseAnimal animal;
    //�ڵ�ģ��
    private GameObject skin;

    //����
    Rigidbody rigidBody;
    public void Init()
    {
        //Ƥ��
        GameObject skinRes = ResManager.LoadPrefab("Bomb");
        skin = (GameObject)Instantiate(skinRes);
        Vector3 Firepos = new Vector3(0, 1.75f, 2.34f);
        skin.transform.parent = this.transform;
        skin.transform.localPosition = Firepos;
        skin.transform.localEulerAngles = Vector3.zero;
        //����
        rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;
    }

    // Start is called before the first frame update
    void Start()
    {
       
        Debug.Log("enterBomb");
        rb = GetComponent<Rigidbody>();
        // ����ʱ��״̬Ϊ�����������
        bombStates = BombStates.HitOtherPlayer;
        players = GameObject.FindGameObjectsWithTag("Player"); //����Player
        player = players[0];
        BeThrowed();
    }

    /// <summary>
    /// ������֪���Ǻͳ�ʼ�ٶȷ���
    /// </summary>
    public void BeThrowed()
    {
        // ���ǣ�45��
        direction = player.transform.forward.normalized + Mathf.Tan(angle) * Vector3.up;
        Debug.Log("Ͷ������" + direction);
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        // ��ըЧ��
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
                        Debug.Log("�������ѣ�Σ�");
                        nearby.gameObject.GetComponent<Animator>().SetTrigger("BeBomb");
                        bombStates = BombStates.HitNothing;
                    }
                }
                break;
        }
        // ��ը����ʧ
        Destroy(gameObject);
    }

    ///// <summary>
    ///// ���ܱ߸����������
    ///// </summary>
    //void KnockBack()
    //{
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

    //    int num = 0;
    //    foreach(Collider nearby in colliders)
    //    {
    //        num += 1;
    //        Debug.Log("��⵽��ײ��" + num);
    //        Rigidbody rigg = nearby.GetComponent<Rigidbody>();
    //        if (rigg != null)
    //        {
    //            rigg.AddExplosionForce(expForce, transform.position, radius);
    //        }
    //    }
    //}
}
