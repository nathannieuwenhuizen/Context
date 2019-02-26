using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SessionData
{
    public static Session CSESSION;
}
public class Session
{
    public List<string> players = new List<string> {"person start" , "person middle", "person final"}; //how many players it will be
    public int timePerRound = 60; // in sec
    public int character; // the ober/waitress
}