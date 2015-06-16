using ONECardPublisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            // You can find your Company Id and Company Password (Company Secret) within the "Get started" section on one.sitrion.com
            Guid CompanyId = new Guid("FEA28DF4-3B31-489B-AE7A-43B5B19CE878");
            string CompanyPassword = "MyCompanySecret";
            var publisher = new Publisher(CompanyPassword, CompanyId);

            // Create a CardRouting object with the metadata for a Card
            var cardrouting = new CardRouting()
            {
                // Initially you have to create a card, with the card id you can also execute other actions on the same card
                ActionType = CardActionType.Create,

                // The datetime which shows up in the header of a card
                CardDate = DateTime.UtcNow,

                // This needs to be a unique id for your card, this id can be used later for card actions like delete or update 
                CardId = Guid.NewGuid().ToString(),

                // This is the identifier between the metadata you're sending here and the card UI, which you have to build with the AppBuilder
                MessageType = "MyCard",

                // This get's displayed in the card header as the person who sent the card,
                // if the Email property matches a ONE user, the profile picture on the card will be retrieved based in this 
                Actor = new SimplePerson() { DisplayName = "John Doe", Email = "john.doe@sitrion.com" },

                // With SendTo you can define the ONE users which should receive this card,
                // if you keep SendTo empty, everyone who has assigned this card in a role will receive the card                
                SendTo = new SimplePerson[] { new SimplePerson() { DisplayName = "Jane Doe", Email = "jane.doe@sitrion.com" } },

                // Data is a simple Dictionary which you can be prefilled with the data which you would need on a card
                Data = new Dictionary<string, string>() { { "PurchaseOrderId", "123456789" }, { "Title", "New Purchase Order" } }
            };


            // Now you can publish the data to the cloud. 
            // If the card UI, which you build with AppBuilder is already there, the TranslateCard method within your card code should combine this metadata with your card
            // If you haven't build the card or assinged the card to a role, this metadata gets ignored
            publisher.PublishCard(cardrouting);
        }
    }
}
