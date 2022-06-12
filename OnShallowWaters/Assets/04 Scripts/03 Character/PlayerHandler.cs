using UnityEngine;
using NamejebTools.SaveSystem;
using System.Collections;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    private PlayerMovement _playerMovement;
    

    private void OnDestroy()
    {
        RoomSpawner.OnResetPlayerPos -= ResetPosition;
    }

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        RoomSpawner.OnResetPlayerPos += ResetPosition;
    }
    
    public void SavePlayer()
    {
        PlayerData playerData = new PlayerData(transform.position, transform.rotation);
        playerData.Save();
    }

    public void LoadPlayer()
    {
        PlayerData.Load();
        print(SaveDataMain.Current.PlayerData.PlayerLocation);
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
}
