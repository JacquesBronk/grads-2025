using System.Text;
using System.Text.Json;

const string apiPath = "https://retroscore.azurewebsites.net/updateScore";
const string myName = "Marie";

string testPath = Path.Combine("tests", "E2ERetroShop", "TestResults.xml");

if(File.Exists(testPath))
{
    string testResults = File.ReadAllText(testPath);
    
    //Post to apiPath with Body {UserName = myName, TestScoreXml = testResults}
    var client = new HttpClient();
    var content = new StringContent(JsonSerializer.Serialize(new { UserName = myName, TestScoreXml = testResults }), Encoding.UTF8, "application/json");
    var response = await client.PostAsync(apiPath, content);
    
    
}
else
{
    Console.WriteLine("File does not exist, please open your terminal, and run TestMe.bat");
}