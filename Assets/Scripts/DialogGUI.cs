using System;
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

    private Action<string, GameObject> m_callback;
    private GameObject m_passGameObject;
    private bool m_isInputEntered;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showDialog(int dialogIndex, Action<string, GameObject> callback, GameObject pass_gameObject)
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

        m_callback(m_inputField.text, m_passGameObject);
        closeDialog();
    }

    public void closeDialog(){
        if (!m_isInputEntered)
        {
            Destroy(m_passGameObject);
        }
        reset();
        gameObject.SetActive(false);
    }

    private void reset(){
        m_inputField.text = String.Empty;
        m_isInputEntered = false;
        m_callback = null;
        m_passGameObject = null;
    }

    public void Test()
    {
        throw new NotImplementedException();
    }
}
