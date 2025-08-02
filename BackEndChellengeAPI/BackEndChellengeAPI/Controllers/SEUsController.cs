using Microsoft.AspNetCore.Mvc;

namespace BackEndChellengeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SEUsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private List<PalestraDto> palestras = new List<PalestraDto>
            {
                new PalestraDto {
                    Id = 1,
                    Imagem = "https://i.imgur.com/arsE55j.png",
                    Titulo = "A Jornada da Inteligência Artificial",
                    Tipo = "Apresentação Oral",
                    Data = "Segunda-feira, 12 de Agosto",
                    Horario = "09:00 AM",
                    Localizacao = "Sala 1 - Bloco A - Campus Crateús"
                },
                new PalestraDto {
                    Id = 2,
                    Imagem = "https://i.imgur.com/QCGxFIV.png",
                    Titulo = "Descomplicando o Front-End Moderno",
                    Tipo = "Painel",
                    Data = "Terça-feira, 13 de Agosto",
                    Horario = "10:30 AM",
                    Localizacao = "Sala 2 - Bloco B - Campus Crateús"
                },
                new PalestraDto {
                    Id = 3,
                    Imagem = "https://i.imgur.com/9YS8A3O.png",
                    Titulo = "Machine Learning na Prática",
                    Tipo = "Workshop",
                    Data = "Quarta-feira, 14 de Agosto",
                    Horario = "02:00 PM",
                    Localizacao = "Sala 3 - Bloco C - Campus Crateús"
                },
                new PalestraDto {
                    Id = 4,
                    Imagem = "https://i.imgur.com/9YS8A3O.png",
                    Titulo = "Boas práticas de API REST",
                    Tipo = "Mesa Redonda",
                    Data = "Quinta-feira, 15 de Agosto",
                    Horario = "04:00 PM",
                    Localizacao = "Sala 4 - Bloco D - Campus Crateús"
                },
                new PalestraDto {
                    Id = 5,
                    Imagem = "https://i.imgur.com/euXOFh9.jpeg",
                    Titulo = "Cloud Computing e o Futuro da TI",
                    Tipo = "Apresentação Oral",
                    Data = "Sexta-feira, 16 de Agosto",
                    Horario = "11:00 AM",
                    Localizacao = "Sala 5 - Bloco E - Campus Crateús"
                },
                new PalestraDto {
                    Id = 6,
                    Imagem = "https://i.imgur.com/chb5ful.png",
                    Titulo = "Automação de Testes com Selenium",
                    Tipo = "Workshop",
                    Data = "Segunda-feira, 19 de Agosto",
                    Horario = "01:00 PM",
                    Localizacao = "Sala 6 - Bloco F - Campus Crateús"
                },
                new PalestraDto {
                    Id = 7,
                    Imagem = "https://i.imgur.com/y77gVLQ.png",
                    Titulo = "Clean Architecture no Mundo Real",
                    Tipo = "Apresentação Oral",
                    Data = "Terça-feira, 20 de Agosto",
                    Horario = "03:00 PM",
                    Localizacao = "Sala 7 - Bloco G - Campus Crateús"
                },
                new PalestraDto {
                    Id = 8,
                    Imagem = "https://i.imgur.com/chb5ful.png",
                    Titulo = "DevOps: Da Teoria à Prática",
                    Tipo = "Painel",
                    Data = "Quarta-feira, 21 de Agosto",
                    Horario = "05:30 PM",
                    Localizacao = "Sala 8 - Bloco H - Campus Crateús"
                },
                new PalestraDto {
                    Id = 9,
                    Imagem = "https://i.imgur.com/arsE55j.png",
                    Titulo = "Introdução ao Desenvolvimento Mobile",
                    Tipo = "Workshop",
                    Data = "Quinta-feira, 22 de Agosto",
                    Horario = "08:30 AM",
                    Localizacao = "Sala 9 - Bloco I - Campus Crateús"
                },
                new PalestraDto {
                    Id = 10,
                    Imagem = "https://i.imgur.com/euXOFh9.png",
                    Titulo = "Segurança da Informação em Ambientes Corporativos",
                    Tipo = "Apresentação Oral",
                    Data = "Sexta-feira, 23 de Agosto",
                    Horario = "10:00 AM",
                    Localizacao = "Sala 10 - Bloco J - Campus Crateús"
                }
            };
        public List<ProfessorDto> professores = new List<ProfessorDto>
        {
            new ProfessorDto { NomeProfessor = "Dr. João Silva", NomeInstituicao = "UFC Crateús" },
            new ProfessorDto { NomeProfessor = "Profa. Ana Souza", NomeInstituicao = "IFCE" }
        };
        List<OrganizadorDto> organizadores = new List<OrganizadorDto>
        {
            new OrganizadorDto { Nome = "Carlos Mendes" },
            new OrganizadorDto { Nome = "Juliana Ferreira" }
        };
        List<ApresentacaoDto> apresentacoes = new List<ApresentacaoDto>
        {
            new ApresentacaoDto
            {
                Titulo = "Aplicações de Inteligência Artificial na Educação",
                Autor = "Maria Clara Souza",
                Orientador = "Dr. Ricardo Almeida"
            },
            new ApresentacaoDto
            {
                Titulo = "Sustentabilidade e Energia Renovável no Ceará",
                Autor = "João Pedro Lima",
                Orientador = "Profa. Carla Mendes"
            },
            new ApresentacaoDto
            {
                Titulo = "O Impacto das Fake News nas Eleições",
                Autor = "Ana Beatriz Rocha",
                Orientador = "Prof. Marcelo Cunha"
            },
            new ApresentacaoDto
            {
                Titulo = "Desenvolvimento de Jogos Educativos com Unity",
                Autor = "Lucas Ferreira",
                Orientador = "Dr. André Vasconcelos"
            },
            new ApresentacaoDto
            {
                Titulo = "Análise de Algoritmos Genéticos para Otimização",
                Autor = "Gabriel Martins",
                Orientador = "Profa. Helena Ribeiro"
            }
        };
        public SEUsController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("Login")]
        public IActionResult Login([FromQuery] string cpf, [FromQuery] string senha)
        {
            string cpfValido = "48072729004";
            string senhaValida = "123456";

            try
            {
                if (cpf != cpfValido || senha != senhaValida)
                    return Unauthorized("Login invalido.");

                return Ok("Login bem sucedido!");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("ObterPalestras")]
        public ActionResult<IEnumerable<PalestraDto>> ObterPalestras()
        { 
            return Ok(palestras);
        }

        [HttpGet("ObterDetalhePalestra")]
        public ActionResult<DetalhePalestraDto> ObterDetalhePalestra([FromQuery] long palestraId)
        {
            var palestra = palestras.FirstOrDefault(p => p.Id == palestraId);

            if (palestra == null)
                return NotFound();

            var detalhe = new DetalhePalestraDto
            {
                Palestra = palestra,
                Avaliadores = professores,
                Organizadores = organizadores,
                Apresentacoes = apresentacoes
            };

            return Ok(detalhe);
        }
    }

    public class PalestraDto
    {
        public int Id { get; set; }
        public string Imagem { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string Data { get; set; }
        public string Horario { get; set; }
        public string Localizacao { get; set; }
    }

    public class OrganizadorDto
    {
        public string Nome { get; set; }
    }

    public class ProfessorDto
    {
        public string NomeProfessor { get; set; }
        public string NomeInstituicao { get; set; }
    }

    public class DetalhePalestraDto
    {
        public PalestraDto Palestra { get; set; }
        public List<ProfessorDto> Avaliadores { get; set; }
        public List<OrganizadorDto> Organizadores { get; set; }
        public List<ApresentacaoDto> Apresentacoes { get; internal set; }
    }

    public class ApresentacaoDto
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Orientador { get; set; }
    }
}
