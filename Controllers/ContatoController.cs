using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModuloAPI.Context;
using ModuloAPI.Entities;

// Controller -> ponto de entrada da disponibilização dos metodos
namespace ModuloAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        // Injeção de dependência -> recebe via construtor AgendaContext, context que possibilita o acesso ao DB
        private readonly AgendaContext _context; // atributo somente leitura
        
        public ContatoController (AgendaContext context) //Construtor
        {
            _context = context;
        }

        // Imprementar CRUD para tabela Contatos
        [HttpPost] // Criar Contato
        public IActionResult Criar (Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges(); // Atualiza o DB
            return CreatedAtAction(nameof(ObterPorId), new { id = contato.Id }, contato); // Retorna ao usuário a rota para obter o novo registro
        }

        [HttpPut("{id}")] // Atualizar Contato
        public IActionResult Atualizar (int id, Contato contato) // contato vem da requisição
        {
            var contatoBanco = _context.Contatos.Find(id); // .Contatos -> DbSet
            if (contatoBanco == null) return NotFound(); // Verificar se contato é válido

            // Atualiza a propriedade no DB pela requisição
            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();

            return Ok(contatoBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar (int id)
        {
            var contatoBanco = _context.Contatos.Find(id);
            if (contatoBanco == null) return NotFound();

            _context.Contatos.Remove(contatoBanco);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("{id}")] // Buscar contato por ID
        public IActionResult ObterPorId (int id)
        {
            var contato = _context.Contatos.Find(id);
            if (contato == null) return NotFound();
            return Ok(contato);
        }

        [HttpGet("ObterPorNome")] // Buscar contato por Nome
        public IActionResult ObterPorNome (string nome)
        {
            var contato = _context.Contatos.Where(x => x.Nome.Contains(nome));
            if (contato == null) return NotFound();
            return Ok(contato);
        }

        [HttpGet("ObterPorTelefone")] // Buscar contato por Telefone
        public IActionResult ObterPorTelefone (string telefone)
        {
            var contato = _context.Contatos.Where(x => x.Telefone.Contains(telefone));
            if (contato == null) return NotFound();
            return Ok(contato);
        }

        [HttpGet("ObterPorAtividade")] // Buscar contato por Atividade
        public IActionResult ObterPorAtividade (bool ativos)
        {
            var contato = _context.Contatos.Where(x => x.Ativo == ativos).ToList();
            if (contato == null) return NotFound();
            return Ok(contato);
        }
    }
}