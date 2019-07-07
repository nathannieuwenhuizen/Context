using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SessionData
{
    public static Session CSESSION;

    public static string Melissa = "M e l i s s a   -   3 0";
    public static string John = "J o h n   -   2 8";
    public static bool HasHighestScore(int index)
    {
        int score = SessionData.CSESSION.players[index].score;

        if (score == 0)
        {
            return false;
        }

        for (int i = 0; i < SessionData.CSESSION.players.Count; i++)
        {
            if (score < SessionData.CSESSION.players[i].score)
            {
                return false;
            }
        }
        return true;
    }

}
public class Session
{
    public int player_count = 2;
    public float timeSavedAtFirstRound = 0;
    public int points = 0;
    public int stellingStartIndex = 0;
    public List<int> stellingen = new List<int> { 0, 1 };
    public int opinionLikes = 0;
    public int character = 0; // the ober/waitress

    //old
    public List<Player> players = new List<Player> { new Player("Player start"), new Player("Player middle"), new Player("Player end") }; //how many players it will be
    public int timePerRound = 60; // in sec

}
public struct Player
{
    public Player(string _name, int _score = 0)
    {
        name = _name;
        score = _score;
    }
    public string name;
    public int score;
}