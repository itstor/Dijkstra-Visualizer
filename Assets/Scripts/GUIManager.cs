using System;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;
    [SerializeField] private DialogGUI m_DialogGUI;
    [SerializeField] private ToastGUI m_ToastGUI;
    [SerializeField] private BlackBlockerGUI m_BlackBlockerGUI;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void showToast(string message, float duration, bool hightPriority = false){
        m_ToastGUI.showToast(message, duration, hightPriority);
    }

    public void showDialog(int dialogIndex, Action<string, bool, Dictionary<string, dynamic>> callback, Dictionary<string, dynamic> pass_gameObject){
        m_DialogGUI.showDialog(dialogIndex, callback, pass_gameObject);
    }

    public void showBlocker(){
        m_BlackBlockerGUI.show();
    }
    public void hideBlocker(){
        m_BlackBlockerGUI.hide();
    }
}
