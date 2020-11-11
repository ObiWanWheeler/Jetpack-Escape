using System.Collections;
using UnityEngine;

public class SkyLightChargeController : MonoBehaviour //Controls transfer of energy between pack and skylight
{
    [SerializeField] private float startCharge = 100f;
    private float chargeLeft;

    [SerializeField] private float transferRate = 10f;  //Charge transferred per second

    private bool inUse = false;

    private void Start()
    {
        chargeLeft = startCharge;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inUse = true;
            StartCoroutine(WhileObjectInTrigger(other.gameObject));
        }
    }

    private IEnumerator WhileObjectInTrigger(GameObject player) //energy transferred linearly with time
    {
        PackCharger pack = player.GetComponentInChildren<PackCharger>();

        while (inUse) //If the player is still using this skylight
        {
            float amount = transferRate * Time.deltaTime;
            bool transferFailed = false;
            
            if (pack != null)
            {
                if (pack.inOutputMode)
                {
                    transferFailed = ReceiveCharge(pack, amount);
                }
                else
                {
                    transferFailed = OutputCharge(pack, amount);
                }                   
            }

            if (transferFailed)
                yield break;

            yield return null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inUse = false;
        }
    }

    private bool OutputCharge(PackCharger pack, float amount) //returns true if skylight failed to output charge
    {
        if (chargeLeft < 0)
        {
            Debug.Log("Skylight closed!");
            return true;
        }

        chargeLeft -= amount;
        pack.ChargePack(amount);

        return false;
    }

    private bool ReceiveCharge(PackCharger pack, float amount) //returns true if pack failed to output charge
    {
        if (pack.GetCharge() < 0)
        {
            Debug.Log("Pack empty!");
            return true;
        }

        chargeLeft += amount;
        pack.ChargePack(-amount);

        return false;
    }
}






