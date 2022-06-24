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
        Transform playerSpawnPoint = room.Find("refs").Find("ref_Entrance");
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
    public static event Action OnRoomChangeStart;
    public static event Action OnRoomChangeFinish;  
    public static event Action<Transform> OnResetPlayerPos;
    
    private int _roomFinishedCount = -1; //to exclude basic room
    private int _bossRoomsIndex;


    private void OnDestroy()
    {
        ExitRoomTrigger.OnExitRoom -= SpawnRoom;
    }
    
    private void Awake()
    {
        SortRooms();
        ExitRoomTrigger.OnExitRoom += SpawnRoom;

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
        //after 5 rooms, spawn boss
        bool isBossStage = (_roomFinishedCount == 5);
        HandleSpawnRoom(isBossStage, dir);
    }

    private void HandleSpawnRoom(bool isBossStage, RoomEntranceDir dir)
    {
        if(OnRoomChangeStart != null) OnRoomChangeStart.Invoke();
        
        Room room = null;
        
        if (isBossStage)
        {
            _roomFinishedCount = -1;
           
            room = roomListSo.bossRooms[_bossRoomsIndex];
            if (_bossRoomsIndex + 1 < roomListSo.bossRooms.Count) _bossRoomsIndex++;
        }
        else
        {
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
            //room = southEntranceRooms[1];
        }
        StartCoroutine(SpawnNewRoom(room));
    }

    private IEnumerator SpawnNewRoom(Room room)
    {
        yield return new WaitForSeconds(1f);
        
        //Remove old room
        Destroy(_prevRoom.gameObject);
        _roomFinishedCount++;
        
        //Spawn new room
        Transform roomTransform = room.roomPrefab.transform;
        _prevRoom = Instantiate(roomTransform, roomTransform.position, roomTransform.rotation);
        
        //Set player position to spawn point
        if (OnResetPlayerPos != null) OnResetPlayerPos.Invoke(Room.FindSpawnPoint(_prevRoom));
        
        if (OnRoomChangeFinish != null) OnRoomChangeFinish.Invoke();
    }
}