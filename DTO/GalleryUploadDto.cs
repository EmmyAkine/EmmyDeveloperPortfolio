namespace EmmyDeveloperPortfolio.DTO {
    public class GalleryUploadDto {
        public string Title { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
    }
}
