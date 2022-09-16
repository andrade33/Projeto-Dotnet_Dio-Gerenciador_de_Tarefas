/********************************************************************/
using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
/********************************************************************/

/********************************************************************/
namespace TrilhaApiDesafio.Controllers
/********************************************************************/
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }
        /********************************************************************/


        /********************************************************************/
        [HttpPost("CriarTarefa")]
        public IActionResult Criar(Tarefa tarefa)
        {
            try
            {
	            if (tarefa.Data == DateTime.MinValue){
	                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
	            }
	            _context.Add(tarefa);
	            _context.SaveChanges();
	            return Ok(tarefa);
	            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
	            return CreatedAtAction(nameof(ObterPorId), new {id = tarefa.Id}, tarefa);
            }
            catch (System.Exception)
            {
	            throw;
            }
        }
        /**************************************************************************/

        
        /**************************************************************************/
        [HttpDelete("{id}")] // o parametro pode ser omitido
        public IActionResult Deletar(int id)
        {
            try
            {
	            var tarefasDoBanco = _context.Tarefas.Find(id);
	
	            if (tarefasDoBanco == null){
	                return NotFound();
	            }
	           
	            //var tarefa = _context.Tarefas.Where(Tarefas.Id)
	            _context.Tarefas.Remove(tarefasDoBanco);
	            _context.SaveChanges();
	            //return NoContent();       
	            return CreatedAtAction(nameof(Deletar), new { id = tarefasDoBanco.Id }, tarefasDoBanco);
            }
            catch (System.Exception)
            {
	            throw;
            }
        }
        /**********************************************************************/


        /**************************************************************************/
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {

            var tarefaDoBanco = _context.Tarefas.Find(id);
            if(tarefaDoBanco == null)
                return NotFound();

            return Ok(tarefaDoBanco);
        }
        /**************************************************************************/


        /**************************************************************************/
        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var tarefas = _context.Tarefas.Select(x => x).ToList();
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            if (tarefas == null)
	                return NotFound();
            return Ok(tarefas);
            
        }
        /**************************************************************************/

        /**************************************************************************/
        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
           try
            {
	            var tarefaDoBanco = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));
	            if (tarefaDoBanco == null)
	                return NotFound();
                return Ok(tarefaDoBanco);
            }
            catch (System.Exception)
            {
	            throw;
            }
        }
        /**************************************************************************/

        /**************************************************************************/
        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            try
            {
	            var tarefaDoBanco = _context.Tarefas.Where(x => x.Data.Date == data.Date);
                if (tarefaDoBanco == null)
	                return NotFound();

	            return Ok(tarefaDoBanco);
            }
            catch (System.Exception)
            {
	            throw;
            }
        }
        /**************************************************************************/

        /**************************************************************************/
        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            try
            {
	            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
	            // Dica: Usar como exemplo o endpoint ObterPorData
	            var tarefaDoBanco = _context.Tarefas.Where(x => x.Status == status);
	            
	            if (tarefaDoBanco == null)
	                return NotFound();
	            
	            return Ok(tarefaDoBanco);
            }
            catch (System.Exception)
            {
	            throw;
            }
        }
        /**************************************************************************/


        /**************************************************************************/
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            try
	        {
	            var tarefaDoBanco = _context.Tarefas.Find(id);
	
	            /*********************************/
	            if (tarefaDoBanco == null)
	                return NotFound();
	            
	            /*********************************/
	            tarefaDoBanco.Titulo = tarefa.Titulo; 
	            tarefaDoBanco.Descricao = tarefa.Descricao; 
	            tarefaDoBanco.Data = tarefa.Data; 
	            tarefaDoBanco.Status = tarefa.Status; 
	            /*********************************/
	            _context.Tarefas.Update(tarefaDoBanco);
	            _context.SaveChanges();
	            return Ok(tarefaDoBanco);
            }
            catch (System.Exception)
            {
	            throw;
            }
        }
        /**************************************************************************/
    }
}

