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

    public int attackUpgrade = 50;
    public int criticalUpgrade = 50;
    public int autoClickUpgrade = 50;

    private void Start()
    {
        UpdateText();
        GameManager.Instance.ButtonController = this;
    }

    public void AttackButton()
    {
        if (GameManager.Instance.Stage.gold >= attackUpgrade)
        {
            GameManager.Instance.Stage.gold -= attackUpgrade;
            GameManager.Instance.Player.controller.damage *= 2;
            attackUpgrade *= 2;
            AttackUpdateText(attackUpgrade, GameManager.Instance.Player.controller.damage);
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
            CriticalUpdateText(criticalUpgrade, GameManager.Instance.Player.controller.critical);
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
            AutoClickUpdateText(autoClickUpgrade, GameManager.Instance.Player.controller.autoClickTime);
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
        AttackUpdateText(attackUpgrade, GameManager.Instance.Player.controller.damage);
        CriticalUpdateText(criticalUpgrade, GameManager.Instance.Player.controller.critical);
        AutoClickUpdateText(autoClickUpgrade, GameManager.Instance.Player.controller.autoClickTime);
    }

    public void AttackUpdateText(int attackGold, int damage)
    {
        attackButtonText.text = $"{attackGold}G";
        attackText.text = $"Damage : {damage} ¡æ {damage * 2}";
        GameManager.Instance.useGold?.Invoke();
    }

    public void CriticalUpdateText(int criticalGold, float critical)
    {
        criticalButtonText.text = $"{criticalGold}G";
        criticalText.text = $"Critical : {critical}% ¡æ {critical + 0.1f}%";
        GameManager.Instance.useGold?.Invoke();
    }

    public void AutoClickUpdateText(int autoClickGold, float autoClickTime)
    {
        float autotTime = autoClickTime - 0.1f;
        autoClickTimeButtonText.text = $"{autoClickGold}G";
        autoClickTimeText.text = $"AutoClickTime : {autoClickTime.ToString("N1")}s ¡æ {autotTime.ToString("N1")}s";
        GameManager.Instance.useGold?.Invoke();
    }

    void SaveGame()
    {
        SaveLoadManager.Instance.PlayerGameData(GameManager.Instance.Player.controller.autoClickTime, GameManager.Instance.Player.controller.damage, GameManager.Instance.Player.controller.critical);
        SaveLoadManager.Instance.ShopGameData(attackUpgrade, criticalUpgrade, autoClickUpgrade);
        SaveLoadManager.Instance.SaveAsJson();
    }
}
