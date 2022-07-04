using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace NamejebTools.GameHandlers
{
    public class LoadLevel : MonoBehaviour
    {
        private static int currLevelIndex;
        
        #region Singleton
    
        public static LoadLevel Instance;
    
        void Awake()
        {
            if (Instance == null || Instance != this)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            
            currLevelIndex = SceneManager.GetActiveScene().buildIndex;
        }
    
        #endregion
    
    
        public static event Action<int> OnLoadLevel;
    
        
        public static int CurrLevelIndex { get => currLevelIndex; }
        
        
        public void LoadNextLevel()
        {
           Load(SceneManager.GetActiveScene().buildIndex + 1);
        }
    
        public void LoadPrevLevel()
        {
            Load(SceneManager.GetActiveScene().buildIndex - 1);
        }
        
        public void LoadMainMenu()
        {
            Load(0);
        }
        
        private void Load(int index)
        {
            currLevelIndex = index;
            
            SceneManager.LoadSceneAsync(index);
            
            if (OnLoadLevel != null)
            {
                OnLoadLevel.Invoke(index);
            }
        }
    }
}
