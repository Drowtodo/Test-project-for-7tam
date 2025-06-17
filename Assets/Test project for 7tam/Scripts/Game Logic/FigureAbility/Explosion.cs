using System.Collections;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Фигурка с этой способностью очищает экшен бар при сборе 3 фигурок в баре
/// </summary>
public class Explosion : FigureAbility
{
    public override void Use(GameObject source)
    {
        var go = new GameObject();
        var rt = go.AddComponent<RectTransform>();
        if(source.TryGetComponent(out RectTransform sourceRT))
        {
            rt.position = sourceRT.position;
            rt.anchoredPosition = sourceRT.anchoredPosition;
            rt.rotation = sourceRT.rotation;
            rt.anchorMax = sourceRT.anchorMax;
            rt.anchorMin = sourceRT.anchorMin;
            rt.pivot = sourceRT.pivot;
            rt.offsetMax = sourceRT.offsetMax;
            rt.offsetMin = sourceRT.offsetMin;
        }
        var img = Instantiate(go, source.transform.parent).AddComponent<Image>();
        img.color = Color.red;
        source.GetComponent<ChosenFigureBar>().Clear();
        img.StartCoroutine(Remover(img.gameObject));
        Destroy(go);
    }

    private IEnumerator Remover(GameObject go)
    {
        yield return new WaitForSecondsRealtime(1);
        Destroy(go);
    }
}
