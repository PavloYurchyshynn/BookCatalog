using AutoMapper;
using BookCatalog.Application.Models.Comment;
using BookCatalog.Application.Services.Contracts;
using BookCatalog.Core.Entities;
using BookCatalog.DataAccess.Repositories.Contracts;

namespace BookCatalog.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        private readonly IBookRepository _bookRepository;

        public CommentService(IMapper mapper, ICommentRepository commentRepository, IBookRepository bookrepository)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
            _bookRepository = bookrepository;
        }

        public async Task<CommentModel> AddCommentAsync(AddCommentModel comment)
        {
            var entity = _mapper.Map<Comment>(comment);
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            var book = await _bookRepository.GetFirstAsync(b => b.id == entity.BookId);

            await _commentRepository.AddAsync(entity);
            return _mapper.Map<CommentModel>(entity);
        }
    }
}
