using Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class TestServices : ITestServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public SysUser Test()
        {
            var a = _unitOfWork.GetRepository<SysUser>();
            return a.GetFirstOrDefault();
        }
    }
}
