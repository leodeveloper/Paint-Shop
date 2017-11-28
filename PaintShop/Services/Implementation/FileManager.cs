using PaintShop.Services.Repository;
using System.Collections.Generic;
using System.IO;

namespace PaintShop.Services.Implementation
{
    public class FileManager : IReadFile
    {
        #region Public Method

        public IEnumerable<string> ReadLine(string pathFile)
        {
            try
            {
                IEnumerable<string> readLines = File.ReadLines(pathFile);
                return readLines;
            }
            catch
            {
                throw;
            }
            
        }

        #endregion
    }
}
