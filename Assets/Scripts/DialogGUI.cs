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
    [SerializeField] private TMPro.TMP_InputField m_nameInputField;
    [SerializeField] private TMPro.TMP_InputField m_xInputField;
    [SerializeField] private TMPro.TMP_InputField m_yInputField;
    private Vector3 m_defaultDialogPanelSize;

    private Action<string, bool, Vector2> m_callback;
    private Dictionary<string, dynamic> m_passGameObject;
    private bool m_isInputEntered;
    private bool m_isShowing = false;

    void Start(){
        m_defaultDialogPanelSize = gameObject.transform.localScale;
    }

    void Update(){
        if (m_isShowing){
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, new Vector3(1.1f,1.1f,0), Time.deltaTime * 10);
            if (Input.GetKeyDown(KeyCode.Return) && m_nameInputField.text != "" && m_xInputField.text != "" && m_yInputField.text != ""){
                storeData();
            }
            if (Input.GetKeyDown(KeyCode.Tab)){
                if (m_nameInputField.isFocused){
                    m_xInputField.Select();
                }
                else if (m_xInputField.isFocused){
                    m_yInputField.Select();
                }
                else if (m_yInputField.isFocused){
                    m_nameInputField.Select();
                }
            }
        }

        else {
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, new Vector3(0f,0f,0f), Time.deltaTime * 10);
        }
    }

    public void showDialog(int dialogIndex, Action<string, bool, Vector2> callback)
    {
        if (dialogIndex == 1){
            m_nameInputField.contentType = TMPro.TMP_InputField.ContentType.DecimalNumber;
        }
        else if (dialogIndex == 0){
            m_nameInputField.contentType = TMPro.TMP_InputField.ContentType.Standard;
        }
        m_dialogIconImage.sprite = m_dialogIcon[dialogIndex];
        m_dialogTitleText.text = m_dialogTitle[dialogIndex];
        m_dialogDescriptionText.text = m_dialogDescription[dialogIndex];
        m_inputHintText.text = m_inputHint[dialogIndex];
        m_callback = callback;
        m_isShowing = true;
        m_xInputField.Select();
        m_xInputField.ActivateInputField();
        
        GUIManager.Instance.showBlocker();
    }

    public void storeData(){
        m_isInputEntered = true;
        m_callback(m_nameInputField.text, m_isInputEntered, new Vector2(float.Parse(m_xInputField.text), float.Parse(m_yInputField.text)));
        GUIManager.Instance.hideBlocker();
        m_isShowing = false;
        reset();
    }

    public void closeDialog(){
        GUIManager.Instance.hideBlocker();
        m_isShowing = false;
        reset();
    }

    private void reset(){
        m_nameInputField.text = String.Empty;
        m_xInputField.text = String.Empty;
        m_yInputField.text = String.Empty;
        m_isInputEntered = false;
        m_callback = null;
        m_passGameObject = null;
    }
}
