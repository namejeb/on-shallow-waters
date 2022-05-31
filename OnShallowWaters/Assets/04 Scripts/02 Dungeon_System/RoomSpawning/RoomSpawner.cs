using UnityEngine;
using System.Collections.Generic;


public enum RoomEntranceDir
{
    SOUTH,
    EAST
}

[System.Serializable]
public class Room
{
    public GameObject roomPrefab;
    public RoomEntranceDir roomEntranceDir;
}

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private RoomListSO roomListSo;
    
    private List<Room> southEntranceRooms = new List<Room>();
    private List<Room> eastEntranceRooms = new List<Room>();
    
    private Dictionary<RoomEntranceDir, List<Room>> roomDict = new Dictionary<RoomEntranceDir, List<Room>>();

    private void Awake()
    {
        SortRooms();
    }

    private void SortRooms()
    {
        foreach(Room room in roomListSo.rooms){
            if (room.roomEntranceDir == RoomEntranceDir.SOUTH)
            {
                southEntranceRooms.Add(room);
            }
            else
            {
                eastEntranceRooms.Add(room);
            }
        }
        
        roomDict.Add(RoomEntranceDir.SOUTH, southEntranceRooms);
        roomDict.Add(RoomEntranceDir.EAST, eastEntranceRooms);
    }

    private void SpawnRoom(RoomEntranceDir dir)
    {
        GameObject room = null;
        int roomIndex = 0;
        
        if (dir == RoomEntranceDir.SOUTH)
        {
            roomIndex = Random.Range(0, southEntranceRooms.Count);
            room = southEntranceRooms[roomIndex].roomPrefab;
        }
        else
        {
            roomIndex = Random.Range(0, eastEntranceRooms.Count);
            room = eastEntranceRooms[roomIndex].roomPrefab;
        }

        Transform roomTransform = room.transform;
        Instantiate(room, roomTransform.position, roomTransform.rotation);
    }
}