using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс содержит логику для работы панели окончания игры.
/// </summary>
public class GamesEndPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text Text;

    [SerializeField]
    private SceneLoader SceneLoader;

    [SerializeField]
    private AudioSource LoseSound;
    [SerializeField]
    private AudioSource WinSound;
    public void OnWin()
    {
        Activate("Победа!", Color.green);
        WinSound.Play();
    }

    public void OnLose()
    {
        Activate("Поражение...", Color.red);
        LoseSound.Play();
    }

    /// <summary>
    /// При вызове этого метода текущий объект становится активным. Текст приобретает заданное значение и цвет.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="textColor"></param>
    private void Activate(string text, Color textColor)
    {
        gameObject.SetActive(true);
        Text.text = text;
        Text.color = textColor;
    }

    /// <summary>
    /// Перезагрузка текущей сцены
    /// </summary>
    public void Restart()
    {
        SceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
