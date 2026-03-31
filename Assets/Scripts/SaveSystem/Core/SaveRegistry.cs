using System.Collections.Generic;

public static class SaveRegistry
{
    private static List<SaveableObject> saveables = new List<SaveableObject>();

    public static void Register(SaveableObject obj)
    {
        if (!saveables.Contains(obj))
            saveables.Add(obj);
    }

    public static void Unregister(SaveableObject obj)
    {
        if (saveables.Contains(obj))
            saveables.Remove(obj);
    }

    public static List<SaveableObject> GetAll()
    {
        return saveables;
    }
}
