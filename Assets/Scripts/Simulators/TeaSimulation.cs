public static class TeaSimulation
{
    public static void UpdateThis(saveData save)
    {
        if (save == null || save.data == null || save.data.tea == null || save.data.tea.kettles == null)
            return;
        foreach (var item in save.data.tea.kettles)
            UpdateKettle(item, save.tickCount);
    }
    static void UpdateKettle(Kettledat kettle, long currentTick)
    {
        if (!kettle.isUnlocked)
            return;
        if (kettle.heldID < 0)
            return;
        if (kettle.ready)
            return;
        Teabag tea = TeaShopManager.I.GetTeaByID(kettle.heldID);
        if (tea == null)
            return;
        long requiredTicks = GameManager.I.GetTicks(tea.growTime, tea.scale);
        long elapsedTicks = currentTick - kettle.brewStartTick;
        if(elapsedTicks >= requiredTicks)
        {
            kettle.ready = true;
            GameManager.I.Save();
            Kettle kettleObject = TeaShopManager.I.GetKettle(kettle.myPos);
            if (kettleObject != null)
                kettleObject.UpdateVisuals();
        }
    }
}