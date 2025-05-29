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

    
    public void OnWin()
    {
        Activate("������!", Color.green);
    }

    public void OnLose()
    {
        Activate("���������...", Color.red);
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
        SceneLoader.LoadScene(SceneManager.loadedSceneCount);
    }

}
