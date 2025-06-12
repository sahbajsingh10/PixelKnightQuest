using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI hudText;
    public int level = 1;
    public int coinsCollected = 0;
    public int totalCoins = 10;
    public int lives = 3;

    private float timer = 0f;
    private bool isGameActive = true;

    void Update()
    {
        if (isGameActive)
        {
            timer += Time.deltaTime;
            UpdateHUD();
        }
    }

    public void UpdateCoins(int collected)
    {
        coinsCollected = collected;
        UpdateHUD();
    }

    public void UpdateLives(int newLives)
    {
        lives = newLives;
        UpdateHUD();
    }

    public void SetTotalCoins(int total)
    {
        totalCoins = total;
    }

    public void StopTimer()
    {
        isGameActive = false;
    }

    void UpdateHUD()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        string timeFormatted = $"{minutes:00}:{seconds:00}";

        hudText.text = $"Level {level}  Coins: {coinsCollected}/{totalCoins}  Lives: {lives}  Time: {timeFormatted}";
    }
}
