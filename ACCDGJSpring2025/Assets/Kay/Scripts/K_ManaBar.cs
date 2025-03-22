using UnityEngine;
using UnityEngine.UI;

public class K_ManaBar : MonoBehaviour
{
    K_WizardBehavior k_WizardBehavior;
    Slider _manaSlider;

    void Start()
    {
        k_WizardBehavior = FindFirstObjectByType<K_WizardBehavior>();
        _manaSlider = GetComponent<Slider>();

        _manaSlider.maxValue = k_WizardBehavior.wizardStats.MaxMana;
        _manaSlider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _manaSlider.value = k_WizardBehavior.currentMana;
    }
}
