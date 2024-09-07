using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public  interface ICategoryRepository
    {
        List<CategoryDTO> getList();
        bool insert(CategoryDTO category);
        bool update(CategoryDTO category);
        bool delete(int id);
    }
}
