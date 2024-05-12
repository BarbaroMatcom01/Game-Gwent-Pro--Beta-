using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public static string Player1Name;
    public static string Player2Name;
    public static ScriptableObject Player1Faction;
    public static ScriptableObject Player2Faction;
    public static bool ReadyPlayer1;
    public static bool ReadyPlayer2;

    public static string SetPlayer1Name(string name)
    {
        Player1Name = name;
        return Player1Name;
    }

    public static string SetPlayer2Name(string name)
    {
        Player2Name = name;
        return Player2Name;
    }

    public void SetPlayer1Faction(ScriptableObject faction)
    {
        Player1Faction = faction;
    }

    public void SetPlayer2Faction(ScriptableObject faction)
    {
        Player2Faction = faction;
    }

    private void Update()
    {
        if(ReadyPlayer1 && ReadyPlayer2)
        {
            SceneManager.LoadScene("BattleBoard");
        }
    }
}
