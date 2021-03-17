using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class RoleData {

	public RoleType RoleType { get; private set; }
	public GameObject RolePrefab{ get; private set; }
	public GameObject ArrowPrefab { get; private set; }
	public Vector3 SpawnPostion { get; private set; }

	public GameObject ExplostionEffect { get; private set; }

	private const string PREFIX_PREFAB = "Prefabs/";
	public RoleData(RoleType roleType, string rolePath,
		string arrowPath, string explostionPath, Transform spawnTransform)
    {
		this.RoleType = roleType;
		this.RolePrefab = Resources.Load<GameObject>(PREFIX_PREFAB+ rolePath);
		this.ArrowPrefab = Resources.Load<GameObject>(PREFIX_PREFAB+ arrowPath);
		this.ExplostionEffect = Resources.Load<GameObject>(PREFIX_PREFAB + explostionPath);
		this.ArrowPrefab.GetComponent<Arrow>().explosionEffect = ExplostionEffect;
		this.SpawnPostion = spawnTransform.position;

	}
}
