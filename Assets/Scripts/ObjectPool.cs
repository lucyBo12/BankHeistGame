using UnityEngine;

public static class ObjectPool 
{
    public static GameObject BulletPool { get; private set; }
    public static GameObject CivPool { get; private set; }
    public static GameObject CopPool { get; private set; }



    [RuntimeInitializeOnLoadMethod]
    private static void Initialize() {
        CreateParentObjects();
        AssignObjects(BulletPool, GetResource("9mm"), 100);
        AssignObjects(CivPool, GetResource("NPC/Civ"), 20);
        AssignObjects(CopPool, GetResource("NPC/Cop"), 40);
    }

    public static GameObject Get(GameObject pool) {
        foreach (Transform obj in pool.transform) {
            if (obj.gameObject.activeSelf) continue;
            return obj.gameObject;
        }

        Debug.LogError($"Out of available objects for {pool}");
        return null;
    }

    private static void CreateParentObjects() {

        GameObject pool = new GameObject("Pool");
        BulletPool = new GameObject("Bullet");
        BulletPool.transform.parent = pool.transform;

        CivPool = new GameObject("Civilians");
        CivPool.transform.parent = pool.transform;

        CopPool = new GameObject("Cops");
        CopPool.transform.parent = pool.transform;

        Object.DontDestroyOnLoad(pool);
    }

    private static void AssignObjects(GameObject parent, GameObject prefab, int quantity) {
        if (prefab == null) {
            Debug.LogError($"Did not find prefab for {parent.name}.");
            return;
        }
        for (int i = 0; i < quantity; i++) {
            var obj = Object.Instantiate(prefab, parent.transform);
            obj.name = $"{parent.name} Object [{(i + 1)}]";
            obj.SetActive(false);
        }
    }

    private static GameObject GetResource(string name) => Resources.Load<GameObject>($"PoolObjects/{name}");

}
