using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays a stamina bar that reflects the player's current stamina.
/// </summary>
public class StaminaUI : MonoBehaviour
{
    public PlayerController playerController;
    public Image staminaFill;
    public Color fullColour = Color.green;
    public Color emptyColour = Color.red;

    void Update()
    {
        if (playerController == null || staminaFill == null) return;
        float normalised = playerController.GetStaminaNormalised();
        staminaFill.fillAmount = normalised;
        staminaFill.color = Color.Lerp(emptyColour, fullColour, normalised);
    }
}