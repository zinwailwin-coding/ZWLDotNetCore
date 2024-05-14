// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");
string jsonStr = await File.ReadAllTextAsync("data.json");
var obj = JsonConvert.DeserializeObject<MainDto>(jsonStr);
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



public class Rootobject
{
    public Tbl_Gallery[] Tbl_Gallery { get; set; }
    public Tbl_Art[] Tbl_Art { get; set; }
    public Tbl_Artist[] Tbl_Artist { get; set; }
}

public class Tbl_Gallery
{
    public int GalleryId { get; set; }
    public int ArtistId { get; set; }
    public int ArtId { get; set; }
}

public class Tbl_Art
{
    public int ArtId { get; set; }
    public string ArtName { get; set; }
    public string ArtDescription { get; set; }
}

public class Tbl_Artist
{
    public int ArtistId { get; set; }
    public string ArtistName { get; set; }
    public Social[] Social { get; set; }
}

public class Social
{
    public string Name { get; set; }
    public string Link { get; set; }
}

