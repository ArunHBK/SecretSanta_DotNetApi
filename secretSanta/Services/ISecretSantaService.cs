using secretSanta.Models;

namespace secretSanta.Services
{
    public interface ISecretSantaService
    {
        public Task<ResponseObject> Randomizer(List<playerDetails> playerList);
        public void SendEmail(string Body, string To);
        public void sendEmailToList(List<playerDetails> playerList, List<string> senderEmailList);
    }
}
