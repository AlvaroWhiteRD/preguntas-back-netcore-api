
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
    public class AnswerQuestionnairesController : ControllerBase
    {
        private readonly IAnswerQuestionnaireServices _answerQuestionnaireServices;
        private readonly IQuestionnaireService _iQuestionnaireService;

        public AnswerQuestionnairesController(IAnswerQuestionnaireServices answerQuestionnaireServices, IQuestionnaireService iQuestionnaireService)
        {
            _answerQuestionnaireServices = answerQuestionnaireServices;
            _iQuestionnaireService = iQuestionnaireService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AnswerQuestionnaires answerQuestionnaires)
        {
            try
            {
                await _answerQuestionnaireServices.SavesAnswerQuestionnaire(answerQuestionnaires);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpGet("{questionnairesId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(int questionnairesId)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int userID = JwtConfirator.GetTokenIdUser(identity);

                var answerQuestionnairesList = await _answerQuestionnaireServices.AnswerQuestionnairesList(questionnairesId, userID);

                if (answerQuestionnairesList == null)
                {
                    return BadRequest(new { message = "Error al buscar el listado de respuestas" });
                }
                return Ok(answerQuestionnairesList);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id){
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int userID = JwtConfirator.GetTokenIdUser(identity);

                var answerQuestionnaire = await _answerQuestionnaireServices.SearchAnswerQuestionnaire(id, userID);
                if (answerQuestionnaire == null)
                {
                    return BadRequest(new { message = "Erro al buscar el cuestionario"});
                }
                await _answerQuestionnaireServices.DeleteAnswerQuestionnaire(answerQuestionnaire);

                return BadRequest(new { message = "La respuesta al cuestionario fue eliminada con exito."});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
          
        [Route("GetQuestionnaireByAnswerID/{answerid}")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetQuestionnaireByAnswerID(int answerid)
        {
            try
            {
                //obtenemos idcuestionario dado un idrespuesta
                int questionnaireId = await _answerQuestionnaireServices.GetQuestionnaireIdByAnswerId(answerid);

                //buscamos el cuestionario
                var questionnaire = await _iQuestionnaireService.GetQuestionnairesByID(questionnaireId);

                //buscamos las respuestas seleccionadas dado un idrespuesta
                var answerList = await _answerQuestionnaireServices.GetAnswerList(answerid);
                return Ok(new { questionnaire = questionnaire, answer = answerList }); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

