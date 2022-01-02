using UnityEngine;

namespace Helper
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        protected virtual void Awake() => Instance = this as T;

        protected void OnApplicationQuit()
        {
            Instance = null;
            Destroy(gameObject);
        }
    }

    public abstract class SingletonPersistent<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if(Instance != null) Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }
    }
}