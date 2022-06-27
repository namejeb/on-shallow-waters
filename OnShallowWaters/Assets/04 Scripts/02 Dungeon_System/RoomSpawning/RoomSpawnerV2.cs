using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using Random = System.Random;

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

public class RoomSpawnerV2 : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float soulShopSpawnRate;
    
    [SerializeField] private Transform rBasic;
    [SerializeField] private List<Room> bossRooms;
    [SerializeField] private List<Level> levelList = new List<Level>();
    private int _levelCounter = 0;
    
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
        
         SetRoomActive(rBasic.transform, true);
         _prevRoom = rBasic.transform;
    }

    private void SortRooms()
    {
        for (int i = 0; i < levelList.Count; i++)
        {
            Level currLevel = levelList[i];
            
            currLevel.southEntranceRooms.Clear();
            currLevel.eastEntranceRooms.Clear();
            currLevel.westEntranceRooms.Clear();
            
            for (int j = 0; j < levelList[i].rooms.Count; j++)
            {
                Room room = levelList[i].rooms[j];
                
                if (room.roomEntranceDir == RoomEntranceDir.SOUTH) { currLevel.southEntranceRooms.Add(room); }
                else if (room.roomEntranceDir == RoomEntranceDir.EAST) { currLevel.eastEntranceRooms.Add(room); }
                else  { currLevel.westEntranceRooms.Add(room); }
                
                room.roomPrefab.SetActive(false);
            }
        }

        for (int i = 0; i < bossRooms.Count; i++)
        {
            bossRooms[i].roomPrefab.SetActive(false);
        }
    }

    private void SpawnRoom(RoomEntranceDir dir)
    {
        //after 5 rooms, spawn boss
        bool isBossStage = (_roomFinishedCount == 1); 
       // isBossStage = true; //boss room debug
       HandleSpawnRoom(isBossStage, dir);
       
       //if is soul shop
       // bool isSoulShop = false;
       // float sShopRate = UnityEngine.Random.Range(0f, 1f);
       // if (sShopRate > soulShopSpawnRate) isSoulShop = true;
       //     
    }

    private void HandleSpawnRoom(bool isBossStage, RoomEntranceDir dir)
    {
        if(OnRoomChangeStart != null) OnRoomChangeStart.Invoke();
        
        Level level = levelList[_levelCounter];
        Room room = null;

        if (isBossStage)
        {
            _roomFinishedCount = 0;
      
           room = bossRooms[_levelCounter];
           _levelCounter += 1;

           DialogueManager.instance.StartDialogue();
        }
        // else if (isSoulShop)
        // {
        //     room = rSoulShop;
        // }
        else
        {
            int roomIndex = 0;
            
            if (dir == RoomEntranceDir.SOUTH)
            {
                roomIndex = UnityEngine.Random.Range(0, level.southEntranceRooms.Count);
                room = level.southEntranceRooms[roomIndex];
            }
            else if (dir == RoomEntranceDir.EAST)
            {
                roomIndex = UnityEngine.Random.Range(0, level.eastEntranceRooms.Count);
                room = level.eastEntranceRooms[roomIndex];
            }
            else
            {
                roomIndex = UnityEngine.Random.Range(0, level.westEntranceRooms.Count);
                room = level.westEntranceRooms[roomIndex];
            }
            //  room = _levelList[0].southEntranceRooms[1];
        }
        StartCoroutine(EnableRoom(room));
    }
    
    
    private IEnumerator EnableRoom(Room room)
    {
        yield return new WaitForSeconds(1f);
        
        //Remove old room
        SetRoomActive(_prevRoom, false);
        _roomFinishedCount++;
        
        //Spawn new room
        SetRoomActive(room.roomPrefab.transform, true);
        _prevRoom = room.roomPrefab.transform;
        
        //Set player position to spawn point
        if (OnResetPlayerPos != null) OnResetPlayerPos.Invoke(Room.FindSpawnPoint(_prevRoom));
        if (OnRoomChangeFinish != null) OnRoomChangeFinish.Invoke();
    }

    private void SetRoomActive(Transform roomTransform, bool status)
    {
        roomTransform.gameObject.SetActive(status);
    }
}