using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseCharacterUI : MonoBehaviour
{
    public static ChooseCharacterUI Instance;

    [SerializeField] Image selectedCharImage;
    [SerializeField] TMP_Text selectedCharText;
    [SerializeField] GameObject pickButtonObject;
    private void Awake()
    {
        Instance = this;
    }
    public void ShowCharacter(CharacterData charData)
    {
        selectedCharText.text = charData.characterName;
        selectedCharImage.sprite = charData.characterIcon;
        if (charData.characterID != PlayerPrefs.GetString(PrefKeys.PICKED_CHAR_ID))
        {
            pickButtonObject.SetActive(true);
        }
        else
        {
            pickButtonObject.SetActive(false);
        }
    }
    public void BackButtonHandler()
    {
        SceneManager.LoadSceneAsync(SceneName.SN_MENU);
    }
    public void PickButtonHandler()
    {
        CharacterSelectionManager.Instance.PickCharacter(CharacterSelectionManager.Instance.SelectedCharacter.characterID);
        pickButtonObject.SetActive(false);
    }
}
