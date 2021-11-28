using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeStateController : MonoBehaviour, INodeStateController
{
    [SerializeField] private float _colorFadeSpeed;
    [SerializeField] private float _glowAnimSpeed;
    [SerializeField] private Color _nodeCurrentColor;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _accessedColor;
    [SerializeField] private Color _pathColor;
    [SerializeField] private Color _visitedColor;
    [SerializeField] private SpriteRenderer _glowSpriteRenderer;
    [SerializeField] private GameObject _glowTransform;

    private SpriteRenderer _nodeSpriteRenderer;
    private bool _isSelected;

    void Start()
    {
        _nodeSpriteRenderer = GetComponent<SpriteRenderer>();
        _nodeSpriteRenderer.color = _defaultColor;
        _glowSpriteRenderer.color = _defaultColor;
        _nodeCurrentColor = _defaultColor;
    }

    void Update()
    {
        OnColorUpdate();
        OnGlowUpdate();
    }

    private void OnColorUpdate()
    {
        if (_nodeCurrentColor != _nodeSpriteRenderer.color)
        {
            _nodeSpriteRenderer.color = Color.Lerp(_nodeSpriteRenderer.color, _nodeCurrentColor, _colorFadeSpeed * Time.deltaTime);
            _glowSpriteRenderer.color = Color.Lerp(_glowSpriteRenderer.color, _nodeCurrentColor, _colorFadeSpeed * Time.deltaTime);
        }
    }

    private void OnGlowUpdate()
    {
        float newGlowScale = Mathf.SmoothStep(_glowTransform.transform.localScale.x, (_isSelected ? 1f : 0.5f), _glowAnimSpeed * Time.deltaTime);
        _glowTransform.transform.localScale = new Vector3(newGlowScale, newGlowScale, newGlowScale);
    }

    public void OnIdle() => _nodeCurrentColor = _defaultColor;
    public void OnAccessed() => _nodeCurrentColor = _accessedColor;
    public void OnPath() => _nodeCurrentColor = _pathColor;
    public void OnVisited() => _nodeCurrentColor = _visitedColor;
    public void OnSelected() => _isSelected = true;
    public void OnDeselected() => _isSelected = false;
}