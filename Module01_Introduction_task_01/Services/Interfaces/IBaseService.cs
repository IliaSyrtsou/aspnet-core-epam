using Module01_Introduction_task_01.Entities;
namespace Module01_Introduction_task_01.Services.Interfaces
{
    public interface IBaseService<T> where T: BaseEntity
    {
    }
}