namespace EmmyDeveloperPortfolio.Models {
    public class GalleryItem {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string ImagePath { get; set; } = "";
        public DateTime UploadedAt { get; set; } = DateTime.Now;

    }
}
