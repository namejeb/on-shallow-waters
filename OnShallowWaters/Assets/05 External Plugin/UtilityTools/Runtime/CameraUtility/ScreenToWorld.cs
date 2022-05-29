using UnityEngine;

namespace NamejebTools.CameraUtility
{
    public class ScreenToWorld: MonoBehaviour
    {
        private Camera _cam;
    
        #region Singleton
        public static ScreenToWorld Instance;

        void Awake()
        {
            Instance = this;
        }
    
        #endregion

        public Vector2 ScreenBoundsInWorld { get; private set; }
    
        void Start()
        {
            _cam = Camera.main;
        
            //Convert screen res to world coordinates
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
      
            ScreenBoundsInWorld = _cam.ScreenToWorldPoint(new Vector2(screenWidth, screenHeight));
        }
    }
}