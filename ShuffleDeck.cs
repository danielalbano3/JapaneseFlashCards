using Godot;
using System;
using System.Collections.Generic;

public class ShuffleDeck
{
  private static int[] CardIndexArray = new int[92];
  
  public static int[] GetShuffle()
  {
    for (int i = 0; i < 46; i++)
    {
      CardIndexArray[i] = i;
    }
    for (int j = 0; j < 46; j++)
    {
      CardIndexArray[j + 46] = j + 50;
    }
    
    Random random = new Random();
    int n = 92; //number of cards
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