using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSelector : MonoBehaviour
{

    public Slider bossRoundSlider;
    public TextMeshProUGUI bossRoundText;

    void Start()
    {

        // Cargar el valor almacenado o usar 4 por defecto
        int savedRound = PlayerPrefs.GetInt("BossRound", 4);
        bossRoundSlider.value = savedRound;
        bossRoundText.text = savedRound.ToString();

        // Suscribirse al evento para detectar cambios en el slider
        bossRoundSlider.onValueChanged.AddListener(UpdateBossRound);

    }

    // Actualizar el valor de la ronda
    public void UpdateBossRound(float value)
    {
        int round = Mathf.RoundToInt(value);
        bossRoundText.text = round.ToString();
        PlayerPrefs.SetInt("BossRound", round);
        PlayerPrefs.Save();
    }
}
