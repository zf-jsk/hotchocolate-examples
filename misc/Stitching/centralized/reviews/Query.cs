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

        
        [UseGQLResponseCache]
        public IEnumerable<Review> GetReviewsByObj(
         [Service] ReviewRepository repository,Review review) =>
         repository.GetReviews();

        public IEnumerable<Review> GetReviewsByAuthor(
            [Service] ReviewRepository repository,
            int authorId) =>
            repository.GetReviewsByAuthorId(authorId);
        [UseGQLResponseCache]
        public IEnumerable<Review> GetReviewsByProduct(
            [Service] ReviewRepository repository,
            int upc) =>
            repository.GetReviewsByProductId(upc);

        [UseGQLResponseCache]
        public IEnumerable<Review> GetReviewsByArray(
            [Service] ReviewRepository repository,
            Review[] reviews) =>
           repository.GetReviews();
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
