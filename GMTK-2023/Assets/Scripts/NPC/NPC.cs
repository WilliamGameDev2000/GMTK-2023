using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    [SerializeField] NPCSO data;

    [SerializeField] GameObject ViewCone;

    float Current_suspicion;
    float Current_trust;
    // Start is called before the first frame update
    protected void Start()
    {
        Current_suspicion = data.sus_level;
        Current_trust = data.trust_level;
    }

    // Update is called once per frame
    protected void Update()
    {
        if(ViewCone.GetComponent<FieldOfView>().visibleTargets.Count != 0)
        {
            AddSuspicion(data.sus_gain_rate);
        }

        AddSuspicion(-data.sus_lose_rate);
    }

    public void AddSuspicion(float sus)
    {
        Current_suspicion += sus;
        Current_suspicion = Mathf.Clamp(Current_suspicion, 0, 1);
    }
    public void AddTrust(float trust)
    {
        Current_trust += trust;
        Current_trust = Mathf.Clamp(Current_trust, 0, 1);
    }

    public float GetSuspicion()
    {
        return Current_suspicion;
    }
    public float GetTrust()
    {
        return Current_trust;
    }
}
