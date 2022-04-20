using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.AspNetCore.Http;

namespace Demo.Accounts
{
    public class GQLServiceM
    {
        private readonly HttpClient _httpClient;
        public async Task<UploadedDocumentDetails> documents(String relativePath, IList<IFile> files)
        {
            UploadedDocumentDetails uploadedDocumentDetails = new UploadedDocumentDetails();
            int count=files.Count;
            uploadedDocumentDetails.DocId = relativePath;
            uploadedDocumentDetails.DocUrl = "https://";
            return uploadedDocumentDetails;
        }
    }
    public class Query
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public  Query(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IEnumerable<User> GetUsers( [Service] UserRepository repository)
        {
            return repository.GetUsers();
        }
            

        public User GetUser(int id, [Service] UserRepository repository) => 
            repository.GetUser(id);
    }

    public class SubQuery
    {
        public SubGraph Get_service() => new SubGraph();
    }

    public class SubGraph
    {

        public String sdl { get; set; } = System.IO.File.ReadAllText("./accounts.graphql");
    }
}