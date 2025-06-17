using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;

/// <summary>
/// Класс отвечающий за логику экшен-бара
/// </summary>
public class ChosenFigureBar : MonoBehaviour
{
    private List<BarSection> SectionsList;

    [SerializeField]
    private AudioSource FigureRemoveSound;

    /// <summary>
    /// Префаб фигурки в качестве эллемента UI
    /// </summary>
    [SerializeField]
    private GameObject FigureUIPrefab;

    /// <summary>
    /// Событие вызываемое после добавления фигурки и проверки на соответсвие остальным
    /// </summary>
    public UnityEvent<ChosenFigureBar> OnFiguresCheck;

    /// <summary>
    /// Текущее кол-во фигурок в экшен-баре
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
    /// Метод для обработки события клика по фигурке и добавления её в первую свободную ячейку экшен-бара 
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
    /// Метод для проверки добавляемой фигурки на соответствие и удаление при наборе 3 одинаковых фигурок в экшен-баре
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
    /// Метод возвращает все фигурки, которые есть в экшен-баре
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
