using EmmyDeveloperPortfolio.DTO;
using EmmyDeveloperPortfolio.Models;
using EmmyDeveloperPortfolio.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmmyDeveloperPortfolio.Controllers.Api {
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class PortfolioApiController : ControllerBase {

        private readonly PortfolioModel _portifolio;
        private readonly TaxServices _taxService;

        public PortfolioApiController(TaxServices taxServices) {
            _taxService = taxServices;
            _portifolio = new PortfolioModel {
                Name = "Emmanuel Oyebamiji",
                Email = "emmanueloyebamiji03@gmail.com",
                University = "Federal University of Oye-Ekiti, Ekiti State, Nigeria",
                Department = "Electrical & Electronics Engineering",

                SkillGroups = new List<SkillGroup> {
                    new SkillGroup
                    {
                        Icon = "bi-controller",
                        Title  = "Game Development",
                        Skills = new List<string>{ "Unity Engine", "C# Scripting", "Physics & Animation",
                              "Shaders & VFX", "Multiplayer (Netcode)"}
                    },
                    new SkillGroup
                    {
                        Icon   = "bi-server",
                        Title  = "Backend & APIs",
                        Skills = new List<string>
                            { "ASP.NET Core", "Web API & MVC", "Entity Framework",
                              "SignalR", "gRPC" }
                    },
                    new SkillGroup
                    {
                        Icon   = "bi-database",
                        Title  = "Databases",
                        Skills = new List<string>
                            { "SQL Server", "PostgreSQL", "Redis", "LINQ", "Dapper" }
                    },
                    new SkillGroup
                    {
                        Icon   = "bi-cloud",
                        Title  = "DevOps & Cloud",
                        Skills = new List<string>
                            { "Azure", "Docker", "CI/CD Pipelines", "Git & GitHub", "Vercel" }
                    },
                    new SkillGroup
                    {
                        Icon   = "bi-boxes",
                        Title  = "Architecture",
                        Skills = new List<string>
                            { "Clean Architecture", "SOLID", "Microservices",
                              "DDD", "Unit Testing" }
                    },
                    new SkillGroup
                    {
                        Icon   = "bi-tools",
                        Title  = "Tools & Workflow",
                        Skills = new List<string>
                            { "Visual Studio", "Rider", "Blender", "Jira", "Figma" }
                    },
                },
                Projects = new List<ProjectModel> {
                     new ProjectModel
                    {
                        Title       = "Skyfall Odyssey",
                        Category    = "Unity • 3D Platformer",
                        Description = "A physics-driven 3D platformer with custom character controllers, " +
                                      "dynamic camera systems, and a level streaming pipeline for seamless worlds.",
                        Tags        = new List<string> { "Unity", "C#", "Shader Graph", "Cinemachine" },
                        Featured    = true,
                        GitHubUrl   = "https://github.com/EmmyAkine",
                        LiveUrl     = ""
                    },
                    new ProjectModel
                    {
                        Title       = "Nexus Admin Suite",
                        Category    = "ASP.NET Core • Web Platform",
                        Description = "An enterprise admin dashboard built on ASP.NET Core with role-based auth, " +
                                      "real-time SignalR updates, and rich analytics.",
                        Tags        = new List<string> { "ASP.NET Core", "SignalR", "SQL Server", "EF Core" },
                        Featured    = true,
                        GitHubUrl   = "https://github.com/EmmyAkine",
                        LiveUrl     = ""
                    },
                    new ProjectModel
                    {
                        Title       = "Prism Puzzles",
                        Category    = "Unity • Mobile Game",
                        Description = "A polished casual puzzle game for iOS and Android with 200+ handcrafted levels " +
                                      "and an in-game economy.",
                        Tags        = new List<string> { "Unity", "C#", "Mobile", "In-App Purchase" },
                        Featured    = false,
                        GitHubUrl   = "https://github.com/EmmyAkine",
                        LiveUrl     = ""
                    },
                    new ProjectModel
                    {
                        Title       = "Forge API Platform",
                        Category    = "ASP.NET Core • Backend",
                        Description = "A modular REST & gRPC API platform following clean architecture, " +
                                      "with containerized deployments and full test coverage.",
                        Tags        = new List<string> { "ASP.NET Core", "gRPC", "Docker", "PostgreSQL" },
                        Featured    = false,
                        GitHubUrl   = "https://github.com/EmmyAkine",
                        LiveUrl     = ""
                    },
                }

            };
        }

        [HttpGet]
        public IActionResult Home() {
            return Ok(_portifolio);
        }

        [HttpGet]
        public IActionResult About() {
            var aboutInfo = new List<string>() { _portifolio.Name, _portifolio.Email, _portifolio.University, _portifolio.Department };
            return Ok(aboutInfo);
        }

        [HttpGet]
        public IActionResult Skills() {
            if (_portifolio.SkillGroups == null || _portifolio.SkillGroups.Count <= 0) {
                return NoContent();
            }
            return Ok(_portifolio.SkillGroups);
        }

        [HttpGet]
        public IActionResult Projects() {
            if (_portifolio.Projects == null || _portifolio.Projects.Count <= 0) {
                return NoContent();
            }
            return Ok(_portifolio.Projects);
        }

        [HttpGet]
        public IActionResult Tax([FromQuery] TaxInputDto taxInputForm) {
            var result = _taxService.Calculate(taxInputForm.Income, taxInputForm.Pension, taxInputForm.Rent);
            return Ok(result);
        }
    }
}
