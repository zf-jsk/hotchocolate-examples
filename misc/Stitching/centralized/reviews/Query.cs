using System;
using System.Collections.Generic;
using HotChocolate;

namespace Demo.Reviews
{
    public class Query
    {
        public IEnumerable<Review> GetReviews(
            [Service] ReviewRepository repository) =>
            repository.GetReviews();

        public IEnumerable<Review> GetReviewsByAuthor(
            [Service] ReviewRepository repository,
            int authorId) =>
            repository.GetReviewsByAuthorId(authorId);

        public IEnumerable<Review> GetReviewsByProduct(
            [Service] ReviewRepository repository,
            int upc) =>
            repository.GetReviewsByProductId(upc);
    }
    public class SubQuery
    {
        public SubGraph Get_service() => new SubGraph();
    }

    public class SubGraph
    {
        public String sdl { get; set; } = System.IO.File.ReadAllText("./reviews.graphql");
    }
}
