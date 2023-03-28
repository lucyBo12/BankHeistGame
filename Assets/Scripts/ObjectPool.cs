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

    public static GameObject Get(GameObject pool)
    {
        foreach (Transform obj in pool.transform)
        {
            if (obj.gameObject.activeSelf) continue;

            var rb = obj.GetComponent<Rigidbody>();
            if (rb != null) { 
                rb.isKinematic = false;
            }

            return obj.gameObject;
        }

        // Create a new object when there are no available objects in the pool
        GameObject prefab = pool.transform.GetChild(0).gameObject;
        var newObj = Object.Instantiate(prefab, pool.transform);
        newObj.name = $"{pool.name} Object [{(pool.transform.childCount + 1)}]";
        newObj.SetActive(false);
        Debug.LogWarning($"Created new object for {pool.name} due to insufficient pool size.");
        return newObj;
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

            var rb = obj.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.isKinematic = true;
            }

            obj.SetActive(false);
        }
    }

    private static GameObject GetResource(string name) => Resources.Load<GameObject>($"PoolObjects/{name}");

}
