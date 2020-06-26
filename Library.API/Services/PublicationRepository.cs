using Library.API.DbContexts;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public class PublicationRepository : RepositoryBase<Domain.Models.Publication>, IPublicationRepository
    {
        public PublicationRepository(LibraryContext context):base(context)
        {
        }

        public IQueryable<Publication> GetFullPublications()
        {
            var publications = _context.Set<Publication>().Include(p => p.PublicationAuthors)
                .ThenInclude(publicationAuthors => publicationAuthors.Author) as IQueryable<Publication>;

            publications = publications.Include(p => p.PublicationCategories).ThenInclude(pc => pc.Category);

            return publications;
        }

        public async Task<Publication> GetFullPublicationAsync(Guid publicationId)
        {
            var publications = GetFullPublications();

            publications = publications.Include(p => p.PublicationCategories).ThenInclude(pc => pc.Category);

            var publicationToReturn = await publications.FirstOrDefaultAsync(p => p.Id == publicationId);

            return publicationToReturn;
        }

        public async Task<ICollection<Publication>> GetFullPublicationsAsync()
        {
            var publications = GetFullPublications();

            publications = publications.OrderBy(p => p.Title)
                .ThenBy(p => p.PublicationAuthors.Select(pa => pa.Author).OrderBy(a => a.Surname).First());

            var publicationToReturn = await publications.ToListAsync();

            return publicationToReturn;
        }
    }
}
