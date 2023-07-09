using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    [SerializeField] NPCSO data;

    [SerializeField] GameObject ViewCone;

    float Current_suspicion;
    float Current_trust;

    GameObject badAction;

    // Start is called before the first frame update
    protected void Start()
    {
        //target = AIGameManager.instance.targetPlayer;
        Current_suspicion = data.sus_level;
        Current_trust = data.trust_level;

        badAction = GameObject.FindGameObjectWithTag("BADACTION");
    }

    // Update is called once per frame
    protected void Update()
    {
        if(ViewCone.GetComponent<FieldOfView>().visibleTargets.Count != 0 && gameObject != AIGameManager.instance.playerModels[AIGameManager.instance.targetPlayer])
        {
/*            if (badAction.activeInHierarchy)
            {
                AddSuspicion(.012f);
            }*/
            AddSuspicion(data.sus_gain_rate);
        }

        AddSuspicion(-data.sus_lose_rate);
    }

    public void AddSuspicion(float sus)
    {
        Current_suspicion += sus;
        Current_suspicion = Mathf.Clamp(Current_suspicion, 0, 1.2f);
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
