using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleManager
{
    //ս���е���Ϸ���
    public static Dictionary<string, BaseAnimal> animals = new Dictionary<string, BaseAnimal>();

    //��ʼ��
    public static void Init()
    {
        //���Ӽ���
        NetManager.AddMsgListener("MsgEnterBattle", OnMsgEnterBattle);
        NetManager.AddMsgListener("MsgBattleResult", OnMsgBattleResult);
        NetManager.AddMsgListener("MsgLeaveBattle", OnMsgLeaveBattle);
        NetManager.AddMsgListener("MsgSyncAnimal", OnMsgSyncAnimal);
        NetManager.AddMsgListener("MsgFire", OnMsgFire);
        NetManager.AddMsgListener("MsgHit", OnMsgHit);
        //NetManager.AddMsgListener("MsgAnimation", OnMsgAnimation);
    }

    //����̹��
    public static void AddAnimal(string id, BaseAnimal animal)
    {
        animals[id] = animal;
    }//

    //ɾ��̹��
    public static void RemoveAnimal(string id)
    {
        animals.Remove(id);
    }

    //��ȡ̹��
    public static BaseAnimal GetAnimal(string id)
    {
        if (animals.ContainsKey(id))
        {
            return animals[id];
        }
        return null;
    }

    //��ȡ��ҿ��Ƶ�̹��
    public static BaseAnimal GetCtrlAnimal()
    {
        return GetAnimal(GameMain.id);
    }

    //����ս��
    public static void Reset()
    {
        //����
        foreach (BaseAnimal animal in animals.Values)
        {
            MonoBehaviour.Destroy(animal.gameObject);
        }
        //�б�
        animals.Clear();
    }

    
    //��ʼս��
    public static void EnterBattle(MsgEnterBattle msg)
    {
        //����
        BattleManager.Reset();
        //�رս���
        PanelManager.Close("RoomPanel");//���Էŵ�����ϵͳ�ļ�����


        //PanelManager.Open<MinimapPanel>();

        //PanelManager.Close("ResultPanel");
        //����̹��
        for (int i = 0; i < msg.animals.Length; i++)
        {
            GenerateAnimal(msg.animals[i]);
        }
        //GameManager.settimebegin(Time.time);
        GameManager.timeBegin = Time.time;
    }

    //����̹��
    public static void GenerateAnimal(AnimalInfo animalInfo)
    {
        //GameObject
        string objName = "Animal_" + animalInfo.id;
        GameObject animalObj = new GameObject(objName);
        //AddComponent
        BaseAnimal animal = animalObj.AddComponent<nouse>();
        
        //����
        animal.camp = animalInfo.camp;
        animal.id = animalInfo.id;
        //init �� ţ �� �� ��
        if (animalInfo.camp == 1)
        {
            animal.Init("RoosterPlayer");
        }
        else if (animalInfo.camp == 2)
        {
            animal.Init("BullPlayer");
        }
        else if (animalInfo.camp == 3)
        {
            animal.Init("SnakePlayer");
        }
        else if (animalInfo.camp == 4)
        {
            animal.Init("TigerPlayer");
        }
        else if (animalInfo.camp == 5)
        {
            animal.Init("Dog");
        }

        if (animalInfo.id == GameMain.id)
        {
            //animal = animalObj.AddComponent<CtrlAnimal>();
            animal = animalObj.GetComponentInChildren<CharacterController>().gameObject.AddComponent<CtrlAnimal>();
            animalObj.GetComponentInChildren<CharacterController>().gameObject.GetComponent<PlayerInput>().enabled = true;
            animalObj.GetComponentInChildren<CinemachineVirtualCamera>().enabled = true;
            animalObj.GetComponentInChildren<CharacterController>().gameObject.AddComponent<playerTrigger>();

            animalObj.GetComponentInChildren<CharacterController>().gameObject.AddComponent<PlayerController>();

            //GameObject itemManager= animalObj.GetComponent<PlayerController>().GetComponent<ItemManager>();
            //GameObject itemmanaGameObj = animalObj.GetComponentInChildren<ItemManager>();
            ////itemManager = GameObject.Find("ItemManager");
            ///

            GameObject minimap = GameObject.Find("UI_MiniMap");
            MiniMapFollowController miniMapFollowController = minimap.GetComponent<MiniMapFollowController>();
            miniMapFollowController.player = animal.transform;



        }
        else
        {
            //animal = animalObj.AddComponent<SyncAnimal>();
            animal = animalObj.GetComponentInChildren<CharacterController>().gameObject.AddComponent<SyncAnimal>();
        }
        //camera
        //if (animalInfo.id == GameMain.id)
        //{
        //    CameraFollow cf = animalObj.GetComponentInChildren<CharacterController>().gameObject.AddComponent<CameraFollow>();
        //}




        //pos rotation
        Vector3 pos = new Vector3(animalInfo.x, animalInfo.y, animalInfo.z);
        Vector3 rot = new Vector3(animalInfo.ex, animalInfo.ey, animalInfo.ez);
        animal.transform.position = pos;
        animal.transform.eulerAngles = rot;
        //�б�
        AddAnimal(animalInfo.id, animal);
    }

    

    //�յ�����ս��Э��
    public static void OnMsgEnterBattle(MsgBase msgBase)
    {
        MsgEnterBattle msg = (MsgEnterBattle)msgBase;
        EnterBattle(msg);
    }

    //�յ�ս������Э��
    public static void OnMsgBattleResult(MsgBase msgBase)
    {
        MsgBattleResult msg = (MsgBattleResult)msgBase;
        //�ж���ʾʤ������ʧ��
        bool isWin = false;
        BaseAnimal animal = GetCtrlAnimal();
        if (animal != null && animal.camp == msg.winCamp)
        {
            isWin = true;
        }
        //��ʾ����,������ʵ��,�Լ�Ӯ�ͱ���Ӯ,��ʾ�������Ϣ��һ��
        //PanelManager.Open<ResultPanel>(isWin);
    }

    //�յ�����˳�Э��
    public static void OnMsgLeaveBattle(MsgBase msgBase)
    {
        MsgLeaveBattle msg = (MsgLeaveBattle)msgBase;
        //����̹��
        BaseAnimal animal = GetAnimal(msg.id);
        if (animal == null)
        {
            return;
        }
        //ɾ��̹��
        RemoveAnimal(msg.id);
        MonoBehaviour.Destroy(animal.gameObject);
    }

    //�յ�����ͬ��Э��
    public static void OnMsgAnimation(MsgBase msgBase)
    {
        MsgAnimation msg = (MsgAnimation)msgBase;
        //��ͬ���Լ�
        if (msg.id == GameMain.id)
            return;
        SyncAnimal animal = (SyncAnimal)GetAnimal(msg.id);
        if (animal == null)
        {
            return;
        }
        //�ƶ�ͬ��
        animal.SyncAnim(msg);
    }

    //�յ�ͬ��Э��
    public static void OnMsgSyncAnimal(MsgBase msgBase)
    {
        MsgSyncAnimal msg = (MsgSyncAnimal)msgBase;
        //��ͬ���Լ�
        if (msg.id == GameMain.id)
        {
            return;
        }
        //����̹��
        SyncAnimal animal = (SyncAnimal)GetAnimal(msg.id);
        if (animal == null)
        {
            return;
        }
        //�ƶ�ͬ��
        animal.SyncPos(msg);
    }

    //�յ�����Э��
    public static void OnMsgFire(MsgBase msgBase)
    {
        MsgFire msg = (MsgFire)msgBase;
        //��ͬ���Լ�
        if (msg.id == GameMain.id)
        {
            return;
        }
        //����̹��
        SyncAnimal animal = (SyncAnimal)GetAnimal(msg.id);
        if (animal == null)
        {
            return;
        }
        //����
        animal.SyncFire(msg);
    }

    //�յ�����Э��
    public static void OnMsgHit(MsgBase msgBase)
    {
        MsgHit msg = (MsgHit)msgBase;
        //����̹��
        BaseAnimal animal = GetAnimal(msg.targetId);
        if (animal == null)
        {
            return;
        }
        //bool isDie = animal.IsDie();
        //������
        animal.Attacked(msg.damage);
        //��ɱ��ʾ
        //if (!isDie && animal.IsDie() && msg.id == GameMain.id)
        //{
        //    PanelManager.Open<KillPanel>();
        //}
    }

}