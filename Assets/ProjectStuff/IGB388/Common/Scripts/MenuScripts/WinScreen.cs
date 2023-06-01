using UnityEngine;

public class WinScreen : MonoBehaviour
{
    public GameObject[] completeCandles; // Array of complete candles
    public GameObject winScreen; // Reference to the win screen GameObject

    private void Update()
    {
        // Check if all complete candles are active
        bool allCandlesActive = CheckAllCandlesActive();

        // If all complete candles are active, trigger win condition
        if (allCandlesActive)
        {
            ActivateWinScreen();
        }
    }

    private bool CheckAllCandlesActive()
    {
        // Loop through the complete candles array
        foreach (GameObject candle in completeCandles)
        {
            // If any complete candle is not active, return false
            if (!candle.activeSelf)
            {
                return false;
            }
        }

        // All complete candles are active
        return true;
    }

    private void ActivateWinScreen()
    {
        winScreen.SetActive(true);
    }
}
