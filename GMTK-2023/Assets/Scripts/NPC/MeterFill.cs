using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterFill : MonoBehaviour
{

    enum MeterType { sus, trust}

    [SerializeField] MeterType meter_type;

    [SerializeField] Slider meter;

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
        switch (meter_type)
        {
            case MeterType.sus:
                meter.value = owner.GetSuspicion();
                break;
            case MeterType.trust:
                meter.value = owner.GetTrust();
                break;
        }
        
    }
}
