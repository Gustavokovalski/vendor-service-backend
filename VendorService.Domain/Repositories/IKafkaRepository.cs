using System.Threading.Tasks;

namespace VendorService.Domain.Repositories
{
    public interface IKafkaRepository
    {
        string SendMessageByKafka(string message);
    }
}
