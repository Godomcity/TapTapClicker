using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI attackButtonText;
    [SerializeField] TextMeshProUGUI criticalButtonText;
    [SerializeField] TextMeshProUGUI autoClickTimeButtonText;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI criticalText;
    [SerializeField] TextMeshProUGUI autoClickTimeText;

    private int attackUpgrade = 50;
    private int criticalUpgrade = 50;
    private int autoClickUpgrade = 50;

    private float test = 0.1f;

    private void Start()
    {
        UpdateText();
    }

    public void AttackButton()
    {
        if (GameManager.Instance.Stage.gold >= attackUpgrade)
        {
            GameManager.Instance.Stage.gold -= attackUpgrade;
            GameManager.Instance.Player.controller.damage *= 2;
            attackUpgrade *= 2;
            AttackUpdateText();
            SaveGame();
            // TODO : ±¸¸Å ¿Ï·á ÆË¾÷
        }
        else
        {
            // TODO : °ñµå ºÎÁ· ÆË¾÷
        }
    }

    public void CriticalButton()
    {
        if (GameManager.Instance.Stage.gold >= criticalUpgrade)
        {
            GameManager.Instance.Stage.gold -= criticalUpgrade;
            GameManager.Instance.Player.controller.critical += 0.1f;
            criticalUpgrade *= 4;
            CriticalUpdateText();
            SaveGame();
            // TODO : ±¸¸Å ¿Ï·á ÆË¾÷
        }
        else
        {
            // TODO : °ñµå ºÎÁ· ÆË¾÷
        }
    }

    public void AutoClickButton()
    {
        if (GameManager.Instance.Stage.gold >= autoClickUpgrade)
        {
            GameManager.Instance.Stage.gold -= autoClickUpgrade;
            GameManager.Instance.Player.controller.autoClickTime -= 0.1f;
            autoClickUpgrade *= 3;
            AutoClickUpdateText();
            SaveGame();
            // TODO : ±¸¸Å ¿Ï·á ÆË¾÷
        }
        else
        {
            // TODO : °ñµå ºÎÁ· ÆË¾÷
        }
    }

    void UpdateText()
    {
        AttackUpdateText();
        CriticalUpdateText();
        AutoClickUpdateText();
    }

    void AttackUpdateText()
    {
        attackButtonText.text = $"{attackUpgrade}G";
        attackText.text = $"Damage : {GameManager.Instance.Player.controller.damage} ¡æ {GameManager.Instance.Player.controller.damage * 2}";
        GameManager.Instance.useGold?.Invoke();
    }

    void CriticalUpdateText()
    {
        criticalButtonText.text = $"{criticalUpgrade}G";
        criticalText.text = $"Critical : {GameManager.Instance.Player.controller.critical}% ¡æ {GameManager.Instance.Player.controller.critical + 0.1f}%";
        GameManager.Instance.useGold?.Invoke();
    }

    void AutoClickUpdateText()
    {
        float autotTime = GameManager.Instance.Player.controller.autoClickTime - 0.1f;
        autoClickTimeButtonText.text = $"{autoClickUpgrade}G";
        autoClickTimeText.text = $"AutoClickTime : {GameManager.Instance.Player.controller.autoClickTime.ToString("N1")}s ¡æ {autotTime.ToString("N1")}s";
        GameManager.Instance.useGold?.Invoke();
    }

    void SaveGame()
    {
        SaveLoadManager.Instance.UpdateGameData(GameManager.Instance.Stage.curentMonsterNumber, GameManager.Instance.Stage.stage, GameManager.Instance.Stage.gold, GameManager.Instance.Player.controller.autoClickTime, GameManager.Instance.Player.controller.damage, GameManager.Instance.Player.controller.critical);
        SaveLoadManager.Instance.SaveAsJson();
    }
}
