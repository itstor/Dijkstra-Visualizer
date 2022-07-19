using UnityEngine;
using UnityEngine.UI;

public class BlackBlockerGUI : MonoBehaviour
{
    // Start is called before the first frame update
    private Image m_blackBlocker;
    private bool m_isActive;
    [SerializeField] private float m_fadeSpeed;

    void Start()
    {
        m_blackBlocker = GetComponent<Image>();
        m_blackBlocker.color = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        if (m_isActive)
        {
            m_blackBlocker.color = new Color(0, 0, 0, Mathf.Lerp(m_blackBlocker.color.a, .5f, m_fadeSpeed * Time.deltaTime));
        }
        else
        {
            m_blackBlocker.color = new Color(0, 0, 0, Mathf.Lerp(m_blackBlocker.color.a, 0, m_fadeSpeed * Time.deltaTime));
        }
    }

    public void show()
    {
        m_blackBlocker.raycastTarget = true;
        m_isActive = true;
    }

    public void hide()
    {
        m_blackBlocker.raycastTarget = false;
        m_isActive = false;
    }
}
