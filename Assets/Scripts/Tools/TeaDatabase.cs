using System.Collections.Generic;
public class TeaDatabase
{
    public static Teabag newTea(int id)
    {
        Teabag newTea = new Teabag();
        newTea.id = id;
        newTea.growTime = 5;
        switch (id)
        {
            case 0:
                newTea.teaName = "English Breakfast";
                newTea.ingredients = new List<int> { 2, 3, 4, 5 };
                newTea.teaVibe = vibe.energetic;
                break;
        }
        return newTea;
    }
}
