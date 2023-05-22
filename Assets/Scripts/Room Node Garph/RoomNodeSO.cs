using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class RoomNodeSO : ScriptableObject
{
    [HideInInspector] public string id;
    [HideInInspector] public List<string> parentRoomNodeIDList = new List<string>();
    [HideInInspector] public List<string> childRoomNodeIDList = new List<string>();
    [HideInInspector] public RoomNodeGraphSO roomNodeGraph;
    public RoomNodeTypeSO roomNodeType;
    [HideInInspector] public RoomNodeTypeListSO RoomNodeTypeList;

    #region Editor
        #if UNITY_EDITOR
        [HideInInspector] public Rect rect;
        [HideInInspector] public bool isLeftMouseDrag;
        [HideInInspector] public bool isSelected;
        public void Initialize(Rect rectObject, RoomNodeGraphSO nodeGraph, RoomNodeTypeSO type)
        {
            rect = rectObject;
            id = Guid.NewGuid().ToString();
            name = "Node";
            roomNodeGraph = nodeGraph;
            roomNodeType = type;
            RoomNodeTypeList = GameResources.Instance.RoomNodeTypeList;
        }

        public void Draw(GUIStyle style)
        {
            GUILayout.BeginArea(rect, style);
            EditorGUI.BeginChangeCheck();
            var selection = EditorGUILayout.Popup("", RoomNodeTypeList.list.FindIndex(x => x == roomNodeType), 
                GetNames());
            roomNodeType = RoomNodeTypeList.list[selection];
            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(this);
            GUILayout.EndArea();
        }

        private string[] GetNames()
        {
            var rooms = new string[RoomNodeTypeList.list.Count];
            for (var i = 0; i < RoomNodeTypeList.list.Count; i++)
            {
                if (RoomNodeTypeList.list[i].displayInNodeGraphEditor)
                {
                    rooms[i] = RoomNodeTypeList.list[i].roomNodeTypeName;
                }
            }
            return rooms;
        }

        public void ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    ProcessMouseDown(e);
                    break;
                case EventType.MouseUp:
                    ProcessMouseUp(e);
                    break;
                case EventType.MouseDrag:
                    ProcessMouseDrag(e);
                    break;
            }
        }

        private void ProcessMouseDown(Event e)
        {
            if (e.button == 0)
            {
                // Selection.activeObject = this;
                isSelected = !isSelected;
            }
        }

        private void ProcessMouseUp(Event e)
        {
            if (e.button == 0)
            {
                if (isLeftMouseDrag)
                {
                    isLeftMouseDrag = false;
                }
            }
        }

        private void ProcessMouseDrag(Event e)
        {
            if (e.button == 0)
            {
                isLeftMouseDrag = true;
                DragNode(e.delta);
                GUI.changed = true;
            }
        }

        private void DragNode(Vector2 mouse)
        {
            rect.position += mouse;
            EditorUtility.SetDirty(this);
        }
        #endif
    #endregion
}
