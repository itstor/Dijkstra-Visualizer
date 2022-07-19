using UnityEngine;

public class NodeState : MonoBehaviour
{
    /*
     Color Note:
        Default Color: #39DA8A //Green
        Accesed Color: #FEDD49 //Yellow
        Path and Start #Color: 5B8DEE //Blue
        Visited Color: #FF5C5C //Red
        End Color: #AC5CD9 //Purple
        Selected Color: #FFFFFF //White
        Drag Add Color: #FFFFFF //White
     */
    [SerializeField] private float _colorFadeSpeed;
    [SerializeField] private float _glowAnimSpeed;
    [SerializeField] private Color _nodeCurrentColor;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _accessedColor;
    [SerializeField] private Color _pathColor;
    [SerializeField] private Color _endColor;
    [SerializeField] private Color _visitedColor;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _dragAddColor;
    [SerializeField] private Color _neighbourColor;
    private Color _tempColor;
    [SerializeField] private GameObject _glowGameObject;

    private SpriteRenderer _glowSpriteRenderer;
    private SpriteRenderer _nodeSpriteRenderer;
    private NodeTextController m_nodeTextController;
    private Color previousColor;
    private bool _isHover;
    private bool _forceGlow;

    void Awake()
    {
        _nodeSpriteRenderer = GetComponent<SpriteRenderer>();
        _glowSpriteRenderer = _glowGameObject.GetComponent<SpriteRenderer>();
        m_nodeTextController = GetComponent<NodeTextController>();

        _nodeSpriteRenderer.color = _glowSpriteRenderer.color = _nodeCurrentColor = _dragAddColor;
    }

    void Update()
    {
        if (_nodeCurrentColor != _nodeSpriteRenderer.color)
        {
            onColorUpdate();
        }

        onGlowUpdate();
    }

    private void onColorUpdate()
    {
        _nodeSpriteRenderer.color = Color.Lerp(_nodeSpriteRenderer.color, _nodeCurrentColor, _colorFadeSpeed * Time.deltaTime);
        _glowSpriteRenderer.color = Color.Lerp(_glowSpriteRenderer.color, _nodeCurrentColor, _colorFadeSpeed * Time.deltaTime);
    }

    private void onGlowUpdate()
    {
        float newGlowScale = Mathf.SmoothStep(_glowGameObject.transform.localScale.x, (_isHover || _forceGlow ? 1f : 0.5f), _glowAnimSpeed * Time.deltaTime);

        _glowGameObject.transform.localScale = new Vector3(newGlowScale, newGlowScale, newGlowScale);
    }

    public void setIdle() => _nodeCurrentColor = _defaultColor;
    public void setAccessed() => _nodeCurrentColor = _accessedColor;
    public void setPath() => _nodeCurrentColor = _pathColor;
    public void setStart() => _nodeCurrentColor = _pathColor;
    public void setVisited() => _nodeCurrentColor = _visitedColor;
    public void setSelected() => _nodeCurrentColor = _selectedColor;
    public void setDragAdd() => _nodeCurrentColor = _dragAddColor;
    public void setEnd() => _nodeCurrentColor = _endColor;
    public void setBack() => _nodeCurrentColor = previousColor;
    public void setNeighbour()
    {
        if (_nodeCurrentColor != _neighbourColor) _tempColor = _nodeCurrentColor;
        _nodeCurrentColor = _nodeCurrentColor != _neighbourColor ? _neighbourColor : _tempColor;
    }
    public void setHover() => _isHover = true;
    public void setExitHover() => _isHover = false;
    public void toggleForceGlow() => _forceGlow = !_forceGlow;
    public void showStep(int step) => m_nodeTextController.showNodeStep(step);
    public void hideStep() => m_nodeTextController.hideNodeStep();
    public void reset() { _nodeCurrentColor = _defaultColor; _isHover = false; _forceGlow = false; }
}