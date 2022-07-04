using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;




public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private RoomListSO roomListSo;

    private List<Level> _levelList = new List<Level>();
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

         GameObject roomBasic = roomListSo.roomBasic;
         _prevRoom = Instantiate(roomBasic.transform, roomBasic.transform.position, roomBasic.transform.rotation);
    }

    private void SortRooms()
    {
        for (int i = 0; i < roomListSo.levels.Count; i++)
        {
            Level currLevel = roomListSo.levels[i];
            
            currLevel.southEntranceRooms.Clear();
            currLevel.eastEntranceRooms.Clear();
            currLevel.westEntranceRooms.Clear();
            
            for (int j = 0; j < roomListSo.levels[i].rooms.Count; j++)
            {
                Room room = roomListSo.levels[i].rooms[j];
                
                if (room.roomEntranceDir == RoomEntranceDir.SOUTH) { currLevel.southEntranceRooms.Add(room); }
                else if (room.roomEntranceDir == RoomEntranceDir.EAST) { currLevel.eastEntranceRooms.Add(room); }
                else  { currLevel.westEntranceRooms.Add(room); }
            }
            _levelList.Add(currLevel);
        }
    }

    private void SpawnRoom(RoomEntranceDir dir)
    {
        //after 5 rooms, spawn boss
        bool isBossStage = (_roomFinishedCount == 5); 
        //isBossStage = true; //boss room debug
        HandleSpawnRoom(isBossStage, dir);
    }

    private void HandleSpawnRoom(bool isBossStage, RoomEntranceDir dir)
    {
        if(OnRoomChangeStart != null) OnRoomChangeStart.Invoke();
        
        Level level = _levelList[_levelCounter];
        Room room = null;

        if (isBossStage)
        {
            _roomFinishedCount = 0;
      
           room = roomListSo.bossRooms[_levelCounter];
           _levelCounter += 1;

           DialogueManager.instance.StartDialogue();
        }
        else
        {
            int roomIndex = 0;
       ;
        
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
        _prevRoom = Instantiate(roomTransform, roomTransform.localPosition, roomTransform.rotation);
        print(room.roomPrefab.transform.localPosition);
        
        //Set player position to spawn point
        if (OnResetPlayerPos != null) OnResetPlayerPos.Invoke(Room.FindSpawnPoint(_prevRoom));
        
        if (OnRoomChangeFinish != null) OnRoomChangeFinish.Invoke();
    }
}