using System;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;
    [SerializeField] private DialogGUI m_dialogGUI;
    [SerializeField] private ToastGUI m_toastGUI;
    [SerializeField] private BlackBlockerGUI m_blackBlockerGUI;

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
        m_toastGUI.showToast(message, duration, hightPriority);
    }

    public void showDialog(int dialogIndex, Action<string, bool, Vector2> callback){
        m_dialogGUI.showDialog(dialogIndex, callback);
    }

    public void showBlocker(){
        m_blackBlockerGUI.show();
    }
    public void hideBlocker(){
        m_blackBlockerGUI.hide();
    }
}
