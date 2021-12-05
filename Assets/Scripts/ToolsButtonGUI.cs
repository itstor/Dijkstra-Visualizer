using UnityEngine;
using UnityEngine.UI;

public class ToolsButtonGUI : MonoBehaviour
{
    [SerializeField] private states.ToolsType m_toolType;
    private CursorStateManager mCursorInstance;

    private void Start() {
        mCursorInstance = CursorStateManager.Instance;
    }

    void Update() {
        // if (mCursorInstance.currentState == (states.CursorState)mToolType){
        //     gameObject.GetComponent<Image>().color = Color.blue;
        // }
        // else{
        //     gameObject.GetComponent<Image>().color = Color.white;
        // }
    }
    public void OnButtonSwitch()
    {
        mCursorInstance.currentState = (states.CursorState)m_toolType;
    }
}
