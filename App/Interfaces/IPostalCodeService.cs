using Domain;
using System.Threading.Tasks;

namespace App.Interfaces
{
    public interface IPostalCodeService
    {
        Task<PostalCodeXmlResponse> GetPostalCodeInfo(string postalCode);
    }   
}
