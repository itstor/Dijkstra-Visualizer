using UnityEngine;
using UnityEngine.UI;
using System.Timers;
using System.Collections.Generic;

public class ToastGUI : MonoBehaviour
{
    private TMPro.TextMeshProUGUI m_ToastText;
    [SerializeField] float m_Padding;
    [SerializeField] private float m_fadeSpeed;
    [SerializeField] private Color m_DefaultToastBoxColor;
    [SerializeField] private Color m_DefaultToastTextColor;
    private bool m_IsShowing = false;
    private Image m_ToastBox;

    private readonly Timer m_time = new Timer();
    private Queue<(string, float)> m_ToastQueue = new Queue<(string, float)>();


    void Start()
    {
        m_ToastText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        m_ToastBox = GetComponent<Image>();
        m_time.Elapsed += onFinish;
        // m_ToastBox.color = m_DefaultToastBoxColor;
    }

    void Update()
    {
        if (m_ToastQueue.Count > 0 && !m_IsShowing)
        {
            var task = m_ToastQueue.Dequeue();
            setToast(task.Item1, task.Item2);
            m_IsShowing = true;
            m_time.Enabled = true;
            m_time.Start();
        }

        if (m_IsShowing)
        {
            Debug.Log("Showing");
            m_ToastText.color = Color.Lerp(m_ToastText.color, m_DefaultToastTextColor, m_fadeSpeed * Time.deltaTime);
            m_ToastBox.color = Color.Lerp(m_ToastBox.color, m_DefaultToastBoxColor, m_fadeSpeed * Time.deltaTime);
        }
        else
        {
            var newToastBoxColor = m_ToastBox.color;
            var newToastTextColor = m_ToastText.color;

            newToastTextColor.a = Mathf.Lerp(newToastTextColor.a, 0, m_fadeSpeed * Time.deltaTime);
            newToastBoxColor.a = Mathf.Lerp(newToastBoxColor.a, 0, m_fadeSpeed * Time.deltaTime);
            m_ToastBox.color = newToastBoxColor;
            m_ToastText.color = newToastTextColor;        
        }
    }

    public void showToast(string message, float duration, bool highPriority = false){
        if (highPriority)
        {
            highPriotizedToast(message, duration);
            return;
        }

        m_ToastQueue.Enqueue((message,duration));
    }

    void highPriotizedToast(string message, float duration)
    {
        setToast(message, duration);
        if (!m_IsShowing)
        {
            m_IsShowing = true;
            m_time.Enabled = true;
            m_time.Start();
        }
    }

    public void setToast(string message, float duration)
    {
        var messageLenght = message.Length;
        var toastLenght = messageLenght * 8f + m_Padding;
        m_ToastBox.rectTransform.sizeDelta = new Vector2(toastLenght, m_ToastBox.rectTransform.sizeDelta.y);
        m_ToastText.text = message;
        m_time.Interval = duration * 1000;
    }

    void onFinish(object o, System.EventArgs e)
    {
        Debug.Log("Toast finished");
        m_IsShowing = false;
        m_time.Stop();
    }
}