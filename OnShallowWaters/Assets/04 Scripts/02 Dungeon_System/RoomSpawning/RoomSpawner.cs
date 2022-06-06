using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public enum RoomEntranceDir
{
    SOUTH,
    EAST,
    WEST
}

[Serializable]
public class Room
{
    public GameObject roomPrefab;
    public RoomEntranceDir roomEntranceDir;

    
    public static Transform FindSpawnPoint(Transform room)
    {
        Transform playerSpawnPoint = room.Find("Entrance temp");
        return playerSpawnPoint;
    }
}

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private RoomListSO roomListSo;
    
    private List<Room> southEntranceRooms = new List<Room>();
    private List<Room> eastEntranceRooms = new List<Room>();
    private List<Room> westEntranceRooms = new List<Room>();
    
    private Dictionary<RoomEntranceDir, List<Room>> roomDict = new Dictionary<RoomEntranceDir, List<Room>>();

    private static Transform _prevRoom;
    public static event Action<Transform> OnRoomChanged;


    private void OnDestroy()
    {
        Boon.OnChangeRoom -= SpawnRoom;
    }
    
    private void Awake()
    {
        SortRooms();
        Boon.OnChangeRoom += SpawnRoom;

        GameObject roomBasic = roomListSo.roomBasic;
        _prevRoom = Instantiate(roomBasic.transform, roomBasic.transform.position, roomBasic.transform.rotation);
    }

    private void SortRooms()
    {
        foreach(Room room in roomListSo.rooms)
        {
            if (room.roomEntranceDir == RoomEntranceDir.SOUTH)
            {
                southEntranceRooms.Add(room);
            }
            else if (room.roomEntranceDir == RoomEntranceDir.EAST)
            {
                eastEntranceRooms.Add(room);
            }
            else
            {
                westEntranceRooms.Add(room);
            }
        }
        
        roomDict.Add(RoomEntranceDir.SOUTH, southEntranceRooms);
        roomDict.Add(RoomEntranceDir.EAST, eastEntranceRooms);
        roomDict.Add(RoomEntranceDir.WEST, westEntranceRooms);
    }

    private void SpawnRoom(RoomEntranceDir dir)
    {
        Room room = null;
        int roomIndex = 0;
        
        if (dir == RoomEntranceDir.SOUTH)
        {
            roomIndex = UnityEngine.Random.Range(0, southEntranceRooms.Count);
            room = southEntranceRooms[roomIndex];
        }
        else if (dir == RoomEntranceDir.EAST)
        {
            roomIndex = UnityEngine.Random.Range(0, eastEntranceRooms.Count);
            room = eastEntranceRooms[roomIndex];
        }
        else
        {
            roomIndex = UnityEngine.Random.Range(0, westEntranceRooms.Count);
            room = westEntranceRooms[roomIndex];
        }
        
   
        //Remove old room
        Destroy(_prevRoom.gameObject);
        
        //Spawn new room
        Transform roomTransform = room.roomPrefab.transform;
        _prevRoom = Instantiate(roomTransform, roomTransform.position, roomTransform.rotation);

        //Set player position to spawn point
        if (OnRoomChanged != null)
        {
            OnRoomChanged.Invoke(Room.FindSpawnPoint(_prevRoom));
        }
    }
}