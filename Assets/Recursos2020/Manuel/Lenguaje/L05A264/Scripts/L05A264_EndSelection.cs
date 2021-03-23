using UnityEngine;
using UnityEngine.UI;

public class L05A264_EndSelection : MonoBehaviour
{
    [Header("Toggle Group")] public ToggleGroup _group;
    [Header("Button Next")] public Button _continue;

    // Update is called once per frame
    void Update()
    {
        _continue.interactable = _group.AnyTogglesOn();
    }
}
