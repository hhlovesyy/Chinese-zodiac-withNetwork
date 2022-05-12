using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public string sceneFrom;
    public string sceneToGo;

    public void TransitionToScene(bool isMain)
    {
        Debug.Log("Enter TransitionToScene" + isMain);
        TransitionManager.Instance.Transition(sceneFrom, sceneToGo, isMain);
    }
}
