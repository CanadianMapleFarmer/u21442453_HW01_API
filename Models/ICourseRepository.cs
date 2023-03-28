namespace u21442453_HW01_API.Models
{
    public interface ICourseRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        // Course
        Task<Course[]> GetAllCourseAsync();
        Task<Course> GetCourseAync(int courseID);
    }
}
