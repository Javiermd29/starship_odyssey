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
        
        int savedRound = PlayerPrefs.GetInt("BossRound", 4); // Retrieve the saved boss round value from PlayerPrefs, defaulting to 4 if not set
        bossRoundSlider.value = savedRound; // Set the slider's value to the saved round
        bossRoundText.text = savedRound.ToString(); // Update the displayed text to reflect the saved round value

        bossRoundSlider.onValueChanged.AddListener(UpdateBossRound);

    }

    public void UpdateBossRound(float value)
    {
        int round = Mathf.RoundToInt(value);  // Round the slider's float value to the nearest integer
        bossRoundText.text = round.ToString(); // Update the displayed text with the new round value
        PlayerPrefs.SetInt("BossRound", round); // Save the updated boss round value in PlayerPrefs
        PlayerPrefs.Save();
    }
}
