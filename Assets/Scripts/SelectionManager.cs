using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    public Material OutlineMaterial, DefaultMaterial, OutlineMaterialHover;
    private Character _selectedCharacter;
    public GameUI UI;
    public SelectionMode CurrentSelectionMode;
    public List<SelectionInstructions> SelectionInstructionsTexts;
    //private bool PlayerHaveClickedOnCharacter = false;

    public void Update()
    {
        if (_selectedCharacter == null && !UI.InstructionText.gameObject.activeSelf)
        {
            UI.UpdateUI(instructionText: GetSelectionInstructionsText(CurrentSelectionMode));
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        if (CurrentSelectionMode != SelectionMode.EnemyTurn && CurrentSelectionMode != SelectionMode.ChacterSelected)
        {
            if (hit.collider != null)
            {
                if (!hit.collider.TryGetComponent<Character>(out var character)) return;

                if (CurrentSelectionMode == SelectionMode.EnemyToAttack && Input.GetMouseButtonDown(0))
                {
                    if (_selectedCharacter.GetType() == typeof(Ally)) ((Ally)_selectedCharacter).Attack(character);
                    CurrentSelectionMode = SelectionMode.Default;
                }



                HoverCharacter(character);
                if (hit.collider != _selectedCharacter.Collider)
                {
                    _selectedCharacter.Visual.material = DefaultMaterial;
                }
                if (Input.GetMouseButtonDown(0) && hit.collider == _selectedCharacter.Collider && CurrentSelectionMode != SelectionMode.EnemyToAttack)
                {
                    SelectCharacter(character);
                }
            }

        }
        if (_selectedCharacter != null && CurrentSelectionMode == SelectionMode.ChacterSelected)
        {
            if (hit.collider != null)
            {
                if (!hit.collider.TryGetComponent<Character>(out var character)) return;
                if (Input.GetMouseButtonDown(0) && hit.collider == character.Collider)
                {
                    SelectCharacter(character);
                }
            }
        }
        //else if (CurrentSelectionMode != SelectionMode.EnemyTurn && Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        //    if (hit.collider != null)
        //    {
        //        if (!hit.collider.TryGetComponent<Character>(out var character)) return;
        //        if (CurrentSelectionMode == SelectionMode.EnemyToAttack)
        //        {
        //            if (_selectedCharacter.GetType() == typeof(Ally)) ((Ally)_selectedCharacter).Attack(character);
        //        }
        //        SelectCharacter(character);
        //        //_selectedCharacter.IsSelected = true;
        //    }
        //}
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
        CurrentSelectionMode = SelectionMode.ChacterSelected;
        if (_selectedCharacter != null) _selectedCharacter.Visual.material = DefaultMaterial;
        _selectedCharacter = character;
        _selectedCharacter.Visual.material = OutlineMaterial;
        UI.UpdateUI(_selectedCharacter);
    }

    private void HoverCharacter<T>(T chara) where T : Character
    {
        if (CurrentSelectionMode != SelectionMode.EnemyToAttack)
        {
            CurrentSelectionMode = SelectionMode.Default;
            if (_selectedCharacter != null) _selectedCharacter.Visual.material = DefaultMaterial;
            _selectedCharacter = chara;
            _selectedCharacter.Visual.material = OutlineMaterialHover;
        }
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
    //public void SetPlayerHasClickedOnCharacter()
    //{
    //    PlayerHaveClickedOnCharacter = true;
    //}
}
