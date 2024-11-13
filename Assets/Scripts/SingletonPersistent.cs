using UnityEngine;

public class SingletonPersistent<T> : MonoBehaviour
    where T : Component
{
    [SerializeField] private bool dontDestroyOnLoad;
        
    public static T Instance { get; private set; }
    
    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            if(dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}