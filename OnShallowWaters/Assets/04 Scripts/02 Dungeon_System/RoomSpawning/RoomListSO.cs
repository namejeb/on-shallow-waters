using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "RoomListSO", menuName = "ScriptableObjects/RoomListSO")]
public class RoomListSO : ScriptableObject
{
    public List<Room> rooms;
}
