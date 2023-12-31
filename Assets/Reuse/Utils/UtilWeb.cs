using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Reuse.Utils
{
    public static class UtilWeb
    {
        /// <summary>
        /// Good to download a file to the User
        /// </summary>
        /// <param name="savePath">Use com essa formatação :  @"\A\B\C\.</param>
        
        #if UNITY_EDITOR
        public static void SaveDownloadURL(string downloadURL, string savePath, string fileName)
        {
            var localFile = Application.persistentDataPath + "/" + savePath + "/" + fileName;
            var wClient = UnityWebRequest.Get(downloadURL);

            wClient.SendWebRequest();

            while (true)
            {
                if (wClient.isDone)
                {
                    break;
                }
            }

            var arrayLength = (int)wClient.downloadedBytes;
            var content = wClient.downloadHandler.data;

            if (wClient.result == UnityWebRequest.Result.ConnectionError ||
                wClient.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Download ERROR - Localization.csv");
                return;
            }
            var fileContent = Encoding.UTF8.GetString(content);

            File.WriteAllText(localFile, fileContent);

            AssetDatabase.Refresh();
        }
        #endif
    }
}
