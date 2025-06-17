using UnityEngine;

/// <summary>
/// ����� ���������� �� ������ ������ �����-����
/// </summary>
public class BarSection : MonoBehaviour
{
    public Figure Figure { get; private set; }
    private FigureUI figureUI;
    private bool _isFree = true;

    /// <summary>
    /// ����� ��� ��������� ����� ������� � ������
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
    /// ����� ���������� �������� ������� � �������� � ������
    /// </summary>
    /// <param name="figure"></param>
    /// <returns></returns>
    public bool EqualityCheck(Figure figure)
    {
        return Figure == figure;
    }

    /// <summary>
    /// ����� ��� �������� ������� �� ������
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
