using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    [SerializeField] NPCSO data;

    float Current_suspicion;
    float Current_trust;
    // Start is called before the first frame update
    void Start()
    {
        Current_suspicion = data.sus_level;
        Current_trust = data.trust_level;
    }

    // Update is called once per frame
    protected void Update()
    {
        AddSuspicion(-.00051f);
        Mathf.Clamp(GetSuspicion(), 0, 1);
    }

    public void AddSuspicion(float sus)
    {
        Current_suspicion += sus;
    }
    public void AddTrust(float trust)
    {
        Current_trust += trust;
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
