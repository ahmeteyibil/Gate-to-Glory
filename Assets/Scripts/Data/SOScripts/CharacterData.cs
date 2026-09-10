using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Character Data")]
public class CharacterData : ScriptableObject
{
    public string characterID;
    public string characterName;
    public GameObject prefab;
    public CharacterStats baseStats;
    public Sprite characterIcon;
}
