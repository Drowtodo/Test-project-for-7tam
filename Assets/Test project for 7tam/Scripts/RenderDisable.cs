using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class RenderDisable : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }
}
