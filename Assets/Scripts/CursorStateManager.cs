using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CursorStateManager : MonoBehaviour
{

    public static CursorStateManager Instance = null;
    public CursorState currentState;

    public enum CursorState
    {
        Idle,
        Drag,
        Connect
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void setStateDrag()
    {
        currentState = currentState != CursorState.Drag ? CursorState.Drag : CursorState.Idle;
    }

    public void setStateIdle()
    {
        currentState = CursorState.Idle;
    }

    public void setStateConnect()
    {
        currentState = CursorState.Connect;
    }
}
