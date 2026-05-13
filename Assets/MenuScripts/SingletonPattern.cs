using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class SingletonPattern : MonoBehaviour
{
    private static SingletonPattern _gameInstance{ get { return instance; }}
    
    private static SingletonPattern instance = null;
    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
