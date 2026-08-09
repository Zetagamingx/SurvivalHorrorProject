using System.Collections.Generic;

public static class SaveRegistry
{
    private static readonly List<SaveableObject> saveables = new();

    public static void Register(SaveableObject obj)
    {
        if (!saveables.Contains(obj))
            saveables.Add(obj);
    }

    public static void Unregister(SaveableObject obj)
    {
        saveables.Remove(obj);
    }

    public static List<SaveableObject> GetAll()
    {
        return saveables;
    }
}