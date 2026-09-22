public class PlantDatabase
{
    public static PlantData newPlant(int id)
    {
        PlantData newPlant = new PlantData();
        newPlant.id = id;
        newPlant.yield = 1;
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
                newPlant.value = 1;
                break;
        }
        return newPlant;
    }
}