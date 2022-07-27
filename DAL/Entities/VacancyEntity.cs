namespace App.Entities
{
    public class VacancyEntity : BaseEntity
    {
        public string Title { get; private set; }
        public string? Description { get; set; }

        public VacancyEntity(string title, string? description)
        {
            Title = title;
            Description = description;
        }
    }
}