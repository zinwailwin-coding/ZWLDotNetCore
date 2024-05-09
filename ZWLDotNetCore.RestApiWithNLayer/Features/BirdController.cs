using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ZWLDotNetCore.RestApiWithNLayer.Features
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdController : ControllerBase
    {
        private async Task<BirdList> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("Birds.json");
            var birdList = JsonConvert.DeserializeObject<BirdList>(jsonStr);
            return birdList;
        }

        [HttpGet("BirdList")]
        public async Task<IActionResult> BirdList()
        {
            var birdList = await GetDataAsync();
            return Ok(birdList.Tbl_Bird);
        }

        [HttpGet("{birdId}")]
        public async Task<IActionResult> getBirdById(int birdId)
        {
            var birdList = await GetDataAsync();
            var bird = birdList.Tbl_Bird.FirstOrDefault(x=>x.Id== birdId);
            return Ok(bird);           
        }
    }

    public class BirdList
    {
        public Tbl_Bird[] Tbl_Bird { get; set; }
    }

    public class Tbl_Bird
    {
        public int Id { get; set; }
        public string BirdMyanmarName { get; set; }
        public string BirdEnglishName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }

}
