using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterFill : MonoBehaviour
{

    [SerializeField] Slider SusMeter;
    [SerializeField] Slider TrustMeter;

    [SerializeField] Shiba owner;

    private void Start()
    {
        if(owner == null)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SusMeter.value = owner.GetSuspicion();
        
        TrustMeter.value = owner.GetTrust();
        
        
    }
}
