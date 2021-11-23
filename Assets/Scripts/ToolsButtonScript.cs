using UnityEngine;
using UnityEngine.UIElements;

public class ToolsButtonScript : MonoBehaviour
{

    [SerializeField] private ToolsType _ToolType;
    private CursorStateManager _CursorInstance;

    private void Start() {
        _CursorInstance = CursorStateManager.Instance;
    }

    enum ToolsType
    {
        Select,
        Connect,
        Add,
    }

    private void Update() {
        if (_CursorInstance.currentState == (CursorStateManager.CursorState)_ToolType){
            Debug.Log("ToolType: " + _ToolType);
        }
    }
    public void OnButtonSwitch()
    {
        switch (_ToolType)
        {
            case ToolsType.Select:
                _CursorInstance.currentState = CursorStateManager.CursorState.Select;
                break;
            case ToolsType.Connect:
                _CursorInstance.currentState = CursorStateManager.CursorState.Connect;
                break;
            case ToolsType.Add:
                _CursorInstance.currentState = CursorStateManager.CursorState.Add;
                break;
        }
    }
}
