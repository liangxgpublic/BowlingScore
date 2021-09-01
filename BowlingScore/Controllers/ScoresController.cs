using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BowlingScore.Models;


namespace BowlingScore.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ScoresController : ControllerBase
    {       

        public ScoresController()
        {
            
        }

        protected const int STRIKE = 10;
        protected const int STRIKE_EXTENSION = 2;
        protected const int CompletedGameNum = 10;
        
        [HttpPost] public async Task<IActionResult> Scores([FromBody] PinDown pin)
        {
            //Pointer to the first throw of current game 
            int currentFirstThrow = 0;

            // Record How many Game Played
            int totalThrow = 0;

            // Record Total Scores
            int totalScore = 0;

            List<int> pinsDowned = pin.pinsDowned;

            // The input request validation check
            if(!inPutValidation(pinsDowned))
            {
                return BadRequest();
            }

            ScoreReponse response = new ScoreReponse();

            while (currentFirstThrow < pinsDowned.Count() && totalThrow < 10)
            {
                totalThrow++;

                // The strike game
                if (pinsDowned[currentFirstThrow] == STRIKE)
                {
                    if ((currentFirstThrow + STRIKE_EXTENSION) < pinsDowned.Count())
                    {
                        totalScore += STRIKE + pinsDowned[currentFirstThrow + 1] + pinsDowned[currentFirstThrow + 2];
                        response.FrameProgressScores.Add(totalScore.ToString());
                    }
                    else
                    {
                        response.FrameProgressScores.Add("*");
                    }

                    currentFirstThrow++;
                }
                // The last game does not finish , only have first throw
                else if (currentFirstThrow == pinsDowned.Count() - 1)
                {
                    totalScore += pinsDowned[currentFirstThrow];
                    response.FrameProgressScores.Add(totalScore.ToString());
                    currentFirstThrow++;
                    totalThrow--;
                }
                // The spare game
                else if ((pinsDowned[currentFirstThrow] + pinsDowned[currentFirstThrow + 1]) == STRIKE)
                {
                    if ((currentFirstThrow + STRIKE_EXTENSION) < pinsDowned.Count())
                    {
                        totalScore += STRIKE + pinsDowned[currentFirstThrow + 2];
                        response.FrameProgressScores.Add(totalScore.ToString());
                    }
                    else
                    {
                        response.FrameProgressScores.Add(@"/");
                        break;
                    }

                    currentFirstThrow += 2;
                }
                // Normal Game
                else if ((pinsDowned[currentFirstThrow] + pinsDowned[currentFirstThrow + 1]) < STRIKE)
                {
                    if ((currentFirstThrow + 1) < pinsDowned.Count())
                    {
                        totalScore += pinsDowned[currentFirstThrow] + pinsDowned[currentFirstThrow + 1];

                        response.FrameProgressScores.Add(totalScore.ToString());
                    }
                    else
                    {                       
                        break;
                    }

                    currentFirstThrow += 2;
                }
                // Exception happen 
                else
                {
                    return BadRequest();
                }
            }

            // Check if Game completed or no
            int responseLength = response.FrameProgressScores.Count();
            if (totalThrow == CompletedGameNum &&
                responseLength > 0 &&
                response.FrameProgressScores[responseLength -1] != "*" &&
                response.FrameProgressScores[responseLength - 1] != @"/"
                )
            {
                response.GameCompleted = true;
            }

            return Ok(response);

        }

        // This function will check the validation of the Request
        protected bool inPutValidation(List<int> requestList)
        {
            if (requestList.Count() > 21 || requestList.Count() == 0)
                return false;

            if (requestList.Any(x => x < 0) || requestList.Any(x => x > 10))
                return false;
            

            return true;
        }


       

    }
}
