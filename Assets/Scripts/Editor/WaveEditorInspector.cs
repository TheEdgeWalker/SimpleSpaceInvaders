using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaveEditor))]
public class WaveEditorInspector : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		WaveEditor waveEditor = (WaveEditor)target;

		if (GUILayout.Button("Save Wave"))
		{
			Save(waveEditor);
		}

		if (GUILayout.Button("Load Wave"))
		{
			Load(waveEditor);
		}
	}

	private void Save(WaveEditor waveEditor)
	{
		string path = EditorUtility.SaveFilePanelInProject("Save WaveData", "Wave", "asset", "Save WaveData");

		if (string.IsNullOrWhiteSpace(path))
			return;

		WaveData waveData = CreateInstance<WaveData>();

		waveData.startingPosition = waveEditor.transform.position;
		waveData.speed = waveEditor.speed;
		waveData.fireCooldown = waveEditor.fireCooldown;

		waveData.columns = new ColumnData[waveEditor.transform.childCount];
		for (int i = 0; i < waveEditor.transform.childCount; ++i)
		{
			Transform column = waveEditor.transform.GetChild(i);
			waveData.columns[i].enemies = new EnemyData[column.childCount];
			for (int j = 0; j < column.childCount; ++j)
			{
				Transform enemy = column.transform.GetChild(j);
				waveData.columns[i].enemies[j].prefab = PrefabUtility.GetCorrespondingObjectFromSource(enemy.gameObject);
				waveData.columns[i].enemies[j].position = enemy.position - waveEditor.transform.position;
			}
		}

		AssetDatabase.CreateAsset(waveData, path);
		AssetDatabase.SaveAssets();
	}

	private void Load(WaveEditor waveEditor)
	{
		string path = EditorUtility.OpenFilePanelWithFilters("Load WaveData", "Assets/Prefabs/Levels", new string[] { "WaveData", "asset" });

		if (string.IsNullOrWhiteSpace(path))
			return;

		for (int i = waveEditor.transform.childCount - 1; i >= 0; --i)
		{
			DestroyImmediate(waveEditor.transform.GetChild(i).gameObject);
		}

		if (path.StartsWith(Application.dataPath))
		{
			path = "Assets" + path.Substring(Application.dataPath.Length);
		}

		WaveData waveData = AssetDatabase.LoadAssetAtPath<WaveData>(path);

		waveEditor.speed = waveData.speed;
		waveEditor.fireCooldown = waveData.fireCooldown;
		waveEditor.transform.position = waveData.startingPosition;

		foreach (ColumnData columnData in waveData.columns)
		{
			GameObject newColumn = new GameObject("Column");
			newColumn.transform.parent = waveEditor.transform;
			newColumn.transform.localPosition = Vector3.zero;

			foreach (EnemyData enemyData in columnData.enemies)
			{
				GameObject newEnemy = (GameObject)PrefabUtility.InstantiatePrefab(enemyData.prefab, newColumn.transform);
				newEnemy.transform.localPosition = enemyData.position;
				newEnemy.SetActive(true);
			}
		}
	}
}
