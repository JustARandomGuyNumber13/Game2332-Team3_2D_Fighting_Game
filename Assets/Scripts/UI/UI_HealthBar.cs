using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fill;
    [SerializeField] private Image _background;
    [SerializeField] private bool _isP1;
    [SerializeField] private Color[] _healthLoopColor;
    private float  _maxHealth;
    private float _healthPerStage;

    private Color _bgOrgColor;
    private int _curStage, _prevStage, _nextStage;

    private void Awake()
    {
        _bgOrgColor = _background.color;
    }
    private void Start()
    {
        InspectorCheck();
    }


    public void Public_SetUp(GameObject p1, GameObject p2)
    {
        PlayerHealthHandler healthHandler = _isP1 ? p1.GetComponent<PlayerHealthHandler>() : p2.GetComponent<PlayerHealthHandler>();
        _maxHealth = healthHandler.health;
        _healthPerStage = _maxHealth / _healthLoopColor.Length;

        if (_healthLoopColor.Length == 0)
        {
            _slider.maxValue = _maxHealth;            
            healthHandler.OnHealthDecreaseEvent.AddListener(UpdateUI_WithoutStage);
            healthHandler.OnHealthDecreaseOverTimerEvent.AddListener(UpdateUI_WithoutStage);
            healthHandler.OnHealthIncreaseEvent.AddListener(UpdateUI_WithoutStage);
            UpdateUI_WithoutStage(_maxHealth);
        }
        else
        {
            _slider.maxValue = _healthPerStage;
            healthHandler.OnHealthDecreaseEvent.AddListener(UpdateUI_WithStage);
            healthHandler.OnHealthDecreaseOverTimerEvent.AddListener(UpdateUI_WithStage);
            healthHandler.OnHealthIncreaseEvent.AddListener(UpdateUI_WithStage);
            UpdateUI_WithStage(_maxHealth);
        }
    }
    private void UpdateUI_WithStage(float health) 
    {
        _curStage = _healthLoopColor.Length - (int)Mathf.Ceil((health / _maxHealth) * _healthLoopColor.Length);

        if(_curStage != _healthLoopColor.Length)
            _fill.color = _healthLoopColor[_curStage];

        if (_curStage + 1 >= _healthLoopColor.Length)
            _background.color = _bgOrgColor;
        else
            _background.color = _healthLoopColor[_curStage + 1];

        float value =  health - ((_healthLoopColor.Length - _curStage - 1) * _healthPerStage);
        _slider.value = health <= 0 ? 0 : value;
    }
    private void UpdateUI_WithoutStage(float health)
    { 
        _slider.value = health;
    }
    private void InspectorCheck()
    {
        if (_healthLoopColor.Length == 1)
            Debug.LogError(GetType().Name + ".cs inspector check failed, _healthLoopColor.Length can not be 1", gameObject);
    }
}
