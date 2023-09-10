﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using slack_api_1.Data;
using slack_api_1.Models;


namespace slack_api_1.Controllers
{
    [ApiController]
    [Route("api")]
    public class slackApiController : ControllerBase
    {
        private readonly SlackContext _context;

        public slackApiController(SlackContext context)
        {
            _context = context;
          //  SeedData(); 
        }

        [HttpGet]
        public async Task<ActionResult<slackModelResponse>> GetSlackData(string slack_name, string track)
        {
            var slackData = await _context.slackData
                .Where(s => s.slack_name == slack_name && s.track == track)
                .Select(s => new slackModelResponse
                {
                    slack_name = s.slack_name,
                    current_day = s.current_day,
                    utc_time = s.utc_time,
                    track = s.track,
                    github_file_url = s.github_file_url,
                    github_repo_url = s.github_repo_url,
                    status_code = s.status_code
                })
                .FirstOrDefaultAsync();

            if (slackData == null)
            {
                return NotFound();
            }

            return slackData;
        }

     /*   private void SeedData()
        {
            var slack_name = "Whitesama";
            var track = "backend";
            DateTime currentDate = DateTime.Now;
            DayOfWeek currentDayOfWeek = currentDate.DayOfWeek;
            string dayOfWeekString = currentDayOfWeek.ToString();
            DateTime utcDateTime = DateTime.UtcNow;
            string formattedUtcDateTime = utcDateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");

            if (!_context.slackData.Any() )
            {
                _ = _context.slackData.Add(new slackModel
                {
                    slack_name = slack_name,
                    current_day = dayOfWeekString,
                    utc_time = formattedUtcDateTime,
                    track = track,
                    github_file_url = "https://github.com/whiteSama001/zuri-slack-api/blob/main/Program.cs",
                    github_repo_url = "https://github.com/whiteSama001/zuri-slack-api",
                    status_code = 200,
                });
                _context.SaveChanges();
            }
        }*/
    }
}
