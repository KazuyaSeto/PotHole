using UnityEngine;
using System.Collections;
using System;

public class PerlinMesh : MonoBehaviour {
	[SerializeField] MeshFilter meshFilter = null;
	[SerializeField] MeshCollider meshCollider = null;
	// Use this for initialization
	void Start () {
		meshFilter.mesh = createMesh (meshFilter.mesh);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3[] vertices = meshFilter.mesh.vertices;
		for(int index = 0, size = meshFilter.mesh.vertices.Length; index < size; ++index)
		{
			vertices[index].y = UnityEngine.Mathf.PerlinNoise(index,0.1f*Time.realtimeSinceStartup);
		}
		meshFilter.mesh.vertices = vertices;

		meshCollider.sharedMesh = meshFilter.mesh;
	}

	Mesh createMesh( Mesh mesh ) {
		Mesh _mesh = new Mesh ();
		_mesh.name = "myMesh";
		// vert
		Debug.Log (mesh.vertices.Length);
		Vector3[] vertices = new Vector3[mesh.vertices.Length];
		Array.Copy (mesh.vertices, vertices, mesh.vertices.Length);
		_mesh.vertices = vertices;

		// color
		Debug.Log (mesh.uv.Length);
		Vector2[] uv       = new Vector2[mesh.uv.Length];
		Array.Copy (mesh.uv, uv, mesh.uv.Length);
		_mesh.uv = uv;

		int[] triangles     = new int[mesh.triangles.Length];
		Debug.Log (mesh.triangles.Length);
		Array.Copy (mesh.triangles, triangles, mesh.triangles.Length);
		_mesh.triangles = triangles;

		return _mesh;
	}
}
