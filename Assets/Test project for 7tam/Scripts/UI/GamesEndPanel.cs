using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ����� �������� ������ ��� ������ ������ ��������� ����.
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
        Activate("������!", Color.green);
        WinSound.Play();
    }

    public void OnLose()
    {
        Activate("���������...", Color.red);
        LoseSound.Play();
    }

    /// <summary>
    /// ��� ������ ����� ������ ������� ������ ���������� ��������. ����� ����������� �������� �������� � ����.
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
    /// ������������ ������� �����
    /// </summary>
    public void Restart()
    {
        SceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
