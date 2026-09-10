using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterButtonManager : MonoBehaviour
{
    [SerializeField] Image buttonBackgroundImage, characterIconImage;
    [SerializeField] Sprite defaultSprite, selectedSprite;
    bool isSelected = false;
    CharacterData characterData;
    CharactersPanelUI charactersPanelUI;
    public void Initialize(CharacterData charData, CharactersPanelUI panelUI)
    {
        characterData = charData;
        charactersPanelUI = panelUI;
        characterIconImage.sprite = charData.characterIcon;
    }

    public void SelectCharacter()
    {
        isSelected = true;
        buttonBackgroundImage.sprite = selectedSprite;
        charactersPanelUI.LastCharButtonManager = this;
        CharacterSelectionManager.Instance.SelectedCharacter = characterData;
        ChooseCharacterUI.Instance.ShowCharacter(characterData);
    }
    public void DeselectCharacter()
    {
        isSelected = false;
        buttonBackgroundImage.sprite = defaultSprite;
    }
    public void CharacterButtonClickHandler()
    {
        Debug.Log($"charactersPanelUI: {charactersPanelUI}, LastCharButtonManager: {charactersPanelUI.LastCharButtonManager}");
        charactersPanelUI.LastCharButtonManager.DeselectCharacter();
        if (CharacterSelectionManager.Instance.SelectedCharacter == characterData) return; // Zaten bu karakter seçili ise bir þey yapma.
        SelectCharacter();
    }
}
