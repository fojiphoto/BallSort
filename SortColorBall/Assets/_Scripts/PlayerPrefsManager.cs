using UnityEngine;

namespace CBG.Scripts
{
    public class PlayerPrefsManager : MonoBehaviour
    {
        // Keys for PlayerPrefs

        private const string CoinsKey = "Coins";
        private const string LevelKey = "CurrentLevelIndex";  // Current Level Key
        internal const string RemoveAds = "remove_ads";
        internal const string InternalLevelCount = "InternalLevelCount;";

        public static readonly string LevelProgress = "LVLPRGRS";
        public static readonly string LoadedLevels = "LoadedLevels";
        public static readonly string TotalCoins = "TotalCoins";
        public static readonly string giftClaimed = "claim";
        public static readonly string SFx = "Sfx";
        public static readonly string Music= "Music";
        public static readonly string firstTimePlay= "firstTimePlay";

        // New keys for saving progression
        private const string CurrentProgressPointsKey = "CurrentProgressPoints";  // Progress points for the current level
        private const string CurrentTotalPointsKey = "CurrentTotalPoints";  // Total points for the current level



        // Maximum number of free hints allowed globally (for all time)
        private const int MaxHints = 3;

        public static void SetPlayerPref(string PrefName, int Value)
        {
            PlayerPrefs.SetInt(PrefName, Value);
        }
        public static int GetPlayerPref(string PrefName)
        {
            return PlayerPrefs.HasKey(PrefName) ? PlayerPrefs.GetInt(PrefName) : 0;
        }


        // New methods to save and retrieve progress points
        public static void SetCurrentProgressPoints(int points)
        {
            PlayerPrefs.SetInt(CurrentProgressPointsKey, points);
            PlayerPrefs.Save();
        }

        public static int GetCurrentProgressPoints()
        {
            return PlayerPrefs.HasKey(CurrentProgressPointsKey) ? PlayerPrefs.GetInt(CurrentProgressPointsKey) : 0;
        }

        public static void SetCurrentTotalPoints(int totalPoints)
        {
            PlayerPrefs.SetInt(CurrentTotalPointsKey, totalPoints);
            PlayerPrefs.Save();
        }

        public static int GetCurrentTotalPoints()
        {
            return PlayerPrefs.HasKey(CurrentTotalPointsKey) ? PlayerPrefs.GetInt(CurrentTotalPointsKey) : 100;  // Default is 100 points for level 1
        }

        // Existing methods...
        // Coins
        public static void SetCoins(int coins)
        {
            Debug.Log("Setting coins to: " + coins);
            PlayerPrefs.SetInt(CoinsKey, coins);
            PlayerPrefs.Save();
        }

        public static int GetCoins()
        {
            int coins = PlayerPrefs.HasKey(CoinsKey) ? PlayerPrefs.GetInt(CoinsKey) : 0;
            Debug.Log("Getting coins: " + coins);
            return coins;
        }

        // Deduct Coins - Return true if successful, false if not enough coins
        public static bool DeductCoins(int amount)
        {
            int currentCoins = GetCoins();
            if (currentCoins >= amount)
            {
                SetCoins(currentCoins - amount);  // Deduct the coins
                Debug.Log("Deducted " + amount + " coins. Remaining: " + (currentCoins - amount));
                return true;
            }
            else
            {
                Debug.Log("Failed to deduct " + amount + " coins. Not enough coins.");
                return false;
            }
        }

        // Level Progress
        public static void SetCurrentLevel(int levelIndex)
        {
            PlayerPrefs.SetInt(LevelKey, levelIndex);
            PlayerPrefs.Save();
        }

        public static int GetCurrentLevel()
        {
            return PlayerPrefs.HasKey(LevelKey) ? PlayerPrefs.GetInt(LevelKey) : 0;  // Default to level 0
        }


        // **Reset All** - Reset all stored values (if needed)
        public static void ResetAll()
        {
            PlayerPrefs.DeleteKey(CoinsKey);
            PlayerPrefs.DeleteKey(LevelKey);
            PlayerPrefs.Save();
        }
    }
}
