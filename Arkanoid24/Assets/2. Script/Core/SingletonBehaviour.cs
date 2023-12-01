using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    private static T instance = null;
    public static T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.LogError(string.Format("허용되지 않은 중복 인스턴스 => {0}", typeof(T)));
            Destroy(this);
            return;
        }

        instance = (T)this;
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}