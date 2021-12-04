using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogGUI : MonoBehaviour
{
    public Sprite[] m_dialogIcon;
    public string[] m_dialogTitle;
    public string[] m_dialogDescription;
    public string[] m_inputHint;

    [SerializeField] private Image m_dialogIconImage;
    [SerializeField] private TMPro.TextMeshProUGUI m_dialogTitleText;
    [SerializeField] private TMPro.TextMeshProUGUI m_dialogDescriptionText;
    [SerializeField] private TMPro.TextMeshProUGUI m_inputHintText;
    [SerializeField] private TMPro.TMP_InputField m_inputField;

    private Action<string, bool, Dictionary<string, dynamic>> m_callback;
    private Dictionary<string, dynamic> m_passGameObject;
    private bool m_isInputEntered;

    public void showDialog(int dialogIndex, Action<string, bool, Dictionary<string, dynamic>> callback, Dictionary<string, dynamic> pass_gameObject)
    {
        m_dialogIconImage.sprite = m_dialogIcon[dialogIndex];
        m_dialogTitleText.text = m_dialogTitle[dialogIndex];
        m_dialogDescriptionText.text = m_dialogDescription[dialogIndex];
        m_inputHintText.text = m_inputHint[dialogIndex];
        m_callback = callback;
        m_passGameObject = pass_gameObject;
        gameObject.SetActive(true);
    }

    public void storeData(){
        m_isInputEntered = true;

        m_callback(m_inputField.text, m_isInputEntered, m_passGameObject);
        reset();
        gameObject.SetActive(false);
    }

    public void closeDialog(){
        m_callback(m_inputField.text, m_isInputEntered, m_passGameObject);
        reset();
        gameObject.SetActive(false);
    }

    private void reset(){
        m_inputField.text = String.Empty;
        m_isInputEntered = false;
        m_callback = null;
        m_passGameObject = null;
    }
}
