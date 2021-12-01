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
    [SerializeField] private GameObject _glowGameObject;
    
    private SpriteRenderer _glowSpriteRenderer;
    private SpriteRenderer _nodeSpriteRenderer;
    private bool _isHover;

    void Awake()
    {
        _nodeSpriteRenderer = GetComponent<SpriteRenderer>();
        _glowSpriteRenderer = _glowGameObject.GetComponent<SpriteRenderer>();

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
        float newGlowScale = Mathf.SmoothStep(_glowGameObject.transform.localScale.x, (_isHover ? 1f : 0.5f), _glowAnimSpeed * Time.deltaTime);

        _glowGameObject.transform.localScale = new Vector3(newGlowScale, newGlowScale, newGlowScale);
    }

    public void onIdle() => _nodeCurrentColor = _defaultColor;
    public void onAccessed() => _nodeCurrentColor = _accessedColor;
    public void onPath() => _nodeCurrentColor = _pathColor;
    public void onStart() => _nodeCurrentColor = _pathColor;
    public void onVisited() => _nodeCurrentColor = _visitedColor;
    public void onSelected() => _nodeCurrentColor = _selectedColor;
    public void onHover() => _isHover = true;
    public void onExitHover() => _isHover = false;
    public void onDragAdd() => _nodeCurrentColor = _dragAddColor;
    public void onEnd() => _nodeCurrentColor = _endColor;
}