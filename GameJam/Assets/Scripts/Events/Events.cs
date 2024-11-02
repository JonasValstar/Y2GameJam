using UnityEngine;


// The Game Events used across the Game.
// Anytime there is a need for a new event, it should be added here.

public static class Events
{
    // Jibbe's Events
    public static PlayerHitEvent PlayerHitEvent = new();
}

// Jibbe's Events
public class PlayerHitEvent : GameEvent
{
    public int dmg;
}