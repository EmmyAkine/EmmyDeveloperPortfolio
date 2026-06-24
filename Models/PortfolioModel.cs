namespace EmmyDeveloperPortfolio.Models {
    public class PortfolioModel {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string University { get; set; } = "";    
        public string Department { get; set; } = "";
        public List<SkillGroup> SkillGroups { get; set; } = [];
        public List<ProjectModel> Projects { get; set; } = [];
        public List<ExperienceItem> Experiences { get; set; } = [];
    }
}
