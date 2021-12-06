using UnityEngine;
using UnityEngine.UI;
using System.Timers;
using System.Collections.Generic;

public class ToastGUI : MonoBehaviour
{
    private TMPro.TextMeshProUGUI m_toastText;
    [SerializeField] float m_padding;
    [SerializeField] private float m_fadeSpeed;
    [SerializeField] private Color m_defaultToastBoxColor;
    [SerializeField] private Color m_defaultToastTextColor;
    private bool m_isShowing = false;
    private Image m_toastBox;

    private readonly Timer m_time = new Timer();
    private Queue<(string, float)> m_ToastQueue = new Queue<(string, float)>();


    void Start()
    {
        m_toastText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        m_toastBox = GetComponent<Image>();
        m_time.Elapsed += onFinish;
        // m_ToastBox.color = m_DefaultToastBoxColor;
    }

    void Update()
    {
        if (m_ToastQueue.Count > 0 && !m_isShowing)
        {
            var task = m_ToastQueue.Dequeue();
            setToast(task.Item1, task.Item2);
            m_isShowing = true;
            m_time.Enabled = true;
            m_time.Start();
        }

        if (m_isShowing)
        {
            // Debug.Log("Showing");
            m_toastText.color = Color.Lerp(m_toastText.color, m_defaultToastTextColor, m_fadeSpeed * Time.deltaTime);
            m_toastBox.color = Color.Lerp(m_toastBox.color, m_defaultToastBoxColor, m_fadeSpeed * Time.deltaTime);
        }
        else
        {
            var newToastBoxColor = m_toastBox.color;
            var newToastTextColor = m_toastText.color;

            newToastTextColor.a = Mathf.Lerp(newToastTextColor.a, 0, m_fadeSpeed * Time.deltaTime);
            newToastBoxColor.a = Mathf.Lerp(newToastBoxColor.a, 0, m_fadeSpeed * Time.deltaTime);
            m_toastBox.color = newToastBoxColor;
            m_toastText.color = newToastTextColor;        
        }
    }

    public void showToast(string message, float duration, bool highPriority = false){
        if (highPriority)
        {
            highPrioritizedToast(message, duration);
            return;
        }

        m_ToastQueue.Enqueue((message,duration));
    }

    void highPrioritizedToast(string message, float duration)
    {
        setToast(message, duration);
        if (!m_isShowing)
        {
            m_isShowing = true;
            m_time.Enabled = true;
            m_time.Start();
        }
    }

    public void setToast(string message, float duration)
    {
        var messageLenght = message.Length;
        var toastLenght = messageLenght * 6f + m_padding;
        m_toastBox.rectTransform.sizeDelta = new Vector2(toastLenght, m_toastBox.rectTransform.sizeDelta.y);
        m_toastText.text = message;
        m_time.Interval = duration * 1000;
    }

    void onFinish(object o, System.EventArgs e)
    {
        Debug.Log("Toast finished");
        m_isShowing = false;
        m_time.Stop();
    }
}