using UnityEngine;
using UnityEngine.Events;


/// <summary>
///  ласс отвечающиий за логику победы/поражени€
/// </summary>
public class GameEndsController : MonoBehaviour
{
    [SerializeField]
    private Generator FigureGenerator;
    [SerializeField]
    private int MaxFigureInBar = 7;

    public UnityEvent OnWin;
    public UnityEvent OnLose;

    public void CheckState(ChosenFigureBar figuresBar)
    {
        if(figuresBar.FiguresCount >= MaxFigureInBar)
        {
            Lose();
            return;
        }

        if(FigureGenerator.RestFiguresCount == 0)
        {
            if(figuresBar.FiguresCount == 0)
            {
                Win();
            }
            else
            {
                Lose();
            }
            
        }
    }

    private void Win()
    {
        OnWin?.Invoke();
    }
    
    private void Lose()
    {
        OnLose?.Invoke();
    }
}
