using Godot;
using System;

public class Main : Node2D
{
    private Position2D TopDeck;
    private Position2D BottomDeck;
    private Position2D PlayPosition;
    private PathFollow2D Start;
    private PathFollow2D Return;

    public override void _Ready()
    {
        base._Ready();
        TopDeck = GetNode<Position2D>("TopDeckPosition");
        BottomDeck = GetNode<Position2D>("Deck/BottomDeckPosition");
        PlayPosition = GetNode<Position2D>("Deck/PlayPosition");
        Start = GetNode<PathFollow2D>("StartPath/StartFollow");
        Return = GetNode<PathFollow2D>("StartPath/ReturnFollow");
    }

}
