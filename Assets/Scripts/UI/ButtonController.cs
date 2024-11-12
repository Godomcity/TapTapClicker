using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI attackButtonText;
    [SerializeField] TextMeshProUGUI criticalButtonText;
    [SerializeField] TextMeshProUGUI autoClickTimeButtonText;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI criticalText;
    [SerializeField] TextMeshProUGUI autoClickTimeText;
    [SerializeField] GameObject buyPopUp;
    [SerializeField] Text buyText;

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

            buyPopUp.SetActive(true);
            buyText.text = "업그레이드 완료";
        }
        else
        {
            buyPopUp.SetActive(true);
            buyText.text = "돈이 부족합니다.";
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

            buyPopUp.SetActive(true);
            buyText.text = "업그레이드 완료";
        }
        else
        {
            buyPopUp.SetActive(true);
            buyText.text = "돈이 부족합니다.";
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

            buyPopUp.SetActive(true);
            buyText.text = "업그레이드 완료";
        }
        else
        {
            buyPopUp.SetActive(true);
            buyText.text = "돈이 부족합니다.";
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
        attackText.text = $"Damage : {damage} → {damage * 2}";
        GameManager.Instance.useGold?.Invoke();
    }

    public void CriticalUpdateText(int criticalGold, float critical)
    {
        criticalButtonText.text = $"{criticalGold}G";
        criticalText.text = $"Critical : {critical}% → {critical + 0.1f}%";
        GameManager.Instance.useGold?.Invoke();
    }

    public void AutoClickUpdateText(int autoClickGold, float autoClickTime)
    {
        float autotTime = autoClickTime - 0.1f;
        autoClickTimeButtonText.text = $"{autoClickGold}G";
        autoClickTimeText.text = $"AutoClickTime : {autoClickTime.ToString("N1")}s → {autotTime.ToString("N1")}s";
        GameManager.Instance.useGold?.Invoke();
    }

    void SaveGame()
    {
        SaveLoadManager.Instance.PlayerGameData(GameManager.Instance.Player.controller.autoClickTime, GameManager.Instance.Player.controller.damage, GameManager.Instance.Player.controller.critical);
        SaveLoadManager.Instance.ShopGameData(attackUpgrade, criticalUpgrade, autoClickUpgrade);
        SaveLoadManager.Instance.SaveAsJson();
    }

    public void ExitPopUp()
    {
        buyPopUp.SetActive(false);
    }
}
