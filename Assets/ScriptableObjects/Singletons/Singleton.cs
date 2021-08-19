using UnityEngine;

namespace ScriptableObjects.Singletons
{
    //[CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class Singleton<T> : ScriptableObject where T : Singleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    T[] assets = Resources.LoadAll<T>("");
                    if (assets == null || assets.Length < 1)
                    {
                        throw new System.Exception("Could not find any singleton scriptable object instances");
                    }

                    if (assets.Length > 1)
                    {
                        Debug.LogWarning("Multiple instances of the singleton scriptable object found");
                    }

                    _instance = assets[0];
                }

                return _instance;
            }
        }
    }
}