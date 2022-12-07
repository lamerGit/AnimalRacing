using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Component
{

    private static bool isShutDown = false;

    private static T instance=null;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                T obj=FindObjectOfType<T>();
                if(obj == null)
                {
                    GameObject gameObject = new();
                    gameObject.name = $"{typeof(T).Name}";
                    obj=gameObject.AddComponent<T>();
                }
                instance = obj;
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    private void Awake()
    {
        if(instance==null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            if(instance!=this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnApplicationQuit()
    {
        isShutDown = true;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        
    }
}
