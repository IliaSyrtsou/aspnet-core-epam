using Module01_Introduction_task_01.Entities;
using Module01_Introduction_task_01.Services.Interfaces;

namespace Module01_Introduction_task_01.Services
{
    public class BaseService<T>: IBaseService<T> where T: BaseEntity
    {
        
    }
}