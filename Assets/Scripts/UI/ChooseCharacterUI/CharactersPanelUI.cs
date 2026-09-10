using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactersPanelUI : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject characterButtonPrefab;
    public CharacterButtonManager LastCharButtonManager { get; set; }
    public void Initialize(List<CharacterData> characters)
    {
        // Tüm karakterler için karakter buton oluþturulacak.
        foreach (var character in characters)
        {
            var newCharButton = Instantiate(characterButtonPrefab, transform); // Vertical layout group bu objede olduðu için parent'ý bu obje yaptýk.
            CharacterButtonManager newCharButtonManager = newCharButton.GetComponent<CharacterButtonManager>();
            newCharButton.GetComponent<CharacterButtonManager>().Initialize(character, this);
            if (character.characterID == PlayerPrefs.GetString(PrefKeys.PICKED_CHAR_ID, "bird_default"))
            {
                Debug.Log($"Picked Char ID matched with character: {PlayerPrefs.GetString(PrefKeys.PICKED_CHAR_ID)}");
                LastCharButtonManager = newCharButtonManager;
                newCharButtonManager.SelectCharacter();
            }
        }
    }
}

