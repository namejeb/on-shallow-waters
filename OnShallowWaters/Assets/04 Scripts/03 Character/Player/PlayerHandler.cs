using UnityEngine;
using NamejebTools.SaveSystem;
using System.Collections;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;
  
    private static PlayerStats _playerStats;
    private static PlayerMovement _playerMovement;
    private BoonEffectsManager _boonEffectsManager;
    
    public Transform CameraTarget
    {
        get => cameraTarget;
    }

    public PlayerStats PlayerStats
    {
        get => _playerStats; 
    }


    public BoonEffectsManager BoonEffectsManager
    {
        get => _boonEffectsManager;
    }


    public static PlayerHandler Instance;

    private void OnDestroy()
    {
        RoomSpawnerV2.OnResetPlayerPos -= ResetPosition;
    }

    private void Awake()
    {
        Instance = this;
        
        _playerMovement = GetComponent<PlayerMovement>();
        RoomSpawnerV2.OnResetPlayerPos += ResetPosition;

        _playerStats = GetComponent<PlayerStats>();

        _boonEffectsManager = transform.root.Find("BoonEffectsManager").GetComponent<BoonEffectsManager>();
    }
    
    public void SavePlayer()
    {
        PlayerData playerData = new PlayerData(transform.position, transform.rotation);
        playerData.Save();
    }

    public void LoadPlayer()
    {
        PlayerData.Load();
       // print(SaveDataMain.Current.PlayerData.PlayerLocation);
    }

    private void ResetPosition(Transform spawnPoint)
    {
        _playerMovement.enabled = false;
        StartCoroutine(EnableMovement());

        Vector3 spawnPos = spawnPoint.position;
        transform.position = new Vector3(spawnPos.x, transform.position.y, spawnPos.z);

        Quaternion spawnRot = spawnPoint.rotation;
        transform.rotation = new Quaternion(spawnRot.x, spawnRot.y, spawnRot.z, spawnRot.w);
    }

    private IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(1f);
        _playerMovement.enabled = true;
    }

    public void EnableAnDisableMove()
    {
        _playerMovement.canMove = !_playerMovement.canMove;
    }
}
