using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.Services
{

    public interface IPersonDetailsService:IDisposable
    {
        List<PersonDetails> GetAllPersonDetails();
    }
    public class PersonDetailsService: IPersonDetailsService
    {
        private bool disposedValue;

        public PersonDetailsService()
        {

        }

        public List<PersonDetails> GetAllPersonDetails()
        {
            List<PersonDetails> personDetailsList = new List<PersonDetails>();
            try
            {                

                string path = @"Data/PersonsDemo.csv";

                using (var streamReader = File.OpenText(path))
                {
                    using (var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture))
                    {
                        personDetailsList = csvReader.GetRecords<PersonDetails>().ToList<PersonDetails>();
                    }
                }
                
            }
            catch (Exception ex)
            {

            }
            return personDetailsList;

        }

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~PersonDetailsService()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
