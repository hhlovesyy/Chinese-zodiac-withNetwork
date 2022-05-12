using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleManager
{
    //战场中的游戏玩家
    public static Dictionary<string, BaseAnimal> animals = new Dictionary<string, BaseAnimal>();

    //初始化
    public static void Init()
    {
        //添加监听
        NetManager.AddMsgListener("MsgEnterBattle", OnMsgEnterBattle);
        NetManager.AddMsgListener("MsgBattleResult", OnMsgBattleResult);
        NetManager.AddMsgListener("MsgLeaveBattle", OnMsgLeaveBattle);
        NetManager.AddMsgListener("MsgSyncAnimal", OnMsgSyncAnimal);
        NetManager.AddMsgListener("MsgFire", OnMsgFire);
        NetManager.AddMsgListener("MsgHit", OnMsgHit);
        //NetManager.AddMsgListener("MsgAnimation", OnMsgAnimation);
    }

    //添加坦克
    public static void AddAnimal(string id, BaseAnimal animal)
    {
        animals[id] = animal;
    }//

    //删除坦克
    public static void RemoveAnimal(string id)
    {
        animals.Remove(id);
    }

    //获取坦克
    public static BaseAnimal GetAnimal(string id)
    {
        if (animals.ContainsKey(id))
        {
            return animals[id];
        }
        return null;
    }

    //获取玩家控制的坦克
    public static BaseAnimal GetCtrlAnimal()
    {
        return GetAnimal(GameMain.id);
    }

    //重置战场
    public static void Reset()
    {
        //场景
        foreach (BaseAnimal animal in animals.Values)
        {
            MonoBehaviour.Destroy(animal.gameObject);
        }
        //列表
        animals.Clear();
    }

    
    //开始战斗
    public static void EnterBattle(MsgEnterBattle msg)
    {
        //重置
        BattleManager.Reset();
        //关闭界面
        PanelManager.Close("RoomPanel");//可以放到房间系统的监听中


        //PanelManager.Open<MinimapPanel>();

        //PanelManager.Close("ResultPanel");
        //产生坦克
        for (int i = 0; i < msg.animals.Length; i++)
        {
            GenerateAnimal(msg.animals[i]);
        }
        //GameManager.settimebegin(Time.time);
        GameManager.timeBegin = Time.time;
    }

    //产生坦克
    public static void GenerateAnimal(AnimalInfo animalInfo)
    {
        //GameObject
        string objName = "Animal_" + animalInfo.id;
        GameObject animalObj = new GameObject(objName);
        //AddComponent
        BaseAnimal animal = animalObj.AddComponent<nouse>();
        
        //属性
        animal.camp = animalInfo.camp;
        animal.id = animalInfo.id;
        //init 鸡 牛 蛇 虎 狗
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
        //列表
        AddAnimal(animalInfo.id, animal);
    }

    

    //收到进入战斗协议
    public static void OnMsgEnterBattle(MsgBase msgBase)
    {
        MsgEnterBattle msg = (MsgEnterBattle)msgBase;
        EnterBattle(msg);
    }

    //收到战斗结束协议
    public static void OnMsgBattleResult(MsgBase msgBase)
    {
        MsgBattleResult msg = (MsgBattleResult)msgBase;
        //判断显示胜利还是失败
        bool isWin = false;
        BaseAnimal animal = GetCtrlAnimal();
        if (animal != null && animal.camp == msg.winCamp)
        {
            isWin = true;
        }
        //显示界面,后面再实现,自己赢和别人赢,显示的面板信息不一样
        //PanelManager.Open<ResultPanel>(isWin);
    }

    //收到玩家退出协议
    public static void OnMsgLeaveBattle(MsgBase msgBase)
    {
        MsgLeaveBattle msg = (MsgLeaveBattle)msgBase;
        //查找坦克
        BaseAnimal animal = GetAnimal(msg.id);
        if (animal == null)
        {
            return;
        }
        //删除坦克
        RemoveAnimal(msg.id);
        MonoBehaviour.Destroy(animal.gameObject);
    }

    //收到动画同步协议
    public static void OnMsgAnimation(MsgBase msgBase)
    {
        MsgAnimation msg = (MsgAnimation)msgBase;
        //不同步自己
        if (msg.id == GameMain.id)
            return;
        SyncAnimal animal = (SyncAnimal)GetAnimal(msg.id);
        if (animal == null)
        {
            return;
        }
        //移动同步
        animal.SyncAnim(msg);
    }

    //收到同步协议
    public static void OnMsgSyncAnimal(MsgBase msgBase)
    {
        MsgSyncAnimal msg = (MsgSyncAnimal)msgBase;
        //不同步自己
        if (msg.id == GameMain.id)
        {
            return;
        }
        //查找坦克
        SyncAnimal animal = (SyncAnimal)GetAnimal(msg.id);
        if (animal == null)
        {
            return;
        }
        //移动同步
        animal.SyncPos(msg);
    }

    //收到开火协议
    public static void OnMsgFire(MsgBase msgBase)
    {
        MsgFire msg = (MsgFire)msgBase;
        //不同步自己
        if (msg.id == GameMain.id)
        {
            return;
        }
        //查找坦克
        SyncAnimal animal = (SyncAnimal)GetAnimal(msg.id);
        if (animal == null)
        {
            return;
        }
        //开火
        animal.SyncFire(msg);
    }

    //收到击中协议
    public static void OnMsgHit(MsgBase msgBase)
    {
        MsgHit msg = (MsgHit)msgBase;
        //查找坦克
        BaseAnimal animal = GetAnimal(msg.targetId);
        if (animal == null)
        {
            return;
        }
        //bool isDie = animal.IsDie();
        //被击中
        animal.Attacked(msg.damage);
        //击杀提示
        //if (!isDie && animal.IsDie() && msg.id == GameMain.id)
        //{
        //    PanelManager.Open<KillPanel>();
        //}
    }

}
