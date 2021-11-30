using UnityEngine;
using UnityEngine.UI;

public class ToolsButtonGUI : MonoBehaviour
{
    [SerializeField] private states.ToolsType mToolType;
    private CursorStateManager mCursorInstance;

    private void Start() {
        mCursorInstance = CursorStateManager.Instance;
    }

    void Update() {
        if (mCursorInstance.currentState == (states.CursorState)mToolType){
            gameObject.GetComponent<Image>().color = Color.blue;
        }
        else{
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }
    public void OnButtonSwitch()
    {
        switch (mToolType)
        {
            case states.ToolsType.Select:
                mCursorInstance.currentState = states.CursorState.Select;
                break;
            case states.ToolsType.Connect:
                mCursorInstance.currentState = states.CursorState.Connect;
                break;
            case states.ToolsType.Add:
            {
                mCursorInstance.currentState = states.CursorState.Add;
            }
                break;
        }
    }
}
