using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

	private EdgeCollider2D edgeCollider;

	public void Awake() {
		CreateEdgeColliders();
	}

	public void FixedUpdate() {
	}

	private void CreateEdgeColliders() {
		var mainCam = Camera.main;
		var bottomLeft = (Vector2)mainCam.ScreenToWorldPoint(new Vector3(0, 0, mainCam.nearClipPlane)) + new Vector2(1, 0);
		var topLeft = (Vector2)mainCam.ScreenToWorldPoint(new Vector3(0, mainCam.pixelHeight, mainCam.nearClipPlane)) + new Vector2(1, 0);
		var topRight = (Vector2)mainCam.ScreenToWorldPoint(new Vector3(mainCam.pixelWidth, mainCam.pixelHeight, mainCam.nearClipPlane)) - new Vector2(1, 0);
		var bottomRight = (Vector2)mainCam.ScreenToWorldPoint(new Vector3(mainCam.pixelWidth, 0, mainCam.nearClipPlane)) - new Vector2(1, 0);

		edgeCollider = GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : GetComponent<EdgeCollider2D>();

		var edgePoints = new[] { bottomLeft, topLeft, topRight, bottomRight, bottomLeft };
		edgeCollider.points = edgePoints;
	}
}
