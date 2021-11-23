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
    public void OnButtonSwitch()
    {
        switch (_ToolType)
        {
            case ToolsType.Select:
                _CursorInstance.currentState = CursorStateManager.CursorState.Select;
                break;
            case ToolsType.Connect:
                _CursorInstance.currentState = _CursorInstance.currentState == CursorStateManager.CursorState.Connect ? CursorStateManager.CursorState.Select : CursorStateManager.CursorState.Connect;
                break;
            case ToolsType.Add:
                _CursorInstance.currentState = _CursorInstance.currentState == CursorStateManager.CursorState.Add ? CursorStateManager.CursorState.Select : CursorStateManager.CursorState.Add;
                break;
        }
    }
}
