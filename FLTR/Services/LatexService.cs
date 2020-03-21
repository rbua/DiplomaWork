using FLTR.Data;
using FLTR.Models;

namespace FLTR.Services
{
    public class LatexService
    {
        private IDataManager _dataManager;
        
        public LatexService(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }
        
        public LatexCompilationResult Compile(string fileName)
        {
            
        }
    }
}