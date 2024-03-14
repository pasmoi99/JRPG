using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
public class SelectionManager : MonoBehaviour
{
    private static SelectionManager _instance;
    public static SelectionManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance == null) _instance = this;
    }
    public Material OutlineMaterial, DefaultMaterial;
    private Character _selectedCharacter;
    public GameUI UI;
    public SelectionMode CurrentSelectionMode;
    public List<SelectionInstructions> SelectionInstructionsTexts;
    private bool PlayerHaveClickedOnCharacter = false;

    public void Update()
    {
        if (_selectedCharacter == null && !UI.InstructionText.gameObject.activeSelf)
        {
            UI.UpdateUI(instructionText: GetSelectionInstructionsText(CurrentSelectionMode));
        }
        if ((CurrentSelectionMode != SelectionMode.EnemyTurn && ! PlayerHaveClickedOnCharacter ) || (CurrentSelectionMode != SelectionMode.EnemyTurn && _selectedCharacter.IsSelected==false )) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider != null)
            {
                if (!hit.collider.TryGetComponent<Character>(out var character)) return;
                if (CurrentSelectionMode == SelectionMode.EnemyToAttack)
                {
                    if (_selectedCharacter.GetType() == typeof(Ally)) ((Ally)_selectedCharacter).Attack(character);
                }
                HoverCharacter(character);
                if (hit.collider != _selectedCharacter.Collider)
                {
                    _selectedCharacter.Visual.material = DefaultMaterial;
                }
            }
            
        }
        else if (CurrentSelectionMode != SelectionMode.EnemyTurn && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider != null)
            {
                if (!hit.collider.TryGetComponent<Character>(out var character)) return;
                if (CurrentSelectionMode == SelectionMode.EnemyToAttack)
                {
                    if (_selectedCharacter.GetType() == typeof(Ally)) ((Ally)_selectedCharacter).Attack(character);
                }
                SelectCharacter(character);
                _selectedCharacter.IsSelected = true;
            }
        }
    }

    private string GetSelectionInstructionsText(SelectionMode selectionMode)
    {
        string result = "";
        foreach (SelectionInstructions instruction in SelectionInstructionsTexts)
        {
            if (instruction.SelectionMode == selectionMode) result += $"{instruction.SelectionInstructionsText}\n";
        }
        return result;
    }

    private void SelectCharacter<T>(T character) where T : Character
    {
        CurrentSelectionMode = SelectionMode.Default;
        if (_selectedCharacter != null) _selectedCharacter.Visual.material = DefaultMaterial;
        _selectedCharacter = character;
        _selectedCharacter.Visual.material = OutlineMaterial;
        UI.UpdateUI(_selectedCharacter);
    }

    private void HoverCharacter<T>(T chara) where T : Character
    {
        CurrentSelectionMode = SelectionMode.Default;
        if (_selectedCharacter != null) _selectedCharacter.Visual.material = DefaultMaterial;
        _selectedCharacter = chara;
        _selectedCharacter.Visual.material = OutlineMaterial;
        UI.UpdateUI(_selectedCharacter);
    }

    public void SetAttackMode()
    {
        
        if (_selectedCharacter == null || _selectedCharacter.GetType() == typeof(Enemy)) return;
        CurrentSelectionMode = SelectionMode.EnemyToAttack;
        UI.UpdateUI(instructionText: GetSelectionInstructionsText(CurrentSelectionMode));
    }

    public void EscapeBattle()
    {
        if (_selectedCharacter == null) return;
        SceneManager.LoadScene(0);
    }
    public void SetPlayerHasClickedOnCharacter()
    {
        PlayerHaveClickedOnCharacter = true;
    }
}
