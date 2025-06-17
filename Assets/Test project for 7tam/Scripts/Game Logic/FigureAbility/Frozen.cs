using UnityEngine;


/// <summary>
/// Фигурка с этой способностью заморожена пока на неё не кликнуть несколько раз
/// </summary>
public class Frozen : FigureAbility
{
    private Figure _figure;
    private SpriteRenderer _backGround;

    private int _clickCount = 0;
    private int _maxClickCount = 3;

    public override void Use(GameObject source)
    {
        
    }

    private void Start()
    {
        _figure =  GetComponent<Figure>();
        _figure.SetOnClickEnabled(false);
        _backGround = _figure.GetFigureInfo().BackgroundSpriteRenderer;
    }

    private void OnMouseDown()
    {
        if(_clickCount < _maxClickCount)
        {
            _clickCount++;
            _backGround.color = new Color(_backGround.color.r, _backGround.color.g + 0.2f * _backGround.color.b, 0.9f * _backGround.color.b, _backGround.color.a);
        }
        else
        {
            _figure.SetOnClickEnabled(true);
            Destroy(this);
        }
    }
}
