using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


/// <summary>
/// Класс отвечающий за логику поведения фигурки
/// </summary>
public class Figure : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer AnimalSpriteRenderer;
    [SerializeField]
    private SpriteRenderer BackgroundSpriteRenderer;

    private SpriteRenderer FormSprite;

    [SerializeField]
    private AudioSource CollisionSound;

    private bool _onClickEnabled = true;

    /// <summary>
    /// Событие вызываемое по нажатию на фигурку
    /// </summary>
    public UnityEvent<Figure> OnClick;

    private void Start()
    {
        FormSprite = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Инициализация фигурки
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
        if(UIChecker.IsPointerOverUI(Pointer.current.position.ReadValue()))
        {
            return;
        }
        
        if(!_onClickEnabled)
        {
            return;
        }

        OnClick?.Invoke(this);
    }

    

    private void OnDestroy()
    {
        OnClick.RemoveAllListeners();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.sqrMagnitude >= 3f)
        {
            CollisionSound.Play();
        }
    }

    //Перегрузка операторов == и != для удобства
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
    /// Метод для получения подробной информации о фигурки
    /// </summary>
    /// <returns></returns>
    public (SpriteRenderer BackgroundSpriteRenderer, Sprite AnimalSprite) GetFigureInfo()
    {
        return (BackgroundSpriteRenderer, AnimalSpriteRenderer.sprite);
    }

    public void SetOnClickEnabled(bool state)
    {
        _onClickEnabled = state;
    }
}
