using UnityEngine;
using UnityEngine.UI;

public class ToolsButtonGUI : MonoBehaviour
{
    [SerializeField] private Color m_selectedColor;
    [SerializeField] private Color m_unselectedColor;
    [SerializeField] private states.ToolsType m_toolType;
    private CursorStateManager m_cursorInstance;

    private void Start() {
        m_cursorInstance = CursorStateManager.Instance;
    }

    void Update() {
        gameObject.GetComponent<Image>().color = m_cursorInstance.m_currentState == (states.CursorState)m_toolType ? m_selectedColor : m_unselectedColor;
    }
    public void OnButtonSwitch()
    {
        m_cursorInstance.m_currentState = (states.CursorState)m_toolType;
    }
}
