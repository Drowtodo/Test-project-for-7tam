using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;

/// <summary>
/// ����� ���������� �� ������ �����-����
/// </summary>
public class ChosenFigureBar : MonoBehaviour
{
    private List<BarSection> SectionsList;

    [SerializeField]
    private AudioSource FigureRemoveSound;

    /// <summary>
    /// ������ ������� � �������� ��������� UI
    /// </summary>
    [SerializeField]
    private GameObject FigureUIPrefab;

    /// <summary>
    /// ������� ���������� ����� ���������� ������� � �������� �� ����������� ���������
    /// </summary>
    public UnityEvent<ChosenFigureBar> OnFiguresCheck;

    /// <summary>
    /// ������� ���-�� ������� � �����-����
    /// </summary>
    public int FiguresCount { get; private set; } = 0;
    private void Start()
    {
        SectionsList = new();
        for (int i = 0; i< transform.childCount; i++)
        {
            SectionsList.Add(transform.GetChild(i).gameObject.GetComponent<BarSection>());
        }
    }

    /// <summary>
    /// ����� ��� ��������� ������� ����� �� ������� � ���������� � � ������ ��������� ������ �����-���� 
    /// </summary>
    /// <param name="figure"></param>
    public void OnFigureClick(Figure figure)
    {
        foreach(var section in SectionsList)
        {
            if(section.IsFree())
            {
                section.Set(figure, FigureUIPrefab);
                break;
            }
        }
        CheckFigures(figure);
    }


    /// <summary>
    /// ����� ��� �������� ����������� ������� �� ������������ � �������� ��� ������ 3 ���������� ������� � �����-����
    /// </summary>
    /// <param name="figure"></param>
    private void CheckFigures(Figure figure)
    {
        List<int> temp = new();
        for(int i =0; i< SectionsList.Count; i++)
        {
            if (!SectionsList[i].IsFree() && SectionsList[i].EqualityCheck(figure))
            {
                temp.Add(i);
            }
        }

        if(temp.Count == 3)
        {
            for(int i = 0; i< temp.Count; i++)
            {
                SectionsList[temp[i]].Remove();
            }
            FiguresCount -= 2;
            FigureRemoveSound.Play();
            if(figure.TryGetComponent(out FigureAbility ability))
            {
                ability.Use(gameObject);
            }
        }
        else
        {
            FiguresCount++;
        }

        OnFiguresCheck?.Invoke(this);
    }


    /// <summary>
    /// ����� ���������� ��� �������, ������� ���� � �����-����
    /// </summary>
    /// <returns></returns>
    public List<Figure> GetFigures()
    {
        return SectionsList.Where(x => !x.IsFree()).Select(x => x.Figure).ToList();
    }

    public void Clear()
    {
        SectionsList.ForEach((x) => x.Remove());
        FiguresCount = 0;
    }
}
