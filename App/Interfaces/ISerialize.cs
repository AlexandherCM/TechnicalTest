using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Interfaces
{
    public interface ISerialize
    {
        T DeserializeFullXML<T>(string xmlContent);
        string ConvertirXML<T>(T obj);
    }
}
