using CosmosDb.Utils.Interop;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace CosmosDb.Model.Shim
{
    internal class ProfileShim : ICosmosDbItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonIgnore()]
        public string EmailAddress => Id;
    }

    public sealed class CosmosDbContextShim : CosmosDbContext
    {
        private new Container container_;

        public CosmosDbContextShim(ICosmosOperations operations) : base(operations)
        {
        }

        public override async Task<Profile> GetProfile(string userId)
        {
            InitializeContext();

            try
            {
                // userId is in fact an emailAddress
                // first lookup in ShimDb for a record matching that email address

                ProfileShim profile = await operations_.GetItemAsync<ProfileShim>(container_, userId, userId);

                // and then, lookup in ProfilesDb for the corresponding record

                return await base.GetProfile(profile.UserId);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        private new void InitializeContext()
        {
            base.InitializeContext();
            container_ = container_ ?? operations_.GetContainer(database_, ConstantsShim.ContainerName);
        }
    }
}
