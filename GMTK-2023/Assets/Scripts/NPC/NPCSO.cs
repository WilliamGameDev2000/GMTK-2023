using UnityEngine;


[CreateAssetMenu(menuName = "Custom/NPCs/Data", fileName = "NewNPCData")]
public class NPCSO : ScriptableObject
{
    public GameObject prefab;
    //current levels
    public float sus_level;
    public float trust_level;
    //how fast they fill up
    public float sus_gain_rate;
    public float trust_gain_rate;
    //how fast they empty
    public float sus_lose_rate;
    public float trust_lose_rate;
}
