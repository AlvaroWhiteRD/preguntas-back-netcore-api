using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionnaireController : ControllerBase
    {
        private readonly IQuestionnaireService _questionnaireService;
        public QuestionnaireController(IQuestionnaireService questionnaireService)
        {
            _questionnaireService = questionnaireService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Questionnaires questionnaire)
        {
            try
            {//Identity
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int userID = JwtConfirator.GetTokenIdUser(identity);

                questionnaire.UserId = userID;
                questionnaire.Active = 1;
                questionnaire.CreationDate = DateTime.Now;
                await _questionnaireService.CreateQuestionnaire(questionnaire);

                return Ok(new { message = "Se agrego el questionario exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetQuestionnaireByUser")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetQuestionnaireByUser()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int userID = JwtConfirator.GetTokenIdUser(identity);

                var questionnaireList = await _questionnaireService.GetListQuestionnaireByUser(userID);
                return Ok(questionnaireList);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{questionnaireID}")]
        public async Task<IActionResult> Get(int questionnaireID)
        {
            try
            {
                var questionnaire = await _questionnaireService.GetQuestionnairesByID(questionnaireID);
                return Ok(questionnaire);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{questionnaireID}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int questionnaireID)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int userID = JwtConfirator.GetTokenIdUser(identity);

                var questionnaire = await _questionnaireService.QuestionnaireSearch(questionnaireID, userID);

                if (questionnaire == null)
                {
                    return BadRequest(new { message = " No se encontro ningun cuestionario"});
                }
                await _questionnaireService.QuestionnaireDelete(questionnaire);

                return Ok(new { message = "El cuentionario fue eliminado con exico"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
        [Route("GetListQuestionnaires")]
        [HttpGet]
        public async Task<IActionResult> GetListQuestionnaires()
        {
            try
            {
                var listQuestionnaire = await _questionnaireService.GetListQuestionnaires();
                return Ok(listQuestionnaire);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
