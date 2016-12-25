using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour {
	
	public TextAsset textAsset;
	public GameObject playerGO;
	public GameObject groundGO;
	public GameObject flagGO;
	public GameObject spikeGO;

	private int maxX = 0;
	private int maxY = 0;
	private int leftOffAtIndex = 0;

	private static int level = 0;
	public static int getLevel() {
		return level;
	}

	// Use this for initialization
	void Start () {
		string levelData = textAsset.text.ToString().Replace("\r\n", "\n");

		GetInitialCoordinates (levelData);

		Debug.Log ("NextLevelStartPosition is @: " + leftOffAtIndex);

		char[,] currentLevel = new char[maxX+1, maxY+1];

		int x = 0;
		int y = 0;

		int count = 0;
		char current = levelData[count];
		while (current != 'X') {
			currentLevel [x, y] = current;
			if (current == '\n') {
				y++;
				x = 0;
			} else if (current != '\n') {
				x++;
			}
			count++;
			current = levelData[count];
		}

		GameObject groundHolder = new GameObject ();
		groundHolder.name = "GroundHolder";
		for (var vert = 0; vert < maxY; vert++) {
			for (var horiz = 0; horiz < maxX; horiz++) {
				switch (currentLevel [horiz, vert]) {
				case '#':
					GameObject ground = Instantiate (groundGO, new Vector3 (horiz, -vert, 0), Quaternion.identity);
					ground.transform.parent = groundHolder.transform;
					break;
				case '@':
					Instantiate (playerGO, new Vector3 (horiz, -vert, 0), Quaternion.identity);
					break;
				case '$':
					Instantiate (flagGO, new Vector3 (horiz, -vert, 0), Quaternion.identity);
					break;
				case '!':
					Instantiate (spikeGO, new Vector3 (horiz, -vert, 0), Quaternion.identity);
					break;
				default:
					break;
				}
			}
		}

		level++;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetLevelsDimensions(int x, int y) {

	}

	void GetInitialCoordinates(string levelData) {
		int firstPassX = 0;

		int firstPassCount = 0;
		char firstPassCurrent = levelData[firstPassCount];
		while (firstPassCurrent != 'X') {
			if (firstPassCurrent == '\n') {
				maxY++;
				maxX = firstPassX;
				firstPassX = 0;
			} else if (firstPassCurrent != '\n') {
				firstPassX++;
			}
			firstPassCount++;
			firstPassCurrent = levelData[firstPassCount];
		}

		leftOffAtIndex = firstPassCount;
	}
}
