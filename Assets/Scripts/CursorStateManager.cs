using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CursorStateManager : MonoBehaviour
{

    public static CursorStateManager Instance = null;
    public CursorState currentState;

    public enum CursorState
    {
        Select,
        Connect,
        Add,
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
