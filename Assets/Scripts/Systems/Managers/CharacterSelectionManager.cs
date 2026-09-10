using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour
{
    public static CharacterSelectionManager Instance;
    [SerializeField] CharactersPanelUI charactersPanelUI;
    [SerializeField] List<CharacterData> characters;
    public CharacterData SelectedCharacter { get; set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        charactersPanelUI.Initialize(characters);
    }
    public void PickCharacter(string characterID)
    {
        PlayerPrefs.SetString(PrefKeys.PICKED_CHAR_ID, characterID);
    }
}
