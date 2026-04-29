using App.Presenters;
using Domain;
using System.Threading.Tasks;

namespace App.Interfaces
{
    public interface IPostalCodeService
    {
        Task<Response<InfoEstado>> GetPostalCodeInfo(string postalCode);
    }   
}
