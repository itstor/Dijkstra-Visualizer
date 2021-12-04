using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;
    [SerializeField] private DialogGUI m_DialogGUI;
    [SerializeField] private ToastGUI m_ToastGUI;

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

    public void showToast(string message, float duration){
        m_ToastGUI.showToast(message, duration);
    }

    public void testToast(){
        showToast("Test Toast", 2f);
    }

    public void tesToast2(){
        showToast("Test Toast 2", 2f);
    }
}
