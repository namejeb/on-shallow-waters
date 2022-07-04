using System.Collections;
using UnityEngine;

/*
 * This script needs to exists in an object before use, only one is needed,
 *   preferably on the main camera
 */


namespace NamejebTools.CameraUtility 
{    
    public class CameraShake3D : MonoBehaviour
    {
        [SerializeField] protected float magnitude;
        [SerializeField] protected float duration;

        public static CameraShake3D Instance;
        void Awake()
        {
            Instance = this;
        }
        
        
        //For outside script to use
        
        //default is Main Camera
        public void Shake() { StartShaking(); }
        
        //default is Main Camera
        public void Shake(float m, float d) { StartShaking(m, d); }

        //shake specific camera
        public void Shake(Camera cam) { StartShaking(cam); }
        
        //shake specific camera
        public void Shake(float m, float d, Camera cam) { StartShaking(m, d, cam); }



        private void StartShaking() { StartCoroutine(cShake(magnitude, duration, Camera.main)); }

        private void StartShaking(Camera cam) { StartCoroutine(cShake(magnitude, duration, cam)); }
        
        private void StartShaking(float m, float d, Camera cam) { StartCoroutine(cShake(m, d, cam)); }

        private void StartShaking(float m, float d) { StartCoroutine(cShake(m, d, Camera.main)); }
        
        
        
        private static IEnumerator cShake(float m, float d, Camera cam)
        {
            Transform camTransform = cam.transform;
            
            Vector3 originalPos = camTransform.position;
            
            float startTime = 0f;
            while (startTime < d)
            {
                float x = Random.Range(-1f, 1f) * m;
                float y = Random.Range(-1f, 1f) * m;
                float z = Random.Range(-1f, 1f) * m;

                camTransform.position = new Vector3(x, y, z);

                startTime += Time.deltaTime;
                
                yield return null;
            }

            camTransform.position = originalPos;
        }
    }
}