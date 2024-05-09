// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");
string jsonStr = await File.ReadAllTextAsync("data.json");
var obj= JsonConvert.DeserializeObject<MainDto>(jsonStr);
foreach (var question in obj.questions)
{
    Console.WriteLine(question.questionNo);
}
Console.ReadLine();

static void myanmarToEng(string num)
{
    num.Replace("၃", "3");
    num.Replace("၄", "4");
    num.Replace("၅", "5");
    num.Replace("၆", "6");
}
public class MainDto
{
    public Question[] questions { get; set; }
    public Answer[] answers { get; set; }
    public string[] numberList { get; set; }
}

public class Question
{
    public int questionNo { get; set; }
    public string questionName { get; set; }
}

public class Answer
{
    public int questionNo { get; set; }
    public int answerNo { get; set; }
    public string answerResult { get; set; }
}
