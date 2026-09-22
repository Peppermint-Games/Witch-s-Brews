public static class GardenSimulation
{
    public static void Update(saveData save)
    {
        if (save == null || save.data == null || save.data.plants == null)
            foreach (var item in save.data.plants)
                UpdatePlant(item, save.tickCount);
    }
    static void UpdatePlant(Plantdat plant, long currentTick)
    {
        if (plant == null || !plant.isUnlocked)
            return;
        PlantData def = PlantDatabase.newPlant(plant.heldID);
        long growthDuration = GameManager.I.GetTicks(def.growTime, def.scale);
        long ticksPassed = currentTick - plant.lastGrowthTick;
        if (def == null || growthDuration <=0 || ticksPassed < growthDuration)
            return;
        long completedCycles = ticksPassed / growthDuration;
        plant.harvestCount += (int)completedCycles * def.yield;
        plant.lastGrowthTick += completedCycles * growthDuration;
    }
}