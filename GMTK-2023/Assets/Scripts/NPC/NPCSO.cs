using UnityEngine;


[CreateAssetMenu(menuName = "Custom/NPCs/Data", fileName = "NewNPCData")]
public class NPCSO : ScriptableObject
{
    public GameObject prefab;
    public float sus_level;
    public float trust_level;
}
