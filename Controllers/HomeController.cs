using EmmyDeveloperPortfolio.Models;
using EmmyDeveloperPortfolio.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EmmyDeveloperPortfolio.Data;
using EmmyDeveloperPortfolio.DTO;

namespace EmmyDeveloperPortfolio.Controllers {
    public class HomeController : Controller {

        private readonly PortfolioModel _portfolio;
        private readonly TaxServices _taxService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeController(TaxServices taxServices, AppDbContext context, IWebHostEnvironment env) {
            _taxService = taxServices;
            _context = context;
            _env = env;
            _portfolio = new PortfolioModel {
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
                },
                Experiences = new List<ExperienceItem>() {
                    new ExperienceItem 
                    {
                        Company = "Pixel Forge Studios",
                        Role = "Unity Game Developer",
                        Duration = "Mar 2024 — Present",
                        Description = "Built core gameplay systems for a 3D platformer using Unity and C#, including custom character controllers, physics-based puzzles, and a save/load system. Collaborated with a small team of 4 to ship two title updates."
                    },
                    new ExperienceItem
                    {
                        Company = "Freelance",
                        Role = "ASP.NET Backend Developer",
                        Duration = "Jan 2025 — Aug 2025",
                        Description = "Designed and built REST APIs for a logistics tracking client using ASP.NET Core and Entity Framework, including authentication, order status endpoints, and Swagger documentation for their mobile team."
                    },
                    new ExperienceItem
                    {
                        Company = "FUOYE Tech Community",
                        Role = "Student Developer & Mentor",
                        Duration = "Sep 2023 — Dec 2024",
                        Description = "Taught introductory C# and game development workshops to fellow students, ran weekly coding sessions, and helped organize a campus-wide hackathon with over 60 participants."
                    },
                    new ExperienceItem
                    {
                        Company = "Bincom Academy",
                        Role = "C# Beginner Class — Student",
                        Duration = "2026",
                        Description = "Currently building this portfolio as a live training project — covering ASP.NET Core MVC, SQL Server, Web APIs, CI/CD with Azure DevOps, and SOLID principles."
                    }
                }
            };
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult About() {

            return View(_portfolio);
        }

        public IActionResult Skills() {
            return View(_portfolio);
        }

        public IActionResult Projects() {
            return View(_portfolio);
        }

        public IActionResult Experience() {
            return View(_portfolio.Experiences);
        }

        [HttpGet]
        public IActionResult Contact() {
            return View(_portfolio);
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactFormDto formDto) {
            var contactMessage = new ContactMessage {
                Name = formDto.Name,
                Email = formDto.Email,
                Message = formDto.Message
            };

            _context.ContactMessages.Add(contactMessage);
            await _context.SaveChangesAsync();

            TempData["ContactSuccess"] = true;
            return RedirectToAction("Contact");
        }


        [HttpGet]
        public IActionResult Tax() {
            return View();
        }

        [HttpPost]
        public IActionResult Tax(TaxInputDto taxInputForm) {
            var result = _taxService.Calculate(taxInputForm.Income, taxInputForm.Pension, taxInputForm.Rent);
            //Console.WriteLine($"Total Annual Tax: {result.Tax} Effective tax rate: {result.EffectiveTaxRate} and Monthly NetIncome: {result.MonthlyNet}");
            return View(result);
        }

        [HttpGet]
        public IActionResult Gallery() {
            var items = _context.GalleryItems.OrderByDescending(g => g.UploadedAt).ToList();
            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> Gallery([FromForm] GalleryUploadDto uploadDto) {

            if (uploadDto.Image != null && uploadDto.Image.Length > 0) {
                // Build a unique filename so two uploads don't overwrite each other
                var fileName = Guid.NewGuid() + Path.GetExtension(uploadDto.Image.FileName);
                var uploadsFolder = Path.Combine(Environment.GetEnvironmentVariable("HOME")!, "data", "uploads");
                Directory.CreateDirectory(uploadsFolder);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create)) {
                    await uploadDto.Image.CopyToAsync(stream);
                }

                // Save the record to the database
                var galleryItem = new GalleryItem {
                    Title = uploadDto.Title,
                    ImagePath = "/uploads/" + fileName
                };

                _context.GalleryItems.Add(galleryItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Gallery");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
