public class PlantDatabase
{
    public static PlantData newPlant(int id)
    {
        PlantData newPlant = new PlantData();
        newPlant.id = id;
        newPlant.yield = 1;
        newPlant.value = 1;
        switch (id)
        {
            case 0:
                newPlant.plantName = "Chamomile";
                newPlant.description = "A relaxing and floral scent";
                newPlant.growTime = 3;
                newPlant.vibe = vibe.chill;
                newPlant.value = 2;
                break;
            case 1:
                newPlant.plantName = "Blueberries";
                newPlant.description = "A sweet anti-oxidant fruit";
                newPlant.growTime = 5;
                newPlant.yield = 3;
                newPlant.vibe = vibe.energetic;
                break;
            case 2:
                newPlant.plantName = "Black tea";
                newPlant.description = "A bold base flavor";
                newPlant.growTime = 1;
                newPlant.vibe = vibe.energetic;
                break;
            case 3:
                newPlant.plantName = "Assam";
                newPlant.description = "A rich, malty flavour";
                newPlant.growTime = 2;
                newPlant.vibe = vibe.chill;
                break;
            case 4:
                newPlant.plantName = "Ceylon";
                newPlant.description = "A Brisk, lively finish";
                newPlant.growTime = 2;
                newPlant.vibe = vibe.energetic;
                break;
            case 5:
                newPlant.plantName = "Keemun";
                newPlant.description = "Chinese black tea for depth";
                newPlant.growTime = 1;
                newPlant.vibe = vibe.energetic;
                break;
        }
        return newPlant;
    }
}
