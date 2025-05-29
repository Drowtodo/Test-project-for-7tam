using UnityEngine;

/// <summary>
/// Класс шаблона фигурок
/// </summary>
public class FigureSample
{
    public GameObject FormVariants { get; }
    public Color BackgroundColor { get; }
    public Sprite AnimalSprite { get; }
    public int RestCount { get; set; }

    public FigureSample(GameObject formVariants, Color backgroundColor, Sprite animalSprite, int restCount)
    {
        FormVariants = formVariants;
        BackgroundColor = backgroundColor;
        AnimalSprite = animalSprite;
        RestCount = restCount;
    }

    /// <summary>
    /// Метод возращающий шаблон фигурок на основе фигурки
    /// </summary>
    /// <param name="figure"></param>
    /// <param name="restCount"></param>
    /// <returns></returns>
    public static FigureSample FromFigure(Figure figure, int restCount = 1)
    {
        var finfo = figure.GetFigureInfo();
        return new FigureSample(figure.gameObject, finfo.BackgroundSpriteRenderer.color, finfo.AnimalSprite, restCount);
    }
}
