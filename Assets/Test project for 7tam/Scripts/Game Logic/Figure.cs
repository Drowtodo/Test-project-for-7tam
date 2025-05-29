using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// ����� ���������� �� ������ ��������� �������
/// </summary>
public class Figure : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer AnimalSpriteRenderer;
    [SerializeField]
    private SpriteRenderer BackgroundSpriteRenderer;

    private SpriteRenderer FormSprite;

    /// <summary>
    /// ������� ���������� �� ������� �� �������
    /// </summary>
    public UnityEvent<Figure> OnClick;

    private void Start()
    {
        FormSprite = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// ������������� �������
    /// </summary>
    /// <param name="color"></param>
    /// <param name="AnimalSprite"></param>
    public void Init(Color color, Sprite AnimalSprite)
    {
        BackgroundSpriteRenderer.color = color;
        AnimalSpriteRenderer.sprite = AnimalSprite;
    }

    private void OnMouseDown()
    {

        OnClick?.Invoke(this);
    }

    private void OnDestroy()
    {
        OnClick.RemoveAllListeners();
    }


    //���������� ���������� == � != ��� ��������
    public static bool operator ==(Figure a, Figure b)
    {
        if(a is null)
        {
            return b is null;
        }

        if(b is null)
        {
            return a is null;
        }

        return (a.FormSprite.sprite == b.FormSprite.sprite) && (a.AnimalSpriteRenderer.sprite == b.AnimalSpriteRenderer.sprite) && (a.BackgroundSpriteRenderer.color == b.BackgroundSpriteRenderer.color);
    }

    public static bool operator !=(Figure a, Figure b)
    {
        return !(a == b);
    }


    /// <summary>
    /// ����� ��� ��������� ��������� ���������� � �������
    /// </summary>
    /// <returns></returns>
    public (SpriteRenderer BackgroundSpriteRenderer, Sprite AnimalSprite) GetFigureInfo()
    {
        return (BackgroundSpriteRenderer, AnimalSpriteRenderer.sprite);
    }

}
