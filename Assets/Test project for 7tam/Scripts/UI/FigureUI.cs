using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс служащий для преобразования Figure в UI объект
/// </summary>
public class FigureUI : MonoBehaviour
{
    [SerializeField]
    private Image AnimalImage;
    [SerializeField]
    private Image BackgroundImage;
    [SerializeField]
    private Image FormImage;

    /// <summary>
    /// Метод инициализации
    /// </summary>
    /// <param name="figure"></param>
    public void Init(Figure figure)
    {
        var figureInfo = figure.GetFigureInfo();
        AnimalImage.sprite = figureInfo.AnimalSprite;
        FormImage.sprite = figureInfo.BackgroundSpriteRenderer.sprite;
        BackgroundImage.sprite = figureInfo.BackgroundSpriteRenderer.sprite;
        BackgroundImage.color = figureInfo.BackgroundSpriteRenderer.color;
    }
}
