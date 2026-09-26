public static class TeaSimulation
{
    public static void UpdateThis(saveData save)
    {
        if (save == null || save.data == null || save.data.tea == null || save.data.tea.kettles == null)
            return;
        foreach (var item in save.data.tea.kettles)
            UpdateKettle(item, save.tickCount, save);
    }
    static void UpdateKettle(Kettledat kettle, long currentTick, saveData save)
    {
        if (kettle == null || !kettle.isUnlocked || kettle.heldID < 0 || kettle.ready)
            return;
        Teabag tea = TeaResolver.GetTeaByID(kettle.heldID, save);
        if (tea == null)
            return;
        long requiredTicks = GameManager.I.GetTicks(tea.growTime, tea.scale);
        if (requiredTicks <= 0)
            return;
        long elapsedTicks = currentTick - kettle.brewStartTick;
        if (elapsedTicks < requiredTicks)
            return;
        kettle.ready = true;
        RefreshKettleVisual(kettle);
    }
    static void RefreshKettleVisual(Kettledat data)
    {
        if (TeaShopManager.I == null)
            return;
        Kettle kettleObject = TeaShopManager.I.GetKettle(data.myPos);
        if (kettleObject != null)
            kettleObject.UpdateVisuals();
    }
}