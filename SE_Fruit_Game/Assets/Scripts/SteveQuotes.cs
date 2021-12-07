
public static class SteveQuotes
{
    static string[] tileSelection = {"Pick a tile to reveal what's underneath it!", "Help me find all the vegetables!", "Find the carrots before they find me!!"};
    static string[] tileEmpty = {"Opps, there's nothing under that tile...", "No vegetables under this one...", "The vegetables are getting closer!!!", "Try again...!"};

    static string[] carrotFound = {"Hooray! You found a carrot!!!", "Yay, that's one carrot closer to being free!", "A carrot!!", "Be gone carrot."};
    static string[] bananaFound = {"You found Barry!", "Barry will push the vegetables back a square!", "Yes! You found my friend Barry!", "Barry has given you more time to free me!"}; 

    public static string TileSelection
    {
        get 
        {
            int i = UnityEngine.Random.Range(0, tileSelection.Length -1);
            return tileSelection[i];
        }
    }

    public static string TileEmpty
    {
        get 
        {
            int i = UnityEngine.Random.Range(0, tileEmpty.Length -1);
            return tileEmpty[i];
        }
    }

    public static string CarrotFound
    {
        get 
        {
            int i = UnityEngine.Random.Range(0, carrotFound.Length -1);
            return carrotFound[i];
        }
    }


    public static string BananaFound
    {
        get 
        {
            int i = UnityEngine.Random.Range(0, bananaFound.Length -1);
            return bananaFound[i];
        }
    }

}
