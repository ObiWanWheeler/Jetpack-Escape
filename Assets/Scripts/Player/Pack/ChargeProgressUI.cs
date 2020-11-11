using UnityEngine;
using UnityEngine.UI;

public class ChargeProgressUI : MonoBehaviour
{
    private Slider progressSlider;
    [SerializeField] private PackCharger pack = null;

    private void Start()
    {
        progressSlider = GetComponent<Slider>();
        progressSlider.value = 0;
    }

    private void Update()
    {
        if (pack == null)
            return;

        progressSlider.value = SetProgress();
    }

    private float SetProgress()
    {
        float chargePercent = pack.GetCharge() / pack.GetMaxCharge();
        return Mathf.Clamp(chargePercent, 0, 1);
    }
}
