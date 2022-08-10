using System.Collections;
using UnityEngine;
using TMPro;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField pincode;
    [SerializeField] private GameObject error;

    private string DBConnectString;

    public static string nickname = "";

    public bool Logged { get; set; }

    private void Awake()
    {
        DBConnectString = "URI=file:inventoryData.db";
        Logged = false;
    }
    public void Logger()
    {
        using (var connection = new SqliteConnection(DBConnectString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"select * from Logger where Instance = '{pincode.text}'";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nickname = reader["Savename"].ToString();
                        Logged = true;
                    }
                    reader.Close();
                }
                connection.Close();
            }
        }

        if (!Logged)
        {
            error.gameObject.SetActive(true);
            return;
        }
        print("Login Complete");
        StartCoroutine(AwaitedNul());
    }
    private IEnumerator AwaitedNul()
    {
        yield return new WaitForSeconds(2);
        error.gameObject.SetActive(false);
        SceneManager.LoadScene("SampleScene");
    }
}
