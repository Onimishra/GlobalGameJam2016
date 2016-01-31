using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

public class ImageDownload : MonoBehaviour {
    public string[] DownloadURLs;

    public List<string> DownloadedImages;

	// Use this for initialization
	void Start () {
        print(Application.persistentDataPath);
        DownloadedImages = new List<string>();
        StartCoroutine(BatchDownload());
	}

    IEnumerator BatchDownload() {
        foreach (var url in DownloadURLs) {
            yield return StartCoroutine(DownloadImage(url));
        }
    }

    IEnumerator DownloadImage(string downloadURL) {
        string path = Application.persistentDataPath + "/images";
        if (!System.IO.Directory.Exists(path)) {
            System.IO.Directory.CreateDirectory(path);
        }

        Regex regex = new Regex(@".*\/(.*)");
        MatchCollection matches = regex.Matches(downloadURL);
        if (matches.Count > 0) {
            string fileName = matches[0].Groups[1].Value;
            print(fileName);
            string filePath = path + "/" + fileName;

            if (!System.IO.File.Exists(filePath)) {
                WWW www = new WWW(downloadURL);
               	//File.WriteAllBytes(filePath, www.bytes);
                DownloadedImages.Add(filePath);
            } else {
                print("FILE (" + fileName + ") EXISTS");
            }
        }
        yield return null;
    }
}
