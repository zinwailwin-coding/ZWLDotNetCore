using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ZWLDotNetCore.RestApiWithNLayer.Features.LatHtaukBayDin
{
    [Route("api/[controller]")]
    [ApiController]
    public class LatHtaukBayDinController : ControllerBase
    {
        private async Task<LatHtaukBayDin> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("data.json");
            var obj = JsonConvert.DeserializeObject<LatHtaukBayDin>(jsonStr);
            return obj;
        }

        [HttpGet("questions")]
        public async Task<IActionResult> Questions()
        {
            var obj= await GetDataAsync();
            return Ok(obj.questions);
        }

        [HttpGet("numberList")]
        public async Task<IActionResult> NumberList()
        {
            var obj = await GetDataAsync();
            return Ok(obj.numberList);
        }
        [HttpGet("{questionNo}/{no}")]
        public async Task<IActionResult> Answer(int questionNo, int no)
        {
            var obj = await GetDataAsync();
            return Ok(obj.answers.FirstOrDefault(x=>x.answerNo==no && x.questionNo==questionNo));
        }

    }


    public class LatHtaukBayDin
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

}
