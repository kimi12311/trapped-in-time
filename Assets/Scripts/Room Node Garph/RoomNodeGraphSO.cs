using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomNodeGraph", menuName = "Scriptable Objects/Room/Room Node Graph")]
public class RoomNodeGraphSO : ScriptableObject    
{
    [HideInInspector] public RoomNodeTypeListSO roomNodeTypeList;
    [HideInInspector] public List<RoomNodeSO> roomList = new();
    [HideInInspector] public Dictionary<string, RoomNodeSO> roomNodeDictionary = new();
}
