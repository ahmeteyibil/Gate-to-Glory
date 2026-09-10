using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;
    [SerializeField] List<CharacterData> characters;
    private void Awake()
    {   
        Instance = this;
    }
    public CharacterData GetPickedCharacter()
    {
        string savedID = PlayerPrefs.GetString(PrefKeys.PICKED_CHAR_ID, "bird_default");
        CharacterData charData = characters.FirstOrDefault(x => x.characterID == savedID);
        return charData;
    }
    public CharacterData GetCharByID(string id)
    { 
        CharacterData charData = characters.FirstOrDefault(x => x.characterID == id);
        return charData;
    }
}
