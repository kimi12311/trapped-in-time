using System;
using System.Linq;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEditor.Callbacks;

public class RoomNodeGraph : EditorWindow
{
    private GUIStyle nodeStyle;
    private int nodePadding = 25;
    private int nodeBorder = 12;
    private const float NodeHeight = 75f;
    private const float NodeWidth = 160f;
    private RoomNodeSO currentRoomNode = null;
    private static RoomNodeGraphSO currentRoomNodeGraph;
    private static RoomNodeTypeListSO roomNodeTypeList;

    [MenuItem("Room Editor", menuItem = "Window/RoomEditor/Room Editor")]

    private static void OpenWindow()
    {
        GetWindow<RoomNodeGraph>("Room Graph Editor");
    }
    
    private void OnEnable()
    {
        nodeStyle = new GUIStyle
        {
            normal =
            {
                background = EditorGUIUtility.Load("node5") as Texture2D,
                textColor = Color.white
            },
            padding = new RectOffset(nodePadding, nodePadding, nodePadding, nodePadding),
            border = new RectOffset(nodeBorder, nodeBorder, nodeBorder, nodeBorder)
        };
        roomNodeTypeList = GameResources.Instance.RoomNodeTypeList;
    }
    
    [OnOpenAsset(0)]
    public static bool OnDoubleClickAsset(int id, int line)
    {
        var roomNodeGraph = EditorUtility.InstanceIDToObject(id) as RoomNodeGraphSO;
        if (roomNodeGraph == null) return false;
        OpenWindow();
        currentRoomNodeGraph = roomNodeGraph;
        return true;
    }

    private void OnGUI()
    {
        if (currentRoomNodeGraph is not null)
        {
            ProcessEvents(Event.current);
            DrawRoomNodes();
        }

        if (GUI.changed)
            Repaint();
    }

    private void ProcessEvents(Event current)
    {
        if (currentRoomNode is null || currentRoomNode.isLeftMouseDrag == false)
        {
            currentRoomNode = IsMouseOverNode(current);
        }

        if (currentRoomNode is null)
        {
            ProcessRoomNodeGraphEvents(current);
        }
        else
        {
            currentRoomNode.ProcessEvents(current);
        }
    }

    private RoomNodeSO IsMouseOverNode(Event e)
    {
        return currentRoomNodeGraph.roomList.FirstOrDefault(t => t.rect.Contains(e.mousePosition));
    }

    private void ProcessRoomNodeGraphEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                ProcessMouseDownEvent(e);
                break;
        }
    }

    private void ProcessMouseDownEvent(Event e)
    {
        if (e.button == 1)
        {
            ShowContextMenu(e.mousePosition);
        }
    }

    private void ShowContextMenu(Vector2 mousePos)
    {
        var contextMenu = new GenericMenu();
        contextMenu.AddItem(new GUIContent("Add Node"), false, CreateRoomNode, mousePos);
        contextMenu.ShowAsContext();
    }

    private void CreateRoomNode(object mousePosObj)
    {
        CreateRoomNode(mousePosObj, roomNodeTypeList.list.Find(x => x.isNone));
    }

    private void CreateRoomNode(object mousePosObj, RoomNodeTypeSO nodeType)
    {
        var mousePos = (Vector2)mousePosObj;
        var roomNode = ScriptableObject.CreateInstance<RoomNodeSO>();
        currentRoomNodeGraph.roomList.Add(roomNode);
        roomNode.Initialize(new Rect(mousePos, new Vector2(NodeWidth, NodeHeight)), currentRoomNodeGraph, nodeType);
        AssetDatabase.AddObjectToAsset(roomNode, currentRoomNodeGraph);
        AssetDatabase.SaveAssets();
    }

    private void DrawRoomNodes()
    {
        foreach (var node in currentRoomNodeGraph.roomList)
        {
            node.Draw(nodeStyle);
        }

        GUI.changed = true;
    }
}
