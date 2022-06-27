using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class Level
{
    public string name;
    public List<Room> rooms;
    
    [HideInInspector] public List<Room> southEntranceRooms = new List<Room>();
    [HideInInspector] public List<Room> eastEntranceRooms = new List<Room>();
    [HideInInspector] public List<Room> westEntranceRooms = new List<Room>();

}


[CreateAssetMenu(fileName = "RoomListSO", menuName = "ScriptableObjects/RoomListSO")]
public class RoomListSO : ScriptableObject
{
    public GameObject roomBasic;
    public List<Room> bossRooms;

    public List<Level> levels;
}
