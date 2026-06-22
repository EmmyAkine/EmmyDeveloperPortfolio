namespace EmmyDeveloperPortfolio.Models {
    public class ProjectModel {
        public string Title { get; set; } = "";
        public string Category { get; set; } = "";
        public string Description { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public List<string> Tags { get; set; } = [];
        public bool Featured { get; set; } = false;
        public string GitHubUrl { get; set; } = "";
        public string LiveUrl { get; set; } = "";
    }

    public class SkillGroup {
        public string Icon { get; set; } = "";   // Bootstrap icon class e.g. "bi-controller"
        public string Title { get; set; } = "";
        public List<string> Skills { get; set; } = [];
    }
}
