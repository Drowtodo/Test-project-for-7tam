using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

/// <summary>
/// ����� ���������� �� ��������� ������� � �� ����������
/// </summary>
public class Generator : MonoBehaviour
{
    /// <summary>
    /// ��������� ����� ��� �������
    /// </summary>
    [SerializeField]
    private List<Color> ColorVariants;

    /// <summary>
    /// ������� ��� ���������� ������������ ��������
    /// </summary>
    private Dictionary<Color, Type> _figuresAbilitys =new(); 

    /// <summary>
    /// ��������� ����� ��� �������
    /// </summary>
    [SerializeField]
    private List<GameObject> FormVariants;

    /// <summary>
    /// ��������� �������� �������� ��� �������
    /// </summary>
    [SerializeField]
    private List<Sprite> AnimalVariants;

    /// <summary>
    /// ������������ ���-�� ����������� �������� ������� 
    /// </summary>
    [SerializeField, Range(1, 1000)]
    private int MaxTripleVariants;

    /// <summary>
    /// �������������� ���-�� ����� �������
    /// </summary>
    [SerializeField, Range(1, 10)]
    private int MaxTripleCounter;

    private bool IsTouchingFigure = false;
    private List<Figure> CurrentUsingFigures = new();

    /// <summary>
    /// ������� ���������� ��� ����� �� �������
    /// </summary>
    public UnityEvent<Figure> OnFigureClick;

    /// <summary>
    /// ������� ���������� � ������ ��������� �������
    /// </summary>
    public UnityEvent OnGeneratingStarts;

    /// <summary>
    /// ������� ���������� � ����� ��������� �������
    /// </summary>
    public UnityEvent OnGeneratingEnds;

    public int RestFiguresCount { get 
        {
            int i = 0;
            for (int j = transform.childCount - 1; j >= 0; j--)
            {
                if(transform.GetChild(j).gameObject.activeInHierarchy)
                {
                    i++;
                }
            }
            return i;
        } }


    private void Start()
    {
        _figuresAbilitys.Add(Color.red, typeof(Explosion));
        _figuresAbilitys.Add(Color.blue, typeof(Frozen));

        List<FigureSample> FigureSampleList = new();
        for (int i = 0; i < MaxTripleVariants; i++)
        {
            FigureSampleList.Add(new FigureSample(FormVariants[Random.Range(0, FormVariants.Count)],
                ColorVariants[Random.Range(0, ColorVariants.Count)],
                AnimalVariants[Random.Range(0, AnimalVariants.Count)],
                3 * Random.Range(1, MaxTripleCounter + 1)));//�������� ����� � ���������� ��������� �������
        }
        StartCoroutine(Generate(FigureSampleList));
    }

    /// <summary>
    /// �������� ����� ������� �� ����� � �������� ������ "������" �� ������� ����� �� �������
    /// </summary>
    /// <param name="FigureSampleList"></param>
    /// <returns></returns>
    private IEnumerator Generate(List<FigureSample> FigureSampleList)
    {
        OnGeneratingStarts?.Invoke();
        var waiter = new WaitForFixedUpdate();
        while(FigureSampleList.Count > 0)
        {
            if(!IsTouchingFigure)
            {
                int i = Random.Range(0, FigureSampleList.Count);
                var form = Instantiate(FigureSampleList[i].FormVariants, transform.position, FigureSampleList[i].FormVariants.transform.rotation, transform).GetComponent<Figure>();
                form.gameObject.SetActive(true);
                form.Init(FigureSampleList[i].BackgroundColor, FigureSampleList[i].AnimalSprite);
                form.OnClick.AddListener(OnFigureClickWrapper);

                AddAbility(form.gameObject, FigureSampleList[i].BackgroundColor);

                FigureSampleList[i].RestCount--;
                if (FigureSampleList[i].RestCount == 0)
                {
                    FigureSampleList.RemoveAt(i);
                }
            }
            yield return waiter;
        }
        OnGeneratingEnds?.Invoke();
    }


    /// <summary>
    /// ����� ��� ���������� ����������� ��������
    /// </summary>
    /// <param name="figure"></param>
    /// <param name="checkColor"></param>
    private void AddAbility(GameObject figure, Color checkColor)
    {
        if(_figuresAbilitys.ContainsKey(checkColor))
        {
            figure.AddComponent(_figuresAbilitys[checkColor]);
        }
    }

    /// <summary>
    /// ����� "������" ��� ����� �� �������
    /// </summary>
    /// <param name="figure"></param>
    private void OnFigureClickWrapper(Figure figure)
    {
        OnFigureClick?.Invoke(figure);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsTouchingFigure = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsTouchingFigure = false;
    }

    /// <summary>
    /// ���������� ������� ���������� ������� � �����-���, ������� ������������������� ������� ������� � �����-����
    /// </summary>
    /// <param name="bar"></param>
    public void OnFiguresAddedToBar(ChosenFigureBar bar)
    {
        CurrentUsingFigures = bar.GetFigures();
    }

    /// <summary>
    /// ����� ���������� ������� �� ���� � ������ ������� ������� � �����-����
    /// </summary>
    public void Refresh()
    {
        int count = transform.childCount;
        

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            var go = transform.GetChild(i).gameObject;
            if(go.activeInHierarchy)
            {
                Destroy(go);
            }
        }

        List<FigureSample> list = new();
        var uniqueFigures = GetUniqueFigureSample();
        list.AddRange(uniqueFigures);
        count -= uniqueFigures.Select(x => x.RestCount).Sum();
        for (int i = 0; i < count;)
        {
            int currentSampleCount = 3 * Random.Range(1, MaxTripleCounter + 1);
            int restCount = count - i;
            if (currentSampleCount > restCount)
            {
                if (restCount % 3 != 0)
                {
                    currentSampleCount = restCount - restCount % 3 + 3;
                }
                else
                {
                    currentSampleCount = restCount;
                }
            }
            list.Add(new FigureSample(FormVariants[Random.Range(0, FormVariants.Count)],
                ColorVariants[Random.Range(0, ColorVariants.Count)],
                AnimalVariants[Random.Range(0, AnimalVariants.Count)],
                currentSampleCount));
            i += currentSampleCount;
        }
        StartCoroutine(Generate(list));
    }

    /// <summary>
    /// ����� ��� ��������� ����� �������� ������� �� ������ ������� �� �����-����
    /// </summary>
    /// <returns></returns>
    private List<FigureSample> GetUniqueFigureSample()
    {
        List<FigureSample> list = new();
        List<Figure> temp = new(CurrentUsingFigures);
        while(temp.Count != 0)
        {
            Figure tf = temp[0];
            int i = 1;
            FigureSample fs = FigureSample.FromFigure(tf);
            temp.RemoveAt(0);
            for (int j = temp.Count - 1; j >= 0; j--)
            {
                if(tf == temp[j])
                {
                    temp.RemoveAt(j);
                    i++;
                }
            }
            fs.RestCount = 3 - i;
            list.Add(fs);
        }
        return list;
    }
}
