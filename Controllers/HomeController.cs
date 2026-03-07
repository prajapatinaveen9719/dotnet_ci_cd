using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MonitoringStack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetHello()
        {
            return Ok("Hello World from ci cd pipeline devops !.\r\n");


        }


        [HttpGet("exception")]
        public IActionResult GetException()
        {
            throw new InvalidOperationException("Something went wrong !");
        }


        [HttpGet("slow")]
        public async Task<IActionResult> Slow()
        {
            try
            {
                var timeTaken = await DoSomeHeavyTask();

                return Ok(new
                {
                    status = "Success",
                    message = $"Heavy task completed in {timeTaken}ms"
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    status = "Error",
                    error = "Internal Server Error"
                });
            }
        }

        private async Task<int> DoSomeHeavyTask()
        {
            var random = new Random();

            int[] delays = { 100, 150, 200, 300, 600, 500, 1000, 1400 };
            int delay = delays[random.Next(delays.Length)];

            int[] errorChance = { 1, 2, 3, 4, 5, 6, 7, 8 };

            if (errorChance[random.Next(errorChance.Length)] == 1)
            {
                string[] errors =
                {
                "DB Payment Failure",
                "DB Server is Down",
                "Access Denied",
                "Not Found Error"
            };

                throw new Exception(errors[random.Next(errors.Length)]);
            }

            await Task.Delay(delay);

            return delay;
        }


    }
}
