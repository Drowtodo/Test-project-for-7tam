using UnityEngine;

/// <summary>
///  ласс отвечающий за логику €чейки экшен-бара
/// </summary>
public class BarSection : MonoBehaviour
{
    public Figure Figure { get; private set; }
    private FigureUI figureUI;
    private bool _isFree = true;

    /// <summary>
    /// ћетод дл€ установки новой фигурки в €чейку
    /// </summary>
    /// <param name="figure"></param>
    /// <param name="FigureUIPrefab"></param>
    public void Set(Figure figure, GameObject FigureUIPrefab)
    {
        Figure = figure;
        figure.gameObject.SetActive(false);
        figureUI = Instantiate(FigureUIPrefab, transform).GetComponent<FigureUI>();
        figureUI.Init(figure);
        _isFree = false;
    }

    /// <summary>
    /// ћетод сравнивает заданную фигурку с фигуркой в €чейке
    /// </summary>
    /// <param name="figure"></param>
    /// <returns></returns>
    public bool EqualityCheck(Figure figure)
    {
        return Figure == figure;
    }

    /// <summary>
    /// ћетод дл€ удалени€ фигурки из €чейки
    /// </summary>
    public void Remove()
    {
        if(!_isFree)
        {
            Destroy(Figure.gameObject);
            Destroy(figureUI.gameObject);
            _isFree = true;
        }
    }

    
    public bool IsFree()
    {
        return _isFree;
    }

    
}
