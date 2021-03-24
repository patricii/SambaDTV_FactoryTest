using I2CRack;
using ModFactoryTestCore.Interfaces;
using System;
using System.IO;
using System.Text;

namespace ModFactoryTestCore
{
    public class Rack : IPowerable
    {
        public Rack()
        {
            PowerOn();
        }

        public void PowerOn()
        {
            if (File.Exists(".\\station.ini"))
            {
                CItemListEquip.LoadBZConfig();
                CheckReturn(CItemListEquip.InitItemListEquip());// Check GPIB conections etc..
                CheckReturn(CJagLocalFucntions.EntryHandlerTest());
            }
            else
            {
                throw new RackException("Can not find station.ini file.");
            }
        }

        public void PowerOff()
        {
            throw new NotImplementedException();
        }
        
        
        private void CheckReturn(int retCode)
        {
            if (retCode != TestCoreMessages.SUCCESS)
                throw new RackException("Error " + retCode + " Fail to initialize BZ Equipments returned from \"wrapper_Handler_Bz.dll\" when try configure Rack.");
        }      




        /// <summary>
        /// Class that represents Rack custom exceptions
        /// </summary>
        [Serializable]
        public class RackException : Exception
        {
            public RackException()
            { }

            public RackException(string message)
                : base(message)
            { }

            public RackException(string message, Exception innerException)
                : base(message, innerException)
            { }
        }
    
    }
}
