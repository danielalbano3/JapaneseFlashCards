using Godot;
using System;
using System.Collections.Generic;

public class ShuffleDeck
{
  private static int[] CardIndexArray = new int[46];
  
  public static int[] GetShuffle()
  {
    int n = 46; //number of cards
    for (int i = 0; i < n; i++)
    {
      CardIndexArray[i] = i;
    } 

    Random random = new Random();
    while (n > 1)
    {
      n--;
      int k = random.Next(n + 1);
      int value = CardIndexArray[k];
      CardIndexArray[k] = CardIndexArray[n];
      CardIndexArray[n] = value;
    }

    return CardIndexArray;
  }

}