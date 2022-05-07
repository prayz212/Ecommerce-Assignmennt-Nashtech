namespace BackEnd.Services
{
    public class BaseService
    {
        protected int GetTotalPage(int count, int size)
        {
            if (count < 0 || size <= 0) return -1;
            return count % size == 0 ? count / size : (count/ size) + 1;
        }
    }
}