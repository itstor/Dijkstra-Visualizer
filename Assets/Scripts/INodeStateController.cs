interface INodeStateController
{
    void OnIdle();
    void OnAccessed();
    void OnVisited();
    void OnPath();
    void OnSelected();
    void OnDeselected();
}
