using UnityEngine;
using UnityEngine.EventSystems;

public static class UIChecker
{
    /// <summary>
    /// Проверка был ли клик по UI
    /// </summary>
    /// <param name="screenPosition"></param>
    /// <returns></returns>
    public static bool IsPointerOverUI(Vector2 screenPosition)
    {
        PointerEventData eventData = new(EventSystem.current);
        eventData.position = screenPosition;

        var raycastResults = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        return raycastResults.Count > 0;
    }
}
