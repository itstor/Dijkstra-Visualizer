using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogGUI : MonoBehaviour
{
    [SerializeField] private Sprite[] m_dialogIcon;
    [SerializeField] private string[] m_dialogTitle;
    [SerializeField] private string[] m_dialogDescription;
    [SerializeField] private string[] m_inputHint;

    [SerializeField] private Image m_dialogIconImage;
    [SerializeField] private TMPro.TextMeshProUGUI m_dialogTitleText;
    [SerializeField] private TMPro.TextMeshProUGUI m_dialogDescriptionText;
    [SerializeField] private TMPro.TextMeshProUGUI m_inputHintText;
    [SerializeField] private TMPro.TMP_InputField m_inputField;
    private Vector3 m_defaultDialogPanelSize;

    private Action<string, bool, Dictionary<string, dynamic>> m_callback;
    private Dictionary<string, dynamic> m_passGameObject;
    private bool m_isInputEntered;
    private bool m_isShowing = false;

    void Start()
    {
        m_defaultDialogPanelSize = gameObject.transform.localScale;
    }

    void Update()
    {
        if (m_isShowing)
        {
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, new Vector3(1.1f, 1.1f, 0), Time.deltaTime * 10);
            if (Input.GetKeyDown(KeyCode.Return) && m_inputField.text != "")
            {
                storeData();
            }
        }

        else
        {
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10);
        }
    }

    public void showDialog(int dialogIndex, Action<string, bool, Dictionary<string, dynamic>> callback, Dictionary<string, dynamic> pass_gameObject)
    {
        if (dialogIndex == 1)
        {
            m_inputField.contentType = TMPro.TMP_InputField.ContentType.DecimalNumber;
        }
        else if (dialogIndex == 0)
        {
            m_inputField.contentType = TMPro.TMP_InputField.ContentType.Standard;
        }
        m_dialogIconImage.sprite = m_dialogIcon[dialogIndex];
        m_dialogTitleText.text = m_dialogTitle[dialogIndex];
        m_dialogDescriptionText.text = m_dialogDescription[dialogIndex];
        m_inputHintText.text = m_inputHint[dialogIndex];
        m_callback = callback;
        m_passGameObject = pass_gameObject;
        m_isShowing = true;
        m_inputField.Select();
        m_inputField.ActivateInputField();

        GUIManager.Instance.showBlocker();
    }

    public void storeData()
    {
        m_isInputEntered = true;
        closeDialog();
    }

    public void closeDialog()
    {
        m_callback(m_inputField.text, m_isInputEntered, m_passGameObject);
        GUIManager.Instance.hideBlocker();
        m_isShowing = false;
        reset();
    }

    private void reset()
    {
        m_inputField.text = String.Empty;
        m_isInputEntered = false;
        m_callback = null;
        m_passGameObject = null;
    }
}
