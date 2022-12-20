using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Chain : MonoBehaviour {

	public LineRenderer line;
	public Color nodeColor;
	public GameObject nodePrefab;
	public Camera cam;
	public int numberOfNodes;
	public float length;
	public float springPower = 1;
	public float drag = 1;
	public bool hideNodes = true;
	public bool lockEnd;
	public bool lockStart;

	private List<ChainNode> nodes = new List<ChainNode>();
	private ChainNode end;
	private ChainNode start;

	void Start() {
		float nodeLength = length / numberOfNodes;
		ChainNode previousNode = null;
		for (int i = 0; i < numberOfNodes; i++) {
			GameObject nodeObject = Instantiate(nodePrefab, transform);
			ChainNode node = nodeObject.GetComponent<ChainNode>();
			if (i == 0) {
				start = node;
			}
			if (hideNodes) {
				nodeObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
			} else {
				nodeObject.GetComponent<SpriteRenderer>().color = nodeColor;
			}
			nodeObject.transform.position = transform.position + new Vector3(nodeLength * i, 0, 0);
			node.cam = cam;
			node.nextNodeDistance = nodeLength;
			node.springPower = springPower * numberOfNodes;
			node.drag = drag;
			if (previousNode != null) {
				previousNode.next = node;
			}
			previousNode = node;
			nodes.Add(node);
		}
		end = previousNode;
		end.GetComponent<SpriteRenderer>().color = nodeColor;
		start.GetComponent<SpriteRenderer>().color = nodeColor;
		line.positionCount = nodes.Count;
	}

	void Update() {
		end.locked = lockEnd;
		start.locked = lockStart;
		List<Vector3> positions = new List<Vector3>();
		for (int i = 0; i < nodes.Count; i++) {
			line.SetPosition(i, nodes[i].transform.position);
		}
	}

}
