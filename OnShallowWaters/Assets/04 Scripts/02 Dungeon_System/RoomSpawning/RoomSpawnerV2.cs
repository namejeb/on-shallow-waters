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

public class RoomSpawnerV2 : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float soulShopSpawnRate = .2f;
    
    [SerializeField] private Transform rBasic;
    [SerializeField] private Transform rSoulShop;
    private bool _sShopSpawnedInCurrLevel;
    
    [SerializeField] private List<Room> bossRooms;
    [SerializeField] private List<Level> levelList = new List<Level>();
    private int _levelCounter = 0;
    
    private static Transform _prevRoom;
    public static event Action OnRoomChangeStart;
    public static event Action OnRoomChangeFinish;  
    public static event Action<Transform> OnResetPlayerPos;

    private int _roomFinishedCount = -1; //to exclude basic room
    private int _bossRoomsIndex;

    public static bool IsBossRoom { get; private set; }

    private void OnDestroy()
    {
        ExitRoomTrigger.OnExitRoom -= SpawnRoom;
        _roomFinishedCount = -1;
    }
    
    private void Awake()
    {
        SortRooms();
        ExitRoomTrigger.OnExitRoom += SpawnRoom;
        
        SetRoomActive(rSoulShop, false);
        
        SetRoomActive(rBasic.transform, true);
        _prevRoom = rBasic.transform;

        IsBossRoom = false;
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
        // Soul shop can only spawn once in one Level
        if (_sShopSpawnedInCurrLevel)
        {
            //after 5 rooms, spawn boss
            bool isBossStage = (_roomFinishedCount == 5); 
            
            // isBossStage = true; //boss room debug
            HandleSpawnRoom(isBossStage, dir);

            return;
        }
        
        float sShopRate = UnityEngine.Random.Range(0f, 1f);
        if (sShopRate < soulShopSpawnRate)
        {
            HandleSpawnSoulShop();
        }
        else
        {
            //after 5 rooms, spawn boss
            bool isBossStage = (_roomFinishedCount == 5); 
            
            // isBossStage = true; //boss room debug
            HandleSpawnRoom(isBossStage, dir);
        }
    }

    private void HandleSpawnSoulShop()
    {
        if(OnRoomChangeStart != null) OnRoomChangeStart.Invoke();
        
        StartCoroutine(EnableSoulShop());
        _sShopSpawnedInCurrLevel = true;
    }

    private void HandleSpawnRoom(bool isBossStage, RoomEntranceDir dir)
    {
        if(OnRoomChangeStart != null) OnRoomChangeStart.Invoke();
        
        Level level = levelList[_levelCounter];
        Room room = null;

        if (isBossStage)
        {
            IsBossRoom = true;
            
            _roomFinishedCount = 0;
            _sShopSpawnedInCurrLevel = false;
      
           room = bossRooms[_levelCounter];
           _levelCounter += 1;

           DialogueManager.instance.StartDialogue();
        }
        else
        {
            IsBossRoom = false;
            
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
            // room = level.southEntranceRooms[1];
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

    private IEnumerator EnableSoulShop()
    {
        yield return new WaitForSeconds(1f);
        
        //Remove old room
        SetRoomActive(_prevRoom, false);

        //Spawn new room
        SetRoomActive(rSoulShop, true);
        _prevRoom = rSoulShop;
        
        //Set player position to spawn point
        if (OnResetPlayerPos != null) OnResetPlayerPos.Invoke(Room.FindSpawnPoint(_prevRoom));
        if (OnRoomChangeFinish != null) OnRoomChangeFinish.Invoke();
    }

    private void SetRoomActive(Transform roomTransform, bool status)
    {
        roomTransform.gameObject.SetActive(status);
    }
}