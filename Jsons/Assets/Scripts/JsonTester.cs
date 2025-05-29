using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;

public class JsonTester : MonoBehaviour
{
    string path = Path.Combine(Application.streamingAssetsPath, "playerData.json");
    // Start is called before the first frame update
    void Start()
    {
        //Show1();
        Show3();
    }

    void Show1()
    {
        string json = File.ReadAllText(path);
        Debug.Log(json);

        JToken token = JToken.Parse(json);
        JToken players = token["players"];  

        for(int i = 0; i < players.Count(); i++)
        {
            Debug.Log($"{players[i]["id"]} 플레이어 번호 {players[i]["name"]}는 플레이어의 이름");

            JToken skills = players[i]["skills"];

            for(int j = 0; j < skills.Count(); j++)
            {
                Debug.Log($"{name}의 {j + 1}번째 기술은 {skills[j]["name"]}입니다.");

            }
        }
    }

    string url = "https://jsonplaceholder.typicode.com/users/1";
    void Show2()
    {
        StartCoroutine(GetDataForWeb());
    }

    IEnumerator GetDataForWeb()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if(request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log($"망한 인터넷");
        }
        else
        {

            string jsonData = request.downloadHandler.text;
            Debug.Log(jsonData);
        }

    }
    void Show3()
    {
        StartCoroutine(SendDataFormWeb());
    }

    IEnumerator SendDataFormWeb()
    {
        var Tutor = new
        {
            name = "고래티백",
            score = 99999999,
            description = "재밌으니까 한 번 더 보낼래요 렛츠고"

        };
        string jsonData = JsonConvert.SerializeObject(Tutor, Formatting.Indented);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonData);

        // 요청 설정
        string url = "https://sindatadragon.synology.me/TeamSparta/post.php";
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonBytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("실패");

        }
        else
        {
            Debug.Log(request.downloadHandler.text);

        }

    }
}
