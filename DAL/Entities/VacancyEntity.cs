namespace App.Entities
{
    public class VacancyEntity : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }

        public long? WageFrom { get; private set; }
        public long? WageTo { get; private set; }

        public VacancyEntity(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public VacancyEntity(string title, string description, long? wageFrom, long? wageTo) : this(title, description)
        {
            WageFrom = wageFrom;
            WageTo = wageTo;
        }
    }
}