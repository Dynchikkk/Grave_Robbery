using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ValuesUI : MonoBehaviour
{
    [Header("Money Elements")]
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _moneyGiveText;

    [Header("Exp Elements")]
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _xpGiveText;
    [SerializeField] private Slider _xpProgress;

    public void SetMoneyText(int value)
    {
        string allMoney = MainLogic.main.money.AllMoney.ToString();

        SetGiveMoney(value);

        _moneyText.text = allMoney;
    }

    public void SetGiveMoney(int value)
    {
        string countOfMoney = "";
        if (value >= 0)
        {
            countOfMoney += "+";
        }
        countOfMoney += value.ToString();
        _moneyGiveText.text = countOfMoney;

        _moneyGiveText.gameObject.SetActive(false);
        _moneyGiveText.gameObject.SetActive(true);
    }

    public void SetXpGiveText(int value)
    {
        string countOfExp = "";
        if (value >= 0)
        {
            countOfExp += "+";
        }
        countOfExp += value.ToString();
        _xpGiveText.text = countOfExp;

        _xpGiveText.gameObject.SetActive(false);
        _xpGiveText.gameObject.SetActive(true);
    }

    public void SetSliderXp(int value, int maxValue)
    {
        float slidValueInPer = (value * 100) / maxValue;
        float slidValue = slidValueInPer / 100;

        _xpProgress.value = slidValue;
    }

    public void SetLevel(int num)
    {
        _level.text = num.ToString();
    }
}
