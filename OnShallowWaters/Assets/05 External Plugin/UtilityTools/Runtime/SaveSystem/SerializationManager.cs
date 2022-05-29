using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;


namespace NamejebTools.SaveSystem
{
    public class SerializationManager : MonoBehaviour
    {
        private const string FolderName = "/Saves";
        private const string SaveExtension = ".save";
        
        public static bool Save(string saveName, object saveData)
        {
            BinaryFormatter formatter = GetBinaryFormatter();
            
            if(!Directory.Exists( Application.persistentDataPath + FolderName))
            {
                Directory.CreateDirectory(Application.persistentDataPath + FolderName);
            }

            
            string path = Application.persistentDataPath + FolderName + "/" + saveName + SaveExtension;

            FileStream file = File.Create(path);
            formatter.Serialize(file, saveData);

            file.Close();

            return true;
        }

        public static object Load(string saveName)
        {
            string path =  Application.persistentDataPath + FolderName + "/" + saveName + SaveExtension;
            
            if(!File.Exists(path)) return null;
            
            
            BinaryFormatter formatter = GetBinaryFormatter();

            FileStream file = File.Open(path, FileMode.Open);

            try
            {
                object save = formatter.Deserialize(file);
                file.Close();

                GameEvents.DispatchOnLoadEvent();
                
                return save;
            }
            catch
            {
                Debug.LogErrorFormat("Failed to load file at {0}", path);
                file.Close();
                return null;
            }
        }

        private static BinaryFormatter GetBinaryFormatter()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            SurrogateSelector selector = new SurrogateSelector();

            //free to add more surrogates if needed
            Vector3SerializationSurrogate vector3Surrogate = new Vector3SerializationSurrogate();
            QuaternionSerializationSurrogate quaternionSurrogate = new QuaternionSerializationSurrogate();
            
            selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
            selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrogate);

            formatter.SurrogateSelector = selector;
            
            return formatter;
        }
    }
}
