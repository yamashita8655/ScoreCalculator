using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class CsvManager : Singleton<CsvManager> {

	public void Initialize() {
		Debug.Log("CsvManager init");
	}

	public bool SaveCsv(string fileName, string src) {
		bool res = true;
		try {
			string path = Application.persistentDataPath + "/" + fileName;
			StreamWriter sw = new StreamWriter(
				path,
				false,
				Encoding.UTF8);

			//TextBox1.Textの内容を書き込む
			sw.Write(src);
			//閉じる
			sw.Close();
		} catch (IOException e) {
			Debug.Log("Save Error");
			res = false;
		}

		return res;
	}

	public string LoadCsv(string fileNameAndPath) {
		string input = "";
		try {
			//string path = Application.persistentDataPath + "/" + fileName;
			StreamReader sr = new StreamReader(
				fileNameAndPath,
				Encoding.UTF8);
			input = sr.ReadToEnd();
			Debug.Log(input);
			sr.Close();

		} catch (IOException e) {
			Debug.Log("Load Error");
		}

		return input;
	}
}
