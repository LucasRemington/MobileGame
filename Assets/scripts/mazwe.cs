using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazwe : MonoBehaviour {

	[System.Serializable]
	public class Cell {
		public bool visited;
		public GameObject north;//1
		public GameObject east;//2
		public GameObject west;//3
		public GameObject south;//4
	}

	public GameObject wall;
	public float wallLength = 1.0f;
	public int xSize = 5;
	public int ySize = 5;
	private Vector3 initPosition;
	private GameObject wallParent;
	private Cell[] cells;
	private int currentCell = 0;
	private int totalCells;
	private int visitedCells;
	private bool startBuild = false;
	private int currentNeighbor;
	private List<int> lastCells;
	private int backUp = 0;
	private int wallToBreak;

	// Use this for initialization
	void Start () {
		MakeWalls ();
	}
	
	// Update is called once per frame
	void Update () {
		wallParent.transform.position = new Vector3 (-30f, 0.0f, -14.7f);
	}

	void MakeWalls () {
		wallParent = new GameObject ();
		wallParent.name = "WallHolder";
		initPosition = new Vector3 ((-xSize/2)+wallLength/2, 0.0f, (-ySize/2)+wallLength/2);
		Vector3 wallPos = initPosition;
		GameObject tempWall;
		//x
		for (int i = 0; i < ySize; i++) {
			for (int j = 0; j <= xSize; j++) {
				wallPos = new Vector3(initPosition.x + (j*wallLength)-wallLength/2, 0.0f, initPosition.z + (i*wallLength)-wallLength/2);
				tempWall = Instantiate(wall,wallPos,Quaternion.identity);
				tempWall.transform.parent = wallParent.transform;
			}
		}
		//y
		for (int i = 0; i <= ySize; i++) {
			for (int j = 0; j < xSize; j++) {
				wallPos = new Vector3(initPosition.x + (j*wallLength), 0.0f, initPosition.z + (i*wallLength)-wallLength);
				tempWall = Instantiate(wall,wallPos,Quaternion.Euler(0.0f,90.0f,0.0f)) as GameObject;
				tempWall.transform.parent = wallParent.transform;
			}
		}
		CreateCells ();
	}

	void CreateCells () {
		lastCells = new List<int> ();
		lastCells.Clear ();
		totalCells = xSize * ySize;
		GameObject[] allWalls;
		int children = wallParent.transform.childCount;
		allWalls = new GameObject[children];
		cells = new Cell[xSize * ySize];
		int eastToWest = 0;
		int childFinder = 0;
		int termCount = 0;

		for (int i = 0; i < children; i++) {
			if (children > 0) {
				allWalls[i] = wallParent.transform.GetChild(i).gameObject;
			}
		}
		for (int cellProcess = 0; cellProcess < cells.Length; cellProcess++) {
			if (termCount == xSize) {
				eastToWest++;
			}
			cells[cellProcess] = new Cell();
			cells [cellProcess].east = allWalls [eastToWest];
			cells [cellProcess].south = allWalls [childFinder + (xSize + 1) * ySize];
			if (termCount == xSize) {
				//eastToWest += 2;
				termCount = 0;
			} 

			eastToWest++;
			termCount++;
			childFinder++;
			cells [cellProcess].west = allWalls [eastToWest];
			cells [cellProcess].north = allWalls [(childFinder + (xSize + 1) * ySize)+xSize-1];
		}
		MakeMaze ();
	}

	void MakeMaze () {
		while (visitedCells < totalCells) {
			if (startBuild) {
				Neighbors ();
				if (cells [currentNeighbor].visited == false && cells [currentCell].visited == true) {
					BreakWall ();
					cells [currentNeighbor].visited = true;
					visitedCells++;
					lastCells.Add (currentCell);
					currentCell = currentNeighbor;
					if (lastCells.Count > 0) {
						backUp = lastCells.Count - 1;
					}
				}
			} else {
				currentCell = Random.Range(0, totalCells);
				cells[currentCell].visited = true;
				visitedCells++;
				startBuild = true;
			}

			//Invoke ("MakeMaze", 0.0f);
		}
	}

	void Neighbors (){
		int length = 0;
		int[] varNeighbors = new int[4];
		int[] connectingWall = new int[4];
		int check = 0;
		check = ((currentCell + 1) / xSize);
		check -= 1;
		check *= xSize;
		check += xSize;
		//west
		if (currentCell + 1 < totalCells && (currentCell + 1) != check) {
			if (cells [currentCell + 1].visited == false) {
				varNeighbors [length] = currentCell + 1;
				connectingWall [length] = 3;
				length++;
			}
		}
		//east
		if (currentCell - 1 >= 0 && currentCell != check) {
			if (cells [currentCell - 1].visited == false) {
				varNeighbors [length] = currentCell - 1;
				connectingWall [length] = 2;
				length++;
			}
		}
		//north
		if (currentCell + xSize < totalCells) {
			if (cells [currentCell+xSize].visited == false) {
				varNeighbors [length] = currentCell+xSize;
				connectingWall [length] = 1;
				length++;
			}
		}
		//south
		if (currentCell - xSize >= 0) {
			if (cells [currentCell-xSize].visited == false) {
				varNeighbors [length] = currentCell-xSize;
				connectingWall [length] = 4;
				length++;
			}
		}
		if (length != 0) {
			int randomNeighbor = Random.Range (0, length);
			currentNeighbor = varNeighbors [randomNeighbor];
			wallToBreak = connectingWall[randomNeighbor];
		} else {
			if (backUp > 0) {
				currentCell = lastCells [backUp];
				backUp--;
			}
		}
	}

	void BreakWall () {
		switch (wallToBreak) {
		case 1:
			Destroy (cells [currentCell].north); break;
		case 2:
			Destroy (cells [currentCell].east); break;
		case 3:
			Destroy (cells [currentCell].west); break;
		case 4:
			Destroy (cells [currentCell].south); break;
		}
	}

}
