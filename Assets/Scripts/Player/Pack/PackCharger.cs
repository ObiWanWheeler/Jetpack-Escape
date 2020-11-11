using UnityEngine;

public class PackCharger : MonoBehaviour
{
    [SerializeField] private float charge;
    [SerializeField] private float maxCharge= 100f;
    public bool inOutputMode { get; private set; }

    private void Start()
    {
        charge = 0;
        inOutputMode = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            inOutputMode = !inOutputMode;
        }    

        if (charge == maxCharge)
        {
            GameEvents.current.PackFilled();
        }
    }

    public void ChargePack(float amount)
    {
        charge = Mathf.Clamp(charge + amount, 0, maxCharge);
    }

    public float GetCharge()
    {
        return charge;
    }

    public float GetMaxCharge() 
    {
        return maxCharge;
    }
}
