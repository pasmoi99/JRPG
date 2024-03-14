using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image PortraitImage;
    public TMPro.TextMeshProUGUI LifeValue;
    public Image LifeFiller;
    public TMPro.TextMeshProUGUI InstructionText;
    public GameObject ActionsButtonsParent;
    private GameObject _lifeParent;
    private void Start()
    {
        _lifeParent = LifeValue.transform.parent.gameObject;
    }
    private void ShowInstructionText(string instructionText="")
    {
        ShowHUD(true, false, instructionText);
    }
    private void ShowCharacterHUD(bool isAlly = false)
    {
        ShowHUD(false, isAlly, null);
    }
    private void ShowHUD(bool showInstruction = true, bool isAlly = false, string instructionText = "")
    {
        InstructionText.gameObject.SetActive(showInstruction);
        if (!string.IsNullOrEmpty(instructionText))
        {
            InstructionText.text = instructionText;
        }
        _lifeParent.SetActive(!showInstruction);
        PortraitImage.gameObject.SetActive(!showInstruction);
        ActionsButtonsParent.SetActive(!showInstruction && isAlly);
    }
    public void UpdateUI(Character character = null, string instructionText = "")
    {
        if (character == null)
        {
            ShowInstructionText(instructionText);
            return;
        }
        ShowCharacterHUD(character.GetType() == typeof(Ally));
        PortraitImage.sprite = character.SpritePortrait;
        LifeValue.text = $"{character.Life}/{character.LifeMax}";
        LifeFiller.fillAmount = (float)character.Life / (float)character.LifeMax;
    }
}
