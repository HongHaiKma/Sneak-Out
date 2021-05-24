using UnityEngine;

namespace PFramework
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        private static readonly string SOSFolderName = "SingletonScriptableObjects";

        static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<T>(string.Format("{0}/{1}", SOSFolderName, typeof(T)));

#if UNITY_EDITOR
                    if (_instance == null)
                    {
                        string configPath = string.Format("Assets/Resources/{0}/", SOSFolderName);
                        if (!System.IO.Directory.Exists(configPath))
                            System.IO.Directory.CreateDirectory(configPath);

                        _instance = ScriptableObjectHelper.CreateAsset<T>(configPath, typeof(T).ToString());
                    }
#endif
                }

                return _instance;
            }
        }
    }
}