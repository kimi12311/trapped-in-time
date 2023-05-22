using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomNodeTypeListSO", menuName = "Scriptable Objects/Room/Room Node Type List")]
public class RoomNodeTypeListSO : ScriptableObject
{
    #region Header

    [Space(10)] [Header("Room Type List")]

    #endregion
    public List<RoomNodeTypeSO> list;
    #region Validation

#if UNITY_EDITOR
            private void OnValidate()
            {
                HelperUtilities.ValidateEnumerableValues(this, nameof(list), list);
            }
#endif

    #endregion
}