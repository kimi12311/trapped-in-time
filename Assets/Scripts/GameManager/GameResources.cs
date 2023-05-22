using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    private static GameResources instance;

    public static GameResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources");
            }
            return instance;
        }
    }
    
    #region Header Room List
    [Space(10)]
    [Header("Room")]
    #endregion
    #region Tooltip
        [Tooltip("Populate with your room RoomTypeListSO")]
    #endregion

    public RoomNodeTypeListSO RoomNodeTypeList;
}
