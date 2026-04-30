using App.Presenters;
using Domain;
using System.Threading.Tasks;

namespace App.Interfaces
{
    public interface IPostalCodeService
    {
        Task<ResponseWebApi<InfoEstado>> GetPostalCodeInfo(string postalCode);
    }   
}
