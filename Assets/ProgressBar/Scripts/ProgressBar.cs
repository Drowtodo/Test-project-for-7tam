using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Класс для реализации горизонтальной шкалы прогресса
/// </summary>
public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image _fillArea;

    /// <summary>
    /// Заполнение прогресс бара
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public void MoveProgress(float progress)
    {
        _fillArea.fillAmount = progress;
    }

    
    private void Start()
    {
        //Настройка зоны заполнения
        if(_fillArea == null)
        {
            _fillArea.type = Image.Type.Filled;
            _fillArea.fillMethod = Image.FillMethod.Horizontal;
            _fillArea.fillOrigin = (int)Image.OriginHorizontal.Left;
            _fillArea.fillAmount = 0;
        }
    }
}
